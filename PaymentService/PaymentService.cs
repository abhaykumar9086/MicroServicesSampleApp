using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using Newtonsoft.Json;
using SharedMicroServiceLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PaymentService
{
    [Authorize]
    public class PaymentController : ApiController
    {
        private readonly string accessToken = "accessToken";
        private readonly string OrderServiceHostUrl = "OrderService.Host";

        public PaymentController()
        {
            accessToken = ConfigurationManager.AppSettings["accessToken"];
            OrderServiceHostUrl = ConfigurationManager.AppSettings["OrderService.Host"];
        }

        [Route("api/Payment/")]
        [HttpPost]
        [Authorize]
        public ANetApiResponse CreatePaymentResponse(PaymentDTO paymentDTO, String ApiLoginID, String ApiTransactionKey)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = paymentDTO.cardNumber,
                expirationDate = paymentDTO.expirationDate,
                cardCode = paymentDTO.CardCode
            };

            var billingAddress = new customerAddressType
            {
                firstName = paymentDTO.firstName,
                lastName = paymentDTO.lastName,
                address = paymentDTO.address,
                city = paymentDTO.city,
                zip = paymentDTO.zip
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };
            Decimal amount = new Decimal(0.0);

            //Call to API OrderService to get the information of order of customer that he/she has purchased.
            using (HttpClient client = new HttpClient())
            {
                String content = String.Empty;
                client.BaseAddress = new Uri(OrderServiceHostUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + accessToken);
                HttpResponseMessage httpResponseMessage = client.GetAsync("Api/Order/GetOrderDetails/" + paymentDTO.OrderID).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                    Object[] orderDetails = (Object[])json_serializer.DeserializeObject(content);

                    var lineItems = new lineItemType[orderDetails.Count()];

                    for (int i = 0; i < orderDetails.Count(); i++)
                    {
                        Dictionary<String, Object> orderdicobj = ((object[])json_serializer.DeserializeObject(content))[i] as Dictionary<String, Object>;
                        Dictionary<String, Object> productdic = (Dictionary<String, Object>)orderdicobj["Product"];

                        lineItems[i] = new lineItemType
                        {
                            itemId = orderdicobj["ProductID"].ToString(),
                            name = productdic["ProductName"].ToString(),
                            quantity = Convert.ToInt32(orderdicobj["Quantity"]),
                            unitPrice = Convert.ToDecimal(orderdicobj["UnitPrice"]),
                        };
                        amount = amount + (lineItems[i].quantity * lineItems[i].unitPrice);
                    }

                    var transactionRequest = new transactionRequestType
                    {
                        transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card

                        amount = amount,
                        payment = paymentType,
                        billTo = billingAddress,
                        lineItems = lineItems
                    };

                    var request = new createTransactionRequest { transactionRequest = transactionRequest };

                    // instantiate the contoller that will call the service
                    var controller = new createTransactionController(request);
                    controller.Execute();

                    // get the response from the service (errors contained if any)
                    var response = controller.GetApiResponse();

                    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                    {
                        if (response.transactionResponse != null)
                        {
                            Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                        }
                    }
                    else if (response != null)
                    {
                        Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                        if (response.transactionResponse != null)
                        {
                            Console.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
                        }
                    }

                    return response;
                }
                else
                {
                    // something went wrong.
                }
            }
            return null;
        }
    }
}

using DataAccesLayer.Models.Auxillary;
using DataAccessLayer.RepositotyPatterns;
using DataAccessLayer.ViewModels;
using DataAccessLayer.ViewModels.HD_ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HomeDelivey.HomeDelivery_BussinessLgic
{

    public class HDBussinessLogic : Repository, IHDBussinessLogic
    {
        private readonly ISetups _setups;
        public HDBussinessLogic(HDContext context, ISetups setups) : base(context)
        {
            _setups = setups;
        }
        //posting hd order
        public async Task<ResponseMessage> PostOrder(CustomerOrderViewModel co)
        {
            var result = await RecheckOrderAmount(co.MaterialCode, co.Quantity, co.Price);
            if (result.Status != "Success")
            {
                return result;
            }
            //if the hd customer does not exist create one.
            var ifcustomerexists = await CustomerDetails(co.PhoneNumber);
            if (ifcustomerexists == null)
            {
                var postcustomer = $@"insert into Customer(CustomerName,CustomerNumber,Latitude,Longitude,
                RouteID,ContactPerson,ContactNumber,Street,Area,DateAdded,CreatedBy,
                Pricingid,status,customercategoty)Values ('{co.CustomerName}','',
                '{co.Latitude}','{co.Longitude}','','{co.CustomerName}',
                '{co.PhoneNumber}','{co.Street}','',getdate(),
                '{co.CustomerName},'5001',''";
                try
                {
                    await UpdateAsync(postcustomer);
                    return new ResponseMessage { Message = "Customer Updated SuccessFully", Status = "Success" };
                }
                catch (Exception e)
                {
                    return new ResponseMessage { Message = e.Message, Status = "Failed" };
                }
            }//post order
            var customerid = await CustomerDetails(co.PhoneNumber);
            var Sales = await _setups.CodeGenerators("Salesid");
            //add order record to sales details
            var querry = $@"insert into SaleDetail(Saleid,MaterialCode,HasAnExchange,Quantity,SKUTransAmount)
                            Values ({Sales.Message},{co.MaterialCode},{0},{co.Quantity},{co.Price})";

            //add order record to sales Order
            var querry2 = $@"insert into saleorder(Tripid,Saleid,CustomerID,Latitude,Longitude,TransAmount,ReceivedAmount,
				            Variance,Comment,TransTime,DateAdded,Status,ReceiptStatus,CreatedBy)
                            Values ('',{Sales.Message},{customerid.CustomerId},{co.Latitude},{co.Longitude},{co.Price},{0},{0},'',
                            {DateTime.Now},{DateTime.Now},{0},{0},{co.CustomerName})";

            //update codegenerators the next current number
            var updatenextnumber = $@"Update CodeGenerators Set CurrentNumber = CurrentNumber+{Sales.Values} ";
            try
            {
                await UpdateAsync(querry);
                await UpdateAsync(querry2);
                await UpdateAsync(updatenextnumber);

                return new ResponseMessage { Message = "Order Posted SuccessFully", Status = "Success" };
            }
            catch (Exception e)
            {
                return new ResponseMessage { Message = e.Message, Status = "Failed" };
            }
        }
        //return all previous orders
        public async Task<IEnumerable<PreviousOrdersViewModel>> PreviousOrders(string phonenumber)
        {
            var customerdetails = await CustomerDetails(phonenumber);
            var querry = $@"SELECT top 10 sd.DateAdded,Quantity,Replace(ProductDescription,'CYLINDER','')
                        AS ProductDescription FROM SaleDetail sd
                        inner join SaleOrder so on sd.saleid  =so.saleid
                        inner join Product p on p.SAPMaterialCode = sd.MaterialCode
                        WHERE so.customerid  = '{customerdetails.CustomerId}'
                        order by sd.DateAdded desc";
            var result = await FindOptimisedAsync<PreviousOrdersViewModel>(querry);
            return result;
        }
        //Customer Details
        public async Task<CustomerDetails> CustomerDetails(string phonenumber)
        {
            var querry = $@"Select Customerid,CustomerName,ContactNumber as PhoneNumber from Customer where ContactNumber = '{phonenumber}' ";
            var custdetails = await FirstOrDefaultAsync<CustomerDetails>(querry);
            if (custdetails != null)
            {
                return new CustomerDetails
                { CustomerName = custdetails.CustomerName, PhoneNumber = custdetails.PhoneNumber };
            }
            return new CustomerDetails { };
        }
        // a list of all product and prices
        public async Task<IEnumerable<Catalog>> GetCatalog()
        => await FindOptimisedAsync<Catalog>(@"select Price,ltrim(rtrim(MaterialCode)) as MaterialCode,ltrim(rtrim(ProductDescription)) as ProductDescription,
                    isnull(Size,'') as Size,ltrim(rtrim(Brand)) as Brand from productpricing pp
                    inner join product p on p.id = pp.Productid where pricingid = '5001'
                    order by Productid");
        public async Task<ResponseMessage> RecheckOrderAmount(string materialcode, int quantity, double price)
        {
            var querry = $@"select MaterialCode,Price from ProductPricing where PricingId
            = '5001' and MaterialCode = '{materialcode}'";

            var result = await FirstOrDefaultAsync<SKUPrice>(querry);
            if (result == null)
            {
                return new ResponseMessage { Message = "Something Went Wrong Try Again", Status = "Failed" };
            }

            if (result.Price * quantity != price)
            {
                return new ResponseMessage { Message = "The Product Price Have Been Updated Kindly Clear Cache To Continue", Status = "Failed" };
            }
            else
                return new ResponseMessage { Message = "The Product Price Is Correct", Status = "Success" };
        }
    }
}

using DataAccesLayer.Models.Auxillary;
using DataAccessLayer.RepositotyPatterns;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HomeDelivey.HomeDelivery_BussinessLgic
{
    internal class Setups : Repository, ISetups
    {
        public Setups(HDContext context) : base(context)
        {
        }
        public async Task<CodeGenResponse> CodeGenerators(string TypeName)
        {
            var querry = $@"select * from CodeGenerators where TypeName = '{TypeName}'";
            var result = await FirstOrDefaultAsync<CodeGeneratorsViewModel>(TypeName);
            if (result == null)
            {
                return new CodeGenResponse { Message = "", Status = "" };
            }
            var nextnumber = result.Seed + result.CurrentNumber;
            var nextchar = result.Prefix + nextnumber.ToString().PadLeft(result.NumberOfCharacter, '0');
            return new CodeGenResponse { Message = nextchar, Status = "Success", Values = nextnumber };
        }
    }
}

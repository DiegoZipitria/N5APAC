using PAC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.IBusinessLogic
{
    public interface ITokenManager
    {
        string GenerateToken(StudentClaim user);
        StudentClaim ParseToken(string token);
        StudentClaim ValidateToken(string token);
    }
}

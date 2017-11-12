using System.Data;

namespace StkGenCode.Code.Template
{
    public class PropertiesCodeValidatetor : CodeBase
    {
        private string Usign()
        {
            var code = "//https://github.com/JeremySkinner/FluentValidation/wiki/a.-Index" + NewLine;

            code += "using FluentValidation;" + NewLine;
            code += "using WebApp.Business;" + NewLine;
            return code;
        }

        private string BeginNameSpaceAndClass()
        {
            var code = "";
            code = "namespace WebApp.AppCode.Business" + NewLine;
            code += "{" + NewLine;
            code += $"public class {TableName}Validatetor :  AbstractValidator<{TableName}>" + NewLine;
            code += "{" + NewLine;

            return code;
        }

        private string EndNameSpaceAndClass()
        {
            var code = "} }" + NewLine;
            //_code += " }"; //End Name Space

            return code;
        }

        private string Constructor()
        {
            var code = "";
            code += $"public {TableName}Validatetor()";
            code += "{" + NewLine;

            // code += " RuleFor(APP_USER => APP_USER.FirstName).NotEmpty();" + NewLine;
            //var code = "";
            foreach (DataColumn dataColumn in Ds.Tables[0].Columns)
            {
                var nullType = dataColumn.DataType.ToString() == "System.String" ? "" : "?";

                //ยอมให้ Data Type Byte[] ผ่าน
                if (ExceptionType.Contains(dataColumn.DataType.ToString()) &&
                    dataColumn.DataType.ToString() != "System.Byte[]")
                    continue;

                if (dataColumn.Table.PrimaryKey[0].ToString() == dataColumn.ColumnName &&
                    Ds.Tables[0].PrimaryKey[0].AutoIncrement)
                    continue;


                string commentPrefix ="";
                if (dataColumn.AllowDBNull == true)
                {
                    commentPrefix = "";
                }

                else
                {
                    commentPrefix = "//";

                }


                code += commentPrefix+$" RuleFor({TableName.ToLowerInvariant()} => {TableName.ToLowerInvariant()}.{dataColumn.ColumnName}).NotEmpty();" + NewLine;
            }

            code += "" + NewLine;

            code += "}" + NewLine;

            return code;
        }

        public string Comment()
        {
            var code = "";

            code +=
                "//RuleFor(customer => customer.Forename).NotEmpty().WithMessage(\"Please specify a first name\");" +
                NewLine;
            code += "//RuleFor(customer => customer.Discount).NotEqual(0).When(customer => customer.HasDiscount);" +
                    NewLine;
            code += "//RuleFor(customer => customer.Address).Length(20, 250);" + NewLine;
            code +=
                "// RuleFor(customer => customer.Postcode).Must(BeAValidPostcode).WithMessage(\"Please specify a valid postcode\");" +
                NewLine;
            code += "//private bool BeAValidPostcode(string postcode) { " + NewLine;
            code += "// custom postcode validating logic goes here" + NewLine;
            code += "//}  " + NewLine;
            code += "//Customer customer = new Customer();" + NewLine;
            code += "//CustomerValidator validator = new CustomerValidator();" + NewLine;
            code += "//ValidationResult results = validator.Validate(customer);" + NewLine;
            code += "  " + NewLine;
            code += "//bool validationSucceeded = results.IsValid;" + NewLine;
            code += "//IList<ValidationFailure> failures = results.Errors;" + NewLine;

            return code;
        }

        public override void Gen()
        {
            var code = "";
            code += Usign();
            code += BeginNameSpaceAndClass();

            code += Constructor();
            code += EndNameSpaceAndClass();

            code += Comment();

            InnitProperties();
            FileCode.WriteFile(FileName.PropertiesValidateName(), code);
        }
    }
}
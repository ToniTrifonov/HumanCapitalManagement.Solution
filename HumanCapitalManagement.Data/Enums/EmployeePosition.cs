using System.ComponentModel;

namespace HumanCapitalManagement.Contracts.Enums
{
    public enum EmployeePosition
    {
        [Description("Back-End Developer")]
        BackEndDeveloper = 1,

        [Description("Front-End Developer")]
        FrontEndDeveloper = 2,

        [Description("QA Engineer")]
        QAEngineer = 3,

        [Description("Support")]
        Support = 4
    }
}
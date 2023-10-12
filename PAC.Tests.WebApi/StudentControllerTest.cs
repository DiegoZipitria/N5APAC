namespace PAC.Tests.WebApi;
using Moq;
using PAC.IBusinessLogic;
using PAC.Domain;
using PAC.WebAPI;
using Microsoft.AspNetCore.Mvc;
using PAC.BusinessLogic;

[TestClass]
public class StudentControllerTest
{
    private Mock<IStudentLogic>? _studendtlogicMock;
    private StudentController? _studentController

    private const string ValidUsername = "Pedro123";
    private const int ValidId = 1;
    private const string ValidEmail = "pedro@gmail.com";
    private const string ValidPassword = "iiiiiiiA1!";
    private const string ValidDeliveryAddress = "Cuareim 1451";


    [TestInitialize]
    public void InitTest()
    {
        _studendtlogicMock = new Mock<IStudentLogic>(MockBehavior.Strict);
        _studentController = new StudentController(_studendtlogicMock.Object);
    }

    // Importa las bibliotecas necesarias
    // ...
    [TestMethod]
    public void CreateUserAdmin_ValidUserModel_ReturnsCreatedResponse()
    {
        var studentRequest = new Student { Id = 1, Name = "Ejemplo" };
        _studendtlogicMock.Setup(service => service.InsertStudents(It.IsAny<Student>()));

        IActionResult result = _studentController.PostStudentOk(studentRequest);

        CreatedResult createdResult = result as CreatedResult;
        Assert.AreEqual(201, createdResult.StatusCode);
        _studendtlogicMock.VerifyAll();
    }


    [TestMethod]
    public void PostStudentFail_StudentLogicFails_ReturnsInternalServerError()
    {
        // Configura el mock para que arroje una excepción al llamar a InsertStudents
        _studendtlogicMock.Setup(service => service.InsertStudents(It.IsAny<Student>()))
            .Throws(new Exception("Error al insertar el estudiante"));

        var studentRequest = new Student { Id = 1, Name = "Ejemplo" };

        // Ejecuta la acción y captura la respuesta
        IActionResult result = _studentController.PostStudentOk(studentRequest);

        // Verifica que la respuesta sea un InternalServerError
        Assert.IsInstanceOfType(result, typeof(Exception));
    }


}

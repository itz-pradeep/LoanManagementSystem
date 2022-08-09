using AutoMapper;
using FluentAssertions;
using Loan.API.Controllers;
using Loan.API.Dtos.Loan;
using Loan.API.Helpers;
using Loan.Core;
using Loan.Core.Entities;
using Loan.Core.Interfaces;
using Loan.Core.Specifications;
using Loan.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.UnitTests.Systems.Controllers
{
    public class LoanControllerTests
    {
        private readonly Mock<IGenericRepository<LoanApplication>> _mockLoanRepo;
        private readonly Mock<IRepositoryWrapper> _mockRepoWrapper;
        private readonly Mock<IGenericRepository<LoanType>> _mockLoanTypeRepo;
        private readonly IMapper _mapper;
        private LoanController _controller;
        public LoanControllerTests()
        {
            _mockLoanRepo = new Mock<IGenericRepository<LoanApplication>>();
            _mockLoanTypeRepo = new Mock<IGenericRepository<LoanType>>();
            _mockRepoWrapper = new Mock<IRepositoryWrapper>();
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });

            _mapper = mockMapper.CreateMapper();

            _controller = new LoanController(_mockLoanRepo.Object,_mockLoanTypeRepo.Object,_mockRepoWrapper.Object,_mapper);
        }

        private List<LoanApplication> ListLoanApplications()
        {
            return new List<LoanApplication>(){
                new LoanApplication(){
                    Id = 1,
                    FirstName = "Rose",
                    LastName = "Test",
                    CreatedDate=DateTime.Now,
                    PropertyAddress = "test",
                    LoanAmount = 65456,
                    LoanTenure = 5,
                    LoanTypeId = 1,
                    LoanType = new LoanType()
                    {
                        Id = 1,
                        Type = "Conventional mortgages",
                        IsActive = true
                    },
                    LoanStatus= new LoanStatus()
                    {
                        Id=1,
                        Status="Pre-Approval",
                        IsActive=true

                    },
                    IsActive= true
                },
                new LoanApplication(){
                    Id = 2,
                    FirstName = "Guava",
                    LastName = "Test",
                    CreatedDate=DateTime.Now,
                    PropertyAddress = "test",
                    LoanAmount = 65456,
                    LoanTenure = 5,
                    LoanTypeId = 1,
                    LoanType = new LoanType()
                    {
                        Id = 1,
                        Type = "Conventional mortgages",
                        IsActive = true
                    },
                    LoanStatus= new LoanStatus()
                    {
                        Id=1,
                        Status="Pre-Approval",
                        IsActive=true

                    },
                    IsActive= true
                }
            };
        }

        [Fact]
        public async void GetLoanApplicationByID_UnKnownIdPassed_ReturnNotFoundResult()
        {
            //Arrange
            int id = 0;

            _mockRepoWrapper.Setup(service => service.LoanRepo.GetByIdWithCriteria(id))
                .ReturnsAsync(() => null);
            //Act
            var okResult = await _controller.GetLoanApplicationByID(id);

            //Assert
            okResult.Should().BeOfType<NotFoundObjectResult>();

        }

        [Fact]
        public async void GetLoanApplicationByID_KnownIdPassed_ReturnOkResult()
        {
            //Arrange
            int id = 1;

            _mockRepoWrapper.Setup(service => service.LoanRepo.GetByIdWithCriteria(id))
                .ReturnsAsync(new LoanApplication());

            //Act
            var okResult = await _controller.GetLoanApplicationByID(id);

            //Assert
            okResult.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public async void GetLoanApplicationByID_OnKnownIdPassed_ReturnRightItem()
        {
            //Arrange
            int id = 1;

            var data = new LoanApplication()
            {
                Id = id,
                FirstName = "Guava",
                LastName = "Test",
                CreatedDate = DateTime.Now,
                PropertyAddress = "test",
                LoanAmount = 65456,
                LoanTenure = 5,
                LoanTypeId = 1,
                LoanStatusId = 1,
                IsActive = true
            };

            _mockRepoWrapper.Setup(service => service.LoanRepo.GetByIdWithCriteria(id))
                .ReturnsAsync(data);
            //Act
            var okResult = (OkObjectResult) await _controller.GetLoanApplicationByID(id);

            //Assert
            okResult.Should().BeOfType<OkObjectResult>();
            var result = okResult.Value as LoanApplicationResponse;

            Assert.Equal(id,result?.Id);

        }

        [Fact]
        public async Task GetLoanApplications_OnSuccess_Return200Ok()
        {
            LoanApplicationFilter filter = new LoanApplicationFilter();

            _mockRepoWrapper.Setup(service => service.LoanRepo.GetAllWithCriteria(filter))
             .ReturnsAsync(ListLoanApplications);

            //Act
            var okResult = (OkObjectResult)await _controller.GetLoanApplications(filter);

            okResult.Should().BeOfType<OkObjectResult>();

        }


        [Fact]
        public async Task GetLoanApplications_OnSuccess_ReturnAllRecords()
        {
            LoanApplicationFilter filter = new LoanApplicationFilter();

            _mockRepoWrapper.Setup(service => service.LoanRepo.GetAllWithCriteria(filter))
             .ReturnsAsync(ListLoanApplications);

            //Act
            var okResult = (OkObjectResult)await _controller.GetLoanApplications(filter);

            okResult.Should().BeOfType<OkObjectResult>();

            var allRecords = okResult.Value as List<LoanApplicationResponse>;

            allRecords.Should().HaveCount(ListLoanApplications().Count());

        }

        //[Fact]
        //public async void Add_InvalidObjectPassed_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var firstNameMissing = new CreateLoanRequest();

        //    _controller.ModelState.AddModelError("FirstName", "Required");

        //    _mockRepoWrapper.Setup(service => service.LoanRepo.CreateAsync(data));
        //    // Act
        //    var badResponse = await _controller.PostLoanApplication(firstNameMissing);
        //    // Assert
        //    badResponse.Should().BeOfType<BadRequestObjectResult>();
        //}

        //[Fact]
        //public async Task Add_ValidObjectPassed_Returns200Response()
        //{
        //    // Arrange
        //    var data = new CreateLoanRequest()
        //    {
        //        FirstName = "Guava",
        //        LastName = "Test",
        //        PropertyAddress = "test",
        //        LoanAmount = 65456,
        //        LoanTenure = 5,
        //        LoanTypeId = 1
        //    };

        //    _mockRepoWrapper.Setup(service => service.LoanRepo.CreateAsync(data));

        //    // Act
        //    var createdResponse = await _controller.PostLoanApplication(data);

        //    // Assert
        //    createdResponse.Should().BeOfType<OkObjectResult>();
        //}

        //[Fact]
        //public async Task GetLoanApplications_OnSuccess_ReturnListOfApplications()
        //{
        //    LoanApplicationFilter filter = new LoanApplicationFilter();
        //    var spec = new LoanWithTypeAndStatus(filter);
        //    var repository = await CreateRepositoryAsync();

        //    var data = await repository.GetListByFilterAsync(spec);


        //    //Act
        //    var okResult = (OkObjectResult)await _controller.GetLoanApplications(filter);

        //    okResult.Should().BeOfType<OkObjectResult>();

        //    okResult.Value.Should().BeOfType<List<LoanApplicationResponse>>();

        //}

        //[Fact]
        //public async Task GetLoanApplicationByID_OnUnknownIdPassed_Return404NotFound()
        //{
        //    var id = 0;

        //    var notFoundResult = (NotFoundObjectResult) await _controller.GetLoanApplicationByID(id);

        //    notFoundResult.Should().BeOfType<NotFoundObjectResult>();

        //}





        //[Fact]
        //public async Task GetLoanApplicationByID_KnowIdPassed_Return200Ok()
        //{
        //    var id = 1;

        //    var okResult = (OkObjectResult)await _controller.GetLoanApplicationByID(id);

        //    Assert.IsType<OkObjectResult>(okResult as OkObjectResult);

        //}


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ApiExample.Persistence;
using ApiExample.Models;
using ApiExample.Controllers;

namespace ApiExample.Tests
{
  public class ItemsControllerTests : IDisposable
  {
    Mock<IItemRepository> mockRepo;

    public ItemsControllerTests()
    {
      mockRepo = new Mock<IItemRepository>();
    }

    public void Dispose()
    {
      mockRepo = null;
    }

    [Fact]
    public void GetAll_Returns200OK_WhenDBIsEmpty()
    {
      //Arrange
      mockRepo.Setup(repo => repo.GetAll()).Returns(GetItems(0));
      var controller = new ItemsController(mockRepo.Object);
      //Act
      var result = controller.Get();
      //Assert
      Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetAll_ReturnsOneItem_WhenDBHasOneResource()
    {
      //Arrange
      mockRepo.Setup(repo => repo.GetAll()).Returns(GetItems(1));
      var controller = new ItemsController(mockRepo.Object);
      //Act
      var result = controller.Get();
      //Assert
      var okResult = result as OkObjectResult;
      var items = okResult.Value as List<Item>;
      Assert.Single(items);
    }

    private List<Item> GetItems(int num)
    {
      List<Item> items = new List<Item>();
      Guid id = new Guid();
      if (num > 0)
      {
        items.Add(new Item { Id = id, Name = $"name {id}" });
      }
      return items;
    }
  }
}

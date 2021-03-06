﻿#region copyright

// Copyright Information
// ==================================
// SpyStore.Hol - SpyStore.Hol.Dal.Tests - ShoppingCartRepoTests.cs
// All samples copyright Philip Japikse
// http://www.skimedic.com 2019/10/04
// See License.txt for more information
// ==================================

#endregion

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.Hol.Dal.EfStructures;
using SpyStore.Hol.Dal.Exceptions;
using SpyStore.Hol.Dal.Initialization;
using SpyStore.Hol.Dal.Repos;
using SpyStore.Hol.Dal.Repos.Interfaces;
using SpyStore.Hol.Dal.Tests.RepoTests.Base;
using SpyStore.Hol.Models.Entities;
using Xunit;

namespace SpyStore.Hol.Dal.Tests.RepoTests
{
    [Collection("SpyStore.DAL")]
    public class ShoppingCartRepoTests : RepoTestsBase
    {
        private readonly IShoppingCartRepo _repo;

        public ShoppingCartRepoTests()
        {
            _repo = new ShoppingCartRepo(Db, new ProductRepo(Db), new CustomerRepo(Db));
            Db.CustomerId = 1;
            LoadDatabase();
        }

        public override void Dispose()
        {
            _repo.Dispose();
        }

        [Fact]
        public void ShouldAddAnItemToTheCart()
        {
            var item = new ShoppingCartRecord()
            {
                ProductId = 2,
                Quantity = 3,
                DateCreated = DateTime.Now,
                CustomerId = Db.CustomerId
            };
            _repo.Add(item);
            var shoppingCartRecords = _repo.GetShoppingCartRecords(Db.CustomerId).ToList();
            Assert.Equal(2, shoppingCartRecords.Count);
            Assert.Equal(33, shoppingCartRecords[0].ProductId);
            Assert.Equal(1, shoppingCartRecords[0].Quantity);
            Assert.Equal(2, shoppingCartRecords[1].ProductId);
            Assert.Equal(3, shoppingCartRecords[1].Quantity);
        }

        [Fact]
        public void ShouldUpdateQuantityOnAddIfAlreadyInCart()
        {
            var item = new ShoppingCartRecord()
            {
                ProductId = 33,
                Quantity = 1,
                DateCreated = DateTime.Now,
                CustomerId = Db.CustomerId
            };
            _repo.Add(item);
            var shoppingCartRecords = _repo.GetAll().ToList();
            Assert.Single(shoppingCartRecords);
            Assert.Equal(2, shoppingCartRecords[0].Quantity);
        }

        [Fact]
        public void ShouldDeleteOnAddIfQuantityEqualZero()
        {
            var item = new ShoppingCartRecord()
            {
                ProductId = 30,
                Quantity = -1,
                DateCreated = DateTime.Now,
                CustomerId = Db.CustomerId
            };
            _repo.Add(item);
            var shoppingCartRecords = _repo.GetAll().ToList();
            Assert.Equal(0, shoppingCartRecords.Count(x => x.ProductId == 34));
        }

        [Fact]
        public void ShouldDeleteOnAddIfQuantityLessThanZero()
        {
            var item = new ShoppingCartRecord()
            {
                ProductId = 33,
                Quantity = -10,
                DateCreated = DateTime.Now,
                CustomerId = Db.CustomerId
            };
            _repo.Add(item);
            Assert.Equal(0, _repo.Table.Count());
        }

        [Fact]
        public void ShouldUpdateQuantity()
        {
            var item = _repo.Find(1);
            item.Quantity = 5;
            item.DateCreated = DateTime.Now;
            _repo.Update(item);
            var shoppingCartRecords = _repo.GetAll().ToList();
            Assert.Single(shoppingCartRecords);
            Assert.Equal(5, shoppingCartRecords[0].Quantity);
        }

        [Fact]
        public void ShouldDeleteOnUpdateIfQuantityEqualsZero()
        {
            var item = _repo.Find(1);
            item.Quantity = 0;
            item.DateCreated = DateTime.Now;
            _repo.Update(item);
            var shoppingCartRecords = _repo.GetAll().ToList();
            Assert.Empty(shoppingCartRecords);
        }

        [Fact]
        public void ShouldDeleteOnUpdateIfQuantityLessThanZero()
        {
            var item = _repo.Find(1);
            item.Quantity = -10;
            item.DateCreated = DateTime.Now;
            _repo.Update(item);
            var shoppingCartRecords = _repo.GetAll().ToList();
            Assert.Empty(shoppingCartRecords);
        }

        [Fact]
        public void ShouldDeleteCartRecord()
        {
            var item = _repo.Find(1);
            _repo.Context.Entry(item).State = EntityState.Detached;
            _repo.Delete(item);
            Assert.Empty(_repo.GetAll());
        }

        [Fact]
        public void ShouldNotDeleteMissingCartRecord()
        {
            var item = _repo.Find(1);
            var recordToDelete = new ShoppingCartRecord {Id = 200, TimeStamp = item.TimeStamp};
            Assert.Throws<SpyStoreConcurrencyException>(() => _repo.Delete(recordToDelete));
        }

        [Fact]
        public void ShouldThrowWhenAddingToMuchQuantity()
        {
            _repo.Context.SaveChanges();
            var item = new ShoppingCartRecord()
            {
                ProductId = 2,
                Quantity = 500,
                DateCreated = DateTime.Now,
                CustomerId = 2
            };
            var ex = Assert.Throws<SpyStoreInvalidQuantityException>(() => _repo.Update(item));
            Assert.Equal("Can't add more product than available in stock", ex.Message);
        }

        [Fact]
        public void ShouldThrowWhenUpdatingTooMuchQuantity()
        {
            var item = _repo.Find(1);
            item.Quantity = 100;
            item.DateCreated = DateTime.Now;
            var ex = Assert.Throws<SpyStoreInvalidQuantityException>(() => _repo.Update(item));
            Assert.Equal("Can't add more product than available in stock", ex.Message);
        }
    }
}
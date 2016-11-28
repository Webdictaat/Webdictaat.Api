﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Models;

namespace Webdictaat.Api.Test
{
    public class BaseTest
    {
        protected Mock<IPageRepository> _pageRepoMock;
        protected Mock<IMenuRepository> _menuRepo;

        public BaseTest()
        {
            _pageRepoMock = new Mock<IPageRepository>();
            _menuRepo = new Mock<IMenuRepository>();
        }

    }
}

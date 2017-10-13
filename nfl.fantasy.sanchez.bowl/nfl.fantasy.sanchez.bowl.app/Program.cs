﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;

namespace nfl.fantasy.sanchez.bowl.app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureServices(services => services.AddAutofac())
                   .UseStartup<Startup>()
                .Build();
    }

    public class ProgramLogic : IProgramLogic{
        private IDomHelper _domHelper;
        private const string _leagueUrl = "http://fantasy.nfl.com/league/448915/team/";

        public ProgramLogic(IDomHelper domHelper){
            _domHelper = domHelper;
        }

        public async Task MainAsync(){
            var teamId = Teams.Team1.GetHashCode();
            await _domHelper.LoadPage($"{_leagueUrl}{teamId}");
        }
    }

    public interface IProgramLogic
    {
        Task MainAsync();
    }

    //TODO: configure team names dyamically
    public enum Teams : uint
    {
        NoTeam = 0,
        Team1 = 1,
        Team2 = 2,
        Team3 = 3,
        Team4 = 4,
        Team5 = 5,
        Team6 = 6,
        Team7 = 7,
        Team8 = 8,
        Team9 = 9,
        Team10 = 10,
        Team11 = 11,
        Team12 = 12
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace nfl.fantasy.sanchez.bowl.app.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<PlayInfo> PlayerInfos { get; set; }
        readonly IProgramLogic programLogic;

        public IndexModel(IProgramLogic programLogic){
            this.programLogic = programLogic;
        }

        public async Task OnGet()
        {
            var playerInfo = await programLogic.MainAsync();
            PlayerInfos = playerInfo;
        }
    }
}

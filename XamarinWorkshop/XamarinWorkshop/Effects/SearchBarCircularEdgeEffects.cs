using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinWorkshop.Effects
{
    public class SearchBarCircularEdgeEffects : RoutingEffect
    {
        public SearchBarCircularEdgeEffects() : base("XamarinWorkshop.SearchBarCircularEdgeEffects") { }

        public bool IsApplyToDroid { get; set; } = true;
    }
}

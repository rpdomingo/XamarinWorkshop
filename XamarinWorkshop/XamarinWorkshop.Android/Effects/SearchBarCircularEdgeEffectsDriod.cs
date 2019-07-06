using Android.Widget;
using Plugin.CurrentActivity;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinWorkshop.Effects;

[assembly: ResolutionGroupName("XamarinWorkshop")]
[assembly: ExportEffect(typeof(XamarinWorkshop.Droid.Effects.SearchBarCircularEdgeEffectsDriod), nameof(SearchBarCircularEdgeEffects))]
namespace XamarinWorkshop.Droid.Effects
{
    public class SearchBarCircularEdgeEffectsDriod : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control as SearchView;
                if (control == null) return;
                var effect = (SearchBarCircularEdgeEffects)Element.Effects.FirstOrDefault(e => e is SearchBarCircularEdgeEffects);

                if (effect != null)
                {
                    if (!effect.IsApplyToDroid) return;
                    control.SetBackgroundResource(Resource.Drawable.RoundEdge);

                    //search plate
                    int searchPlateId = control.Context.Resources.GetIdentifier("android:id/search_plate", null, null);
                    var searchPlateView = control.FindViewById(searchPlateId);

                    if (searchPlateView != null)
                        searchPlateView.SetBackgroundColor(Android.Graphics.Color.Transparent);

                    //search icon color
                    var searchIconId = control.Context.Resources.GetIdentifier("android:id/ic_shortcut_search.png", null, null);
                    var searchIconView = (ImageView)control.FindViewById(searchIconId);
                    var context = CrossCurrentActivity.Current.Activity;

                    if (searchIconView != null)
                    {
                        var drawable = context.GetDrawable(Resource.Drawable.searchw);
                        searchIconView.SetImageDrawable(drawable);
                    }

                    //hint color
                    var srcTextId = control.Context.Resources.GetIdentifier("android:id/search_src_text", null, null);
                    var searchSourceText = (EditText)control.FindViewById(srcTextId);

                    if (searchSourceText != null)
                    {
                        searchSourceText.SetHintTextColor(Xamarin.Forms.Color.White.ToAndroid());
                        searchSourceText.SetTextColor(Xamarin.Forms.Color.White.ToAndroid());
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
           
        }
    }
}
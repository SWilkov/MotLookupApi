using MotLookupApi.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotLookupApi.Framework.Extensions
{
  public static class MotTestExtensions
  {
    /// <summary>
    /// Returns MOT Tests including the latest relevant test be it failed or passed
    /// this method is used to stop multiple tests being returned on the same completed date
    /// </summary>
    /// <param name="tests"></param>
    /// <returns></returns>
    public static IEnumerable<MotTest> LatestRelevant(this IEnumerable<MotTest> tests)
    {
      if (tests == null)
        throw new ArgumentNullException(nameof(tests));
      var relevant = new List<MotTest>();

      var grouped = tests.GroupBy(x => x.CompletedDate.Year);
      foreach(var grp in grouped)
      {
        if (grp.Count() > 1)
        {
          var ordered = grp.OrderByDescending(x => x.CompletedDate);
          //Add the latest test be it failed or passed
          relevant.Add(ordered.FirstOrDefault());
        }
        else
          relevant.Add(grp.FirstOrDefault());
      }

      return relevant;
    }
  }
}

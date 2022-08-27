using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApi.ApiModel;

namespace UniversityApiTests.FakeData
{
    public class UniversityTestData
    {
        public static List<UniversityApiModel> GetData()
        {
            return new List<UniversityApiModel>
            {
                new UniversityApiModel{
                        Id = 1,
                        Name="HNBGU",
                        Location="Srinagar",
                        AffiliatedCollege="Hnbgu",
                        EstablishedYear=2022
                },
                new UniversityApiModel{
                    Id = 2,
                        Name="HNBG",
                        Location="Srinagar",
                        AffiliatedCollege="Hnbgu",
                        EstablishedYear=2022
                },
                new UniversityApiModel{
                        Id = 3,
                        Name="HNB",
                        Location="Srinagar",
                        AffiliatedCollege="Hnbgu",
                        EstablishedYear=2020
                }
            };
        }
    }
}

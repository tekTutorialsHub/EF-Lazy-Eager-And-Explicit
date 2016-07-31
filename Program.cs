using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


using EFQueryEntityFramework.Models;

namespace EFQueryEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {

            //LazyLoading();
            //EagerLoading();
            ExplicitLoading();

        }


        static void LazyLoading()
        {
            using (AdventureWorks db = new AdventureWorks())
            {

                var product = (from p in db.Products
                               where p.ProductID == 814
                               select p).ToList();

                foreach (var p in product)
                {
                    //a separate query is fired to retrieve the Proudct Model Name
                    Console.WriteLine("{0} {1} {2}", p.ProductID, p.Name, p.ProductModel.Name);
                }
            }


        }


        static void EagerLoading()
        {
            using (AdventureWorks db = new AdventureWorks())
            {
                //Disable Lazy Loading
                db.Configuration.LazyLoadingEnabled = false;
                //Log Database
                db.Database.Log = Console.Write;
                var product = (from p in db.Products
                               .Include("ProductModel")
                               where p.ProductID == 814
                               select p).ToList();

                foreach (var p in product)
                {
                    Console.WriteLine("{0} {1} {2}", p.ProductID, p.Name, p.ProductModel.Name);
                }

                Console.WriteLine("Press any key to Continue");
                Console.ReadKey();

            }


            //Using Lambda Expression
            using (AdventureWorks db = new AdventureWorks())
            {
                //Disable Lazy Loading
                db.Configuration.LazyLoadingEnabled = false;
                //Log Database
                db.Database.Log = Console.Write;

                var product = (from p in db.Products
                               .Include(p => p.ProductModel)
                               where p.ProductID == 814
                               select p).ToList();

                foreach (var p in product)
                {
                    Console.WriteLine("{0} {1} {2}", p.ProductID, p.Name, p.ProductModel.Name);
                }

                Console.WriteLine("Press any key to Continue");
                Console.ReadKey();

            }


            
            using (AdventureWorks db = new AdventureWorks())
            {
                //Disable Lazy Loading
                db.Configuration.LazyLoadingEnabled = false;
                //Log Database
                db.Database.Log = Console.Write;

                var product = (from p in db.Products
                               .Include(p => p.ProductModel)
                               .Include(p => p.ProductVendors)
                               where p.ProductID == 931
                               select p).ToList();

                foreach (var p in product)
                {
                    Console.WriteLine("{0} {1} {2}", p.ProductID, p.Name, p.ProductModel.Name);
                }

                Console.WriteLine("Press any key to Continue");
                Console.ReadKey();
            }



            
            using (AdventureWorks db = new AdventureWorks())
            {
                //Disable Lazy Loading
                db.Configuration.LazyLoadingEnabled = false;
                //Log Database
                db.Database.Log = Console.Write;  

                var product = (from p in db.SalesPersons
                               .Include(p => p.Employee.Person)
                               select p).ToList();

                Console.WriteLine("Press any key to Continue");
                Console.ReadKey();
            }



        }


        static void ExplicitLoading()
        {

            //using (AdventureWorks db = new AdventureWorks())
            //{
            //    //Disable Lazy Loading
            //    db.Configuration.LazyLoadingEnabled = false;
            //    //Log Database
            //    db.Database.Log = Console.Write;

            //    //List of Products querries here.
            //    var product = (from p in db.Products
            //                   orderby p.ProductID descending
            //                   select p).Take(5).ToList();


            //    Console.ReadKey();

            //    foreach (var p in product)
            //    {
            //        //Product model is retrived here
            //        db.Entry(p).Reference(m => m.ProductModel).Load();
            //        Console.WriteLine("{0} {1}  Product Model => {2}", p.ProductID, p.Name, ( p.ProductModel==null) ? "" : p.ProductModel.Name );

            //        Console.ReadKey();
            //    }

            //    Console.WriteLine("Press Any Key");
            //    Console.ReadKey();        
            //}




            using (AdventureWorks db = new AdventureWorks())
            {
                //Disable Lazy Loading
                db.Configuration.LazyLoadingEnabled = false;
                //Log Database
                db.Database.Log = Console.Write;

                //List of Products querries here.
                var productModel = (from pm in db.ProductModels
                                    select pm).Take(5).ToList();
                
                Console.ReadKey();

                foreach (var pm in productModel)
                {
                    Console.WriteLine("Model {0} ", pm.Name);
                    db.Entry(pm).Collection(p => p.Products).Load();

                    foreach (var p in pm.Products)
                    {
                        Console.WriteLine("          {0}", p.Name);

                    }

                    Console.ReadKey();
                }

                Console.WriteLine("Press Any Key");
                Console.ReadKey();
            }






            using (AdventureWorks db = new AdventureWorks())
            {
                //Disable Lazy Loading
                db.Configuration.LazyLoadingEnabled = false;
                //Log Database
                db.Database.Log = Console.Write;

                //List of Products querries here.
                var productModel = (from pm in db.ProductModels
                                    select pm).Take(5).ToList();

                Console.ReadKey();

                foreach (var pm in productModel)
                {
                    Console.WriteLine("Model {0} ", pm.Name);
                    db.Entry(pm).Collection(p => p.Products).Query().Where(p => p.ListPrice > 10).Load();

                    foreach (var p in pm.Products)
                    {
                        Console.WriteLine("          {0}", p.Name);

                    }

                    Console.ReadKey();
                }

                Console.WriteLine("Press Any Key");
                Console.ReadKey();
            }



        }

    }
}




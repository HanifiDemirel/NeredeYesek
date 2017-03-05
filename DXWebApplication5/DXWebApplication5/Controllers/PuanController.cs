using DXWebApplication5.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXWebApplication5.Controllers
{
    public class PuanController : Controller
    {
        // GET: Puan
        private NeredeYesekDBEntities3 db = new NeredeYesekDBEntities3();

        public ActionResult Index()
        {
            foreach (var p in db.Puans)
            {
                db.Puans.Remove(p);
            }
            db.SaveChanges();
            int pid = 1;
            foreach (var res in db.Restorans)
            {
                foreach (var uye in db.Uyelers)
                {

                    Puan puan = new Puan();
                    puan.PID = pid;
                    puan.RID = res.RID;
                    puan.UID = uye.UID;
                    puan.GID = 1;
                    puan.Puan1 = 0;
                    db.Puans.Add(puan);
                    pid++;
                }

            }
            db.SaveChanges();
            toExcel();
            return View();

        }
        public ActionResult toExcel()
        {

            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Uyeler\\Musteriler");
            dt.Columns.Add(dc);
            int i = 0;
            foreach(var p in db.Restorans)
            {
                string rName = db.Restorans.Find(p.RID).RestoranAdi;
                dt.Columns.Add(new DataColumn(rName));
                i++;
            }
            foreach (var p in db.Puans)
            {
                string uName = db.Uyelers.Find(p.UID).Isim+" "+ db.Uyelers.Find(p.UID).SoyIsim;
                DataRow dr;
                dr = dt.NewRow();
                for(int c = 1; c <= i; c++)
                {
                    dr[c] = 0 ;
                }
                dr[0] = uName;
                dt.Rows.Add(dr);
            }


            string dosyaAdi = "xnxx";
            var grid = new GridView();
            grid.DataSource = dt;
            grid.DataBind();

            Response.ClearContent();
            Response.Charset = "utf-8";
            Response.AddHeader("content-disposition", "attachment; filename=" + dosyaAdi + ".xls");

            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();
            return View();
        }
    }
}
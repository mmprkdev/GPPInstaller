using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPPInstaller
{
    // NOTE: For lack of a better idea, this class is being used to store obsolete code
    // that did some noteworthy things.
    class Notes
    {
        
        // NOTE: In this example a closure was used to pass extra information through
        // an event handler.
        //
        //WebClient webclient = new WebClient();
        //Uri uri = new Uri(downloadAddress);
        //
        //webclient.DownloadFileCompleted += DownloadFileComplete(modPack[i], form1);
        //webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
        //await Task.Factory.StartNew(() => webclient.DownloadFileAsync(uri, downloadDest));
        //
        //public static AsyncCompletedEventHandler DownloadFileComplete(Mod mod, Form1 form1)
        //{
        //    // NOTE: Closure example
        //    Action<object, AsyncCompletedEventArgs> action = (sender, e) =>
        //    {
        //        switch (mod.ModName)
        //        {
        //            case "Kopernicus":
        //                mod.State = "Downloaded";
        //                break;
        //            case "GPP":
        //                mod.State = "Downloaded";
        //                break;
        //            case "GPP_Textures":
        //                mod.State = "Downloaded";
        //                break;
        //            default:
        //                break;
        //        }

        //        form1.ProgressBar1Step();

        //        modIndex++;

        //        if (modIndex >= modPack.Count)
        //        {
        //            form1.ProgressLabelUpdate("All Downloads complete.");

        //            Utility util = new Utility(form1);
        //            util.ExtractFiles();
        //        }
        //    };
        //    return new AsyncCompletedEventHandler(action);
        //}
    }
}

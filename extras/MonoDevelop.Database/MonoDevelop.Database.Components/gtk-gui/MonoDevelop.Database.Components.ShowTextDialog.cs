// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace MonoDevelop.Database.Components {
    
    public partial class ShowTextDialog {
        
        private Gtk.VBox vboxContent;
        
        private Gtk.Button button462;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget MonoDevelop.Database.Components.ShowTextDialog
            this.Name = "MonoDevelop.Database.Components.ShowTextDialog";
            this.Title = AddinCatalog.GetString("Text");
            this.TypeHint = ((Gdk.WindowTypeHint)(1));
            this.WindowPosition = ((Gtk.WindowPosition)(1));
            this.SkipTaskbarHint = true;
            // Internal child MonoDevelop.Database.Components.ShowTextDialog.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Name = "vbox";
            w1.BorderWidth = ((uint)(2));
            // Container child vbox.Gtk.Box+BoxChild
            this.vboxContent = new Gtk.VBox();
            this.vboxContent.Name = "vboxContent";
            this.vboxContent.Spacing = 6;
            this.vboxContent.BorderWidth = ((uint)(6));
            w1.Add(this.vboxContent);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(w1[this.vboxContent]));
            w2.Position = 0;
            // Internal child MonoDevelop.Database.Components.ShowTextDialog.ActionArea
            Gtk.HButtonBox w3 = this.ActionArea;
            w3.Name = "GtkDialog_ActionArea";
            w3.LayoutStyle = ((Gtk.ButtonBoxStyle)(4));
            // Container child GtkDialog_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.button462 = new Gtk.Button();
            this.button462.CanFocus = true;
            this.button462.Name = "button462";
            this.button462.UseUnderline = true;
            this.button462.Label = AddinCatalog.GetString("button462");
            this.AddActionWidget(this.button462, 0);
            Gtk.ButtonBox.ButtonBoxChild w4 = ((Gtk.ButtonBox.ButtonBoxChild)(w3[this.button462]));
            w4.Expand = false;
            w4.Fill = false;
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 400;
            this.DefaultHeight = 300;
            this.Show();
        }
    }
}
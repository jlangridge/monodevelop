// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace MonoDevelop.Ide.Gui.OptionPanels {
    
    
    internal partial class BuildMessagePanelWidget {
        
        private Gtk.VBox vbox1;
        
        private Gtk.Table table5;
        
        private Gtk.ComboBox comboboxJumpToFirst;
        
        private Gtk.Label label6;
        
        private Gtk.Frame frame1;
        
        private Gtk.Alignment GtkAlignment;
        
        private Gtk.Table table2;
        
        private Gtk.ComboBox comboboxBuildResultsAfter;
        
        private Gtk.ComboBox comboboxBuildResultsDuring;
        
        private Gtk.Label label1;
        
        private Gtk.Label label2;
        
        private Gtk.Label GtkLabel;
        
        private Gtk.Frame frame2;
        
        private Gtk.Alignment GtkAlignment1;
        
        private Gtk.Table table3;
        
        private Gtk.ComboBox comboboxErrorPadAfter;
        
        private Gtk.ComboBox comboboxErrorPadDuring;
        
        private Gtk.Label label3;
        
        private Gtk.Label label4;
        
        private Gtk.Label GtkLabel1;
        
        private Gtk.Frame frame3;
        
        private Gtk.Alignment GtkAlignment2;
        
        private Gtk.Table table4;
        
        private Gtk.ComboBox comboboxMessageBubbles;
        
        private Gtk.Label label5;
        
        private Gtk.Label GtkLabel2;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget MonoDevelop.Ide.Gui.OptionPanels.BuildMessagePanelWidget
            Stetic.BinContainer.Attach(this);
            this.Name = "MonoDevelop.Ide.Gui.OptionPanels.BuildMessagePanelWidget";
            // Container child MonoDevelop.Ide.Gui.OptionPanels.BuildMessagePanelWidget.Gtk.Container+ContainerChild
            this.vbox1 = new Gtk.VBox();
            this.vbox1.Name = "vbox1";
            this.vbox1.Spacing = 6;
            // Container child vbox1.Gtk.Box+BoxChild
            this.table5 = new Gtk.Table(((uint)(1)), ((uint)(2)), false);
            this.table5.Name = "table5";
            this.table5.RowSpacing = ((uint)(6));
            this.table5.ColumnSpacing = ((uint)(6));
            // Container child table5.Gtk.Table+TableChild
            this.comboboxJumpToFirst = Gtk.ComboBox.NewText();
            this.comboboxJumpToFirst.Name = "comboboxJumpToFirst";
            this.table5.Add(this.comboboxJumpToFirst);
            Gtk.Table.TableChild w1 = ((Gtk.Table.TableChild)(this.table5[this.comboboxJumpToFirst]));
            w1.LeftAttach = ((uint)(1));
            w1.RightAttach = ((uint)(2));
            w1.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table5.Gtk.Table+TableChild
            this.label6 = new Gtk.Label();
            this.label6.Name = "label6";
            this.label6.Xalign = 1F;
            this.label6.LabelProp = Mono.Unix.Catalog.GetString("Jump to first error or warning:");
            this.table5.Add(this.label6);
            Gtk.Table.TableChild w2 = ((Gtk.Table.TableChild)(this.table5[this.label6]));
            w2.XOptions = ((Gtk.AttachOptions)(4));
            w2.YOptions = ((Gtk.AttachOptions)(4));
            this.vbox1.Add(this.table5);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox1[this.table5]));
            w3.Position = 0;
            w3.Expand = false;
            w3.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.frame1 = new Gtk.Frame();
            this.frame1.Name = "frame1";
            this.frame1.ShadowType = ((Gtk.ShadowType)(0));
            // Container child frame1.Gtk.Container+ContainerChild
            this.GtkAlignment = new Gtk.Alignment(0F, 0F, 1F, 1F);
            this.GtkAlignment.Name = "GtkAlignment";
            this.GtkAlignment.LeftPadding = ((uint)(12));
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            this.table2 = new Gtk.Table(((uint)(2)), ((uint)(2)), false);
            this.table2.Name = "table2";
            this.table2.RowSpacing = ((uint)(6));
            this.table2.ColumnSpacing = ((uint)(6));
            // Container child table2.Gtk.Table+TableChild
            this.comboboxBuildResultsAfter = Gtk.ComboBox.NewText();
            this.comboboxBuildResultsAfter.Name = "comboboxBuildResultsAfter";
            this.table2.Add(this.comboboxBuildResultsAfter);
            Gtk.Table.TableChild w4 = ((Gtk.Table.TableChild)(this.table2[this.comboboxBuildResultsAfter]));
            w4.TopAttach = ((uint)(1));
            w4.BottomAttach = ((uint)(2));
            w4.LeftAttach = ((uint)(1));
            w4.RightAttach = ((uint)(2));
            w4.XOptions = ((Gtk.AttachOptions)(4));
            w4.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table2.Gtk.Table+TableChild
            this.comboboxBuildResultsDuring = Gtk.ComboBox.NewText();
            this.comboboxBuildResultsDuring.Name = "comboboxBuildResultsDuring";
            this.table2.Add(this.comboboxBuildResultsDuring);
            Gtk.Table.TableChild w5 = ((Gtk.Table.TableChild)(this.table2[this.comboboxBuildResultsDuring]));
            w5.LeftAttach = ((uint)(1));
            w5.RightAttach = ((uint)(2));
            w5.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table2.Gtk.Table+TableChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.Xalign = 1F;
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("Show during build:");
            this.table2.Add(this.label1);
            Gtk.Table.TableChild w6 = ((Gtk.Table.TableChild)(this.table2[this.label1]));
            w6.XOptions = ((Gtk.AttachOptions)(4));
            w6.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table2.Gtk.Table+TableChild
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.Xalign = 1F;
            this.label2.LabelProp = Mono.Unix.Catalog.GetString("Show after build:");
            this.table2.Add(this.label2);
            Gtk.Table.TableChild w7 = ((Gtk.Table.TableChild)(this.table2[this.label2]));
            w7.TopAttach = ((uint)(1));
            w7.BottomAttach = ((uint)(2));
            w7.XOptions = ((Gtk.AttachOptions)(4));
            w7.YOptions = ((Gtk.AttachOptions)(4));
            this.GtkAlignment.Add(this.table2);
            this.frame1.Add(this.GtkAlignment);
            this.GtkLabel = new Gtk.Label();
            this.GtkLabel.Name = "GtkLabel";
            this.GtkLabel.LabelProp = Mono.Unix.Catalog.GetString("<b>Build Results Window</b>");
            this.GtkLabel.UseMarkup = true;
            this.frame1.LabelWidget = this.GtkLabel;
            this.vbox1.Add(this.frame1);
            Gtk.Box.BoxChild w10 = ((Gtk.Box.BoxChild)(this.vbox1[this.frame1]));
            w10.Position = 1;
            w10.Expand = false;
            w10.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.frame2 = new Gtk.Frame();
            this.frame2.Name = "frame2";
            this.frame2.ShadowType = ((Gtk.ShadowType)(0));
            // Container child frame2.Gtk.Container+ContainerChild
            this.GtkAlignment1 = new Gtk.Alignment(0F, 0F, 1F, 1F);
            this.GtkAlignment1.Name = "GtkAlignment1";
            this.GtkAlignment1.LeftPadding = ((uint)(12));
            // Container child GtkAlignment1.Gtk.Container+ContainerChild
            this.table3 = new Gtk.Table(((uint)(2)), ((uint)(2)), false);
            this.table3.Name = "table3";
            this.table3.RowSpacing = ((uint)(6));
            this.table3.ColumnSpacing = ((uint)(6));
            // Container child table3.Gtk.Table+TableChild
            this.comboboxErrorPadAfter = Gtk.ComboBox.NewText();
            this.comboboxErrorPadAfter.Name = "comboboxErrorPadAfter";
            this.table3.Add(this.comboboxErrorPadAfter);
            Gtk.Table.TableChild w11 = ((Gtk.Table.TableChild)(this.table3[this.comboboxErrorPadAfter]));
            w11.TopAttach = ((uint)(1));
            w11.BottomAttach = ((uint)(2));
            w11.LeftAttach = ((uint)(1));
            w11.RightAttach = ((uint)(2));
            w11.XOptions = ((Gtk.AttachOptions)(4));
            w11.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table3.Gtk.Table+TableChild
            this.comboboxErrorPadDuring = Gtk.ComboBox.NewText();
            this.comboboxErrorPadDuring.Name = "comboboxErrorPadDuring";
            this.table3.Add(this.comboboxErrorPadDuring);
            Gtk.Table.TableChild w12 = ((Gtk.Table.TableChild)(this.table3[this.comboboxErrorPadDuring]));
            w12.LeftAttach = ((uint)(1));
            w12.RightAttach = ((uint)(2));
            w12.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table3.Gtk.Table+TableChild
            this.label3 = new Gtk.Label();
            this.label3.Name = "label3";
            this.label3.Xalign = 1F;
            this.label3.LabelProp = Mono.Unix.Catalog.GetString("Show after build:");
            this.table3.Add(this.label3);
            Gtk.Table.TableChild w13 = ((Gtk.Table.TableChild)(this.table3[this.label3]));
            w13.TopAttach = ((uint)(1));
            w13.BottomAttach = ((uint)(2));
            w13.XOptions = ((Gtk.AttachOptions)(4));
            w13.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table3.Gtk.Table+TableChild
            this.label4 = new Gtk.Label();
            this.label4.Name = "label4";
            this.label4.Xalign = 1F;
            this.label4.LabelProp = Mono.Unix.Catalog.GetString("Show during build:");
            this.table3.Add(this.label4);
            Gtk.Table.TableChild w14 = ((Gtk.Table.TableChild)(this.table3[this.label4]));
            w14.XOptions = ((Gtk.AttachOptions)(4));
            w14.YOptions = ((Gtk.AttachOptions)(4));
            this.GtkAlignment1.Add(this.table3);
            this.frame2.Add(this.GtkAlignment1);
            this.GtkLabel1 = new Gtk.Label();
            this.GtkLabel1.Name = "GtkLabel1";
            this.GtkLabel1.LabelProp = Mono.Unix.Catalog.GetString("<b>Errors and Warnings Pad</b>");
            this.GtkLabel1.UseMarkup = true;
            this.frame2.LabelWidget = this.GtkLabel1;
            this.vbox1.Add(this.frame2);
            Gtk.Box.BoxChild w17 = ((Gtk.Box.BoxChild)(this.vbox1[this.frame2]));
            w17.Position = 2;
            w17.Expand = false;
            w17.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.frame3 = new Gtk.Frame();
            this.frame3.Name = "frame3";
            this.frame3.ShadowType = ((Gtk.ShadowType)(0));
            // Container child frame3.Gtk.Container+ContainerChild
            this.GtkAlignment2 = new Gtk.Alignment(0F, 0F, 1F, 1F);
            this.GtkAlignment2.Name = "GtkAlignment2";
            this.GtkAlignment2.LeftPadding = ((uint)(12));
            // Container child GtkAlignment2.Gtk.Container+ContainerChild
            this.table4 = new Gtk.Table(((uint)(1)), ((uint)(2)), false);
            this.table4.Name = "table4";
            this.table4.RowSpacing = ((uint)(6));
            this.table4.ColumnSpacing = ((uint)(6));
            // Container child table4.Gtk.Table+TableChild
            this.comboboxMessageBubbles = Gtk.ComboBox.NewText();
            this.comboboxMessageBubbles.Name = "comboboxMessageBubbles";
            this.table4.Add(this.comboboxMessageBubbles);
            Gtk.Table.TableChild w18 = ((Gtk.Table.TableChild)(this.table4[this.comboboxMessageBubbles]));
            w18.LeftAttach = ((uint)(1));
            w18.RightAttach = ((uint)(2));
            w18.YOptions = ((Gtk.AttachOptions)(4));
            // Container child table4.Gtk.Table+TableChild
            this.label5 = new Gtk.Label();
            this.label5.Name = "label5";
            this.label5.Xalign = 1F;
            this.label5.LabelProp = Mono.Unix.Catalog.GetString("Show during build:");
            this.table4.Add(this.label5);
            Gtk.Table.TableChild w19 = ((Gtk.Table.TableChild)(this.table4[this.label5]));
            w19.XOptions = ((Gtk.AttachOptions)(4));
            w19.YOptions = ((Gtk.AttachOptions)(4));
            this.GtkAlignment2.Add(this.table4);
            this.frame3.Add(this.GtkAlignment2);
            this.GtkLabel2 = new Gtk.Label();
            this.GtkLabel2.Name = "GtkLabel2";
            this.GtkLabel2.LabelProp = Mono.Unix.Catalog.GetString("<b>Message Bubbles</b>");
            this.GtkLabel2.UseMarkup = true;
            this.frame3.LabelWidget = this.GtkLabel2;
            this.vbox1.Add(this.frame3);
            Gtk.Box.BoxChild w22 = ((Gtk.Box.BoxChild)(this.vbox1[this.frame3]));
            w22.Position = 3;
            w22.Expand = false;
            w22.Fill = false;
            this.Add(this.vbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Hide();
        }
    }
}
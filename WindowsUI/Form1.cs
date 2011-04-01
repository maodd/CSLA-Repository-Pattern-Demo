using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLibrary;
using Csla.Core;
using Order = BusinessLibrary.TweekedCsla.Order;

namespace WindowsUI
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        this.cslaActionExtender1.ResetActionBehaviors(BusinessLibrary.TweekedCsla.Order.NewOrder());
//      this.cslaActionExtender1.ResetActionBehaviors(new Order2());
    }

      /// <summary>
      /// NO code needed here, save action is triggered by csla Action extendeder by 
      /// setting the default save/cancel button for datasource. 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
    private void SaveButton_Click(object sender, EventArgs e)
    {
//        var dataSource = this.orderBindingSource.DataSource;
//
//         Second time save fail, how to refresh datasource? or just close the form and reload it?
//        var savedOrder = ((ISavable)dataSource).Save();
//          this.orderBindingSource.DataSource = savedOrder;
//          this.cslaActionExtender1.ResetActionBehaviors(savedOrder);

        // TODO: how to catch save exception in CslaActionExtender?
    }

   

 
  }
}

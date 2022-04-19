Imports System.Drawing
Partial Class Admin_Manage_Section
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    
    End Sub
    
    Protected Sub Button_Search_Click(sender As Object, e As EventArgs) Handles Button_Search.Click
        GridView_List.DataBind()
    End Sub
    
    Protected Sub Button_AddNew_Click(sender As Object, e As EventArgs) Handles Button_AddNew.Click
        Switch_To_Insert()
    End Sub
    Protected Sub Button_Edit_Click(sender As Object, e As EventArgs)
        Dim oSender As Button = sender
        Switch_To_Edit()
        Load_Data(oSender.CommandArgument)
    End Sub
    Protected Sub Button_SaveNew_Click(sender As Object, e As EventArgs) Handles Button_SaveNew.Click
        Save_New()
    End Sub
    Protected Sub Button_SaveChanges_Click(sender As Object, e As EventArgs) Handles Button_SaveChanges.Click
        Save_Changes()
    End Sub
    Protected Sub Button_Cancel_Click(sender As Object, e As EventArgs) Handles Button_Cancel.Click
        Switch_To_List()
    End Sub
    Protected Sub Button_Delete_Click(sender As Object, e As EventArgs) Handles Button_Delete.Click
        Delete_Data()
    End Sub
    Sub Switch_To_List()
        Panel_list.Visible = True
        Panel_Edit.Visible = False
        Button_AddNew.Visible = True
    End Sub
    Sub Switch_To_Insert()
        Panel_list.Visible = False
        Panel_Edit.Visible = True
        Button_AddNew.Visible = False
        Label_EditTitle.Text = "افزودن"
        Button_SaveNew.Visible = True
        Button_SaveChanges.Visible = False
        Button_Cancel.Visible = True
        Button_Delete.Visible = False
        Label_Message.Text = ""
        '-> remove old data 
        TextBox_Str_SectName.Text = ""
        TextBox_int_ShowOrder.Text = ""
    End Sub
    Sub Switch_To_Edit()
        Panel_list.Visible = False
        Panel_Edit.Visible = True
        Button_AddNew.Visible = False
        Label_EditTitle.Text = "ویرایش"
        Button_SaveNew.Visible = False
        Button_SaveChanges.Visible = True
        Button_Cancel.Visible = True
        Button_Delete.Visible = True
    End Sub
    Function Validate_Inputs() As Boolean
        Dim RV As Boolean = True
        Label_Message.ForeColor = Color.Red
        If TextBox_Str_SectName.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا نام بخش را وارد کنید."
            TextBox_Str_SectName.Focus()
            Return False
        End If

        If TextBox_int_ShowOrder.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا ترتیب نمایش را وارد کنید."
            TextBox_int_ShowOrder.Focus()
            Return False
        End If

        Return RV
    End Function
    Sub Save_New()
        If Validate_Inputs() Then
            Dim SqlStr As String = "INSERT INTO dbo.TBL_Sects (Str_SectName,int_ShowOrder) VALUES (@Str_SectName,@int_ShowOrder) "
            Dim DAL as New AZDBL 
            DAL.SqlStr = SqlStr
            DAL.Params.Clear()
            DAL.Params.Add("@Str_SectName", TextBox_Str_SectName.Text) 
            DAL.Params.Add("@int_ShowOrder", TextBox_int_ShowOrder.Text) 
            Try 
                DAL.ExecuteNonQuery()
                Switch_To_List()
                GridView_List.DataBind()
                Label_ActionMessage.Text = "اطلاعات با موفقیت ثبت شد."
            Catch ex As Exception
                Label_Message.Text = "ثبت اطلاعات در بانک اطلاعانی با اشکال مواجه شد!" + ex.Message
            End Try
            DAL.Dispose() 
        End If
    End Sub
    Sub Load_Data(ID As Integer)
        Dim SqlStr As String = "SELECT * FROM dbo.TBL_Sects WHERE SectID=@SectID" 
        Dim DAL As New AZDBL
        DAL.SqlStr = SqlStr
        DAL.Params.Clear()
        DAL.Params.Add("@SectID", ID)
        Dim DT As New System.Data.DataTable
        Try
            DT = DAL.ExecuteDatatable()
        Catch ex As Exception
            
        End Try
        DAL.Dispose()
        
        For Each row As System.Data.DataRow In DT.Rows
            Label_SectID.Text = row.Item("SectID")
            TextBox_Str_SectName.Text = row.Item("Str_SectName").ToString()
            TextBox_int_ShowOrder.Text = row.Item("int_ShowOrder").ToString()
        Next
        DT.Dispose()
    End Sub
    Sub Save_Changes()
        If Validate_Inputs() Then
            Dim SqlStr As String = "UPDATE dbo.TBL_Sects SET  Str_SectName=@Str_SectName , int_ShowOrder=@int_ShowOrder WHERE SectID=@SectID "
            Dim DAL As New AZDBL
            DAL.SqlStr = SqlStr
            DAL.Params.Clear()
            DAL.Params.Add("@SectID", Label_SectID.Text)
            DAL.Params.Add("@Str_SectName", TextBox_Str_SectName.Text)
            DAL.Params.Add("@int_ShowOrder", TextBox_int_ShowOrder.Text)
            Try
                DAL.ExecuteNonQuery()
                Switch_To_List()
                GridView_List.DataBind()
                Label_ActionMessage.Text = "اطلاعات با موفقیت به روز رسانی شد."
            Catch ex As Exception
                Label_Message.Text = "ثبت تغییرات در بانک اطلاعانی با اشکال مواجه شد!" + ex.Message
                
            End Try
            DAL.Dispose()
            
        End If
    End Sub
    Sub Delete_Data()
        Dim SqlStr As String = "DELETE FROM dbo.TBL_Sects WHERE SectID=@SectID" 
        
        Dim DAL As New AZDBL
        DAL.SqlStr = SqlStr
        DAL.Params.Clear()
        DAL.Params.Add("@SectID", Label_SectID.Text)
        Try
            DAL.ExecuteNonQuery()
            Switch_To_List()
            GridView_List.DataBind()
            Label_ActionMessage.Text = "اطلاعات با موفقیت حذف شد."
        Catch ex As Exception
            Label_Message.Text = "حذف اطلاعات از بانک اطلاعانی با اشکال مواجه شد!" + ex.Message
        End Try
        DAL.Dispose()
    End Sub
End Class

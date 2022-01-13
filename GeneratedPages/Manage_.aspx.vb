Imports System.Drawing
Partial Class Admin_Manage_
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
        TextBox_UserName.Text = ""
        TextBox_Password.Text = ""
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
        If TextBox_UserName.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا UserName را وارد کنید."
            TextBox_UserName.Focus()
            Return False
        End If

        If TextBox_Password.Text = "" Then
            'RV = False
            Label_Message.Text = "لطفا Password را وارد کنید."
            TextBox_Password.Focus()
            Return False
        End If

        Return RV
    End Function
    Sub Save_New()
        If Validate_Inputs() Then
            Dim SqlStr As String = "INSERT INTO dbo.Tbl_Users (UserName,Password) VALUES (@UserName,@Password) "
            Dim DAL as New AZDBL 
            DAL.SqlStr = SqlStr
            DAL.Params.Clear()
            DAL.Params.Add("@UserName", TextBox_UserName.Text) 
            DAL.Params.Add("@Password", TextBox_Password.Text) 
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
        Dim SqlStr As String = "SELECT * FROM dbo.Tbl_Users WHERE id=@id" 
        Dim DAL As New AZDBL
        DAL.SqlStr = SqlStr
        DAL.Params.Clear()
        DAL.Params.Add("@id", ID)
        Dim DT As New System.Data.DataTable
        Try
            DT = DAL.ExecuteDatatable()
        Catch ex As Exception
            
        End Try
        DAL.Dispose()
        
        For Each row As System.Data.DataRow In DT.Rows
            Label_id.Text = row.Item("id")
            TextBox_UserName.Text = row.Item("UserName").ToString()
            TextBox_Password.Text = row.Item("Password").ToString()
        Next
        DT.Dispose()
    End Sub
    Sub Save_Changes()
        If Validate_Inputs() Then
            Dim SqlStr As String = "UPDATE dbo.Tbl_Users SET  UserName=@UserName , Password=@Password WHERE id=@id "
            Dim DAL As New AZDBL
            DAL.SqlStr = SqlStr
            DAL.Params.Clear()
            DAL.Params.Add("@id", Label_id.Text)
            DAL.Params.Add("@UserName", TextBox_UserName.Text)
            DAL.Params.Add("@Password", TextBox_Password.Text)
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
        Dim SqlStr As String = "DELETE FROM dbo.Tbl_Users WHERE id=@id" 
        
        Dim DAL As New AZDBL
        DAL.SqlStr = SqlStr
        DAL.Params.Clear()
        DAL.Params.Add("@id", Label_id.Text)
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

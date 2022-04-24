Imports Microsoft.VisualBasic

Public Module App
    Public Function CnnStr() As String
        Return System.Configuration.ConfigurationManager.ConnectionStrings("CnnStr").ConnectionString.ToString
    End Function
End Module

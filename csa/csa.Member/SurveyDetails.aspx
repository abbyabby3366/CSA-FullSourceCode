<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SurveyDetails.aspx.cs" Inherits="csa.Member.SurveyDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
        <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2.min.css") %>" />
    <link rel="stylesheet" href="<%=Page.ResolveUrl("~/assets/libs/select2/css/select2-bootstrap-5-theme.min.css") %>" />
    <style>
        .sectiontitle {
            font-weight: 800;
            border-bottom: solid 1px silver;
            font-size: medium;
            width: 100%;
            margin-bottom: 10px;
            padding-top: 5px;
            display: block;
            color: gray;
        }
        .select2-container .select2-selection--multiple .select2-selection__choice {
            background-color: unset;
        }

        .select2-container--bootstrap-5 .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            font-size: unset;
        }
   
        .no-border {
            border: none;        /* Menghapus border */
            background: none;   /* Menghapus background (opsional) */
            outline: none;      /* Menghapus outline saat difokuskan */
            /* Tambahkan gaya lain sesuai kebutuhan */
        }

        .readonly-select {
            pointer-events: none;       /* Prevents clicking */
        }

        .select2-selection {
            border: none !important;
            background-color: white !important;
        }
        .select2-selection .select2-selection--multiple {
            border: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%@ Register TagPrefix="uc" TagName="SurveyYabam" Src="~/Uc/SurveyYabam.ascx" %>
    <uc:SurveyYabam ID="SurveyYabam" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type='text/javascript' src="<%=Page.ResolveUrl("~/assets/libs/select2/js/select2.min.js") %>"></script>
    <script>
        var vm

        $('[multiple="multiple"]').select2({ placeholder: 'Select an option', theme: 'bootstrap-5' })

        $('.nyatakan').on('change', function (e) {
            $('#nyatakan_' + $(this).attr('name')).addClass('d-none')
            let showNyatakan = false
            const dataNyatakan = String($(this).data('nyatakan'))
            if ($(this).data('select2') !== undefined) {
                showNyatakan = $(this).val().includes(dataNyatakan)
            }
            else {
                showNyatakan = dataNyatakan == $(this).val()
            }
            if (showNyatakan) {
                $('#nyatakan_' + $(this).attr('name')).removeClass('d-none')
            }
        })

        loadYabam()
    </script>
</asp:Content>

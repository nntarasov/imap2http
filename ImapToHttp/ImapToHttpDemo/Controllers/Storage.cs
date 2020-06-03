using System;
using System.Collections.Generic;
using System.Linq;

namespace ImapToHttpDemo.Controllers
{
    public class DirectoryData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DirectoryData> Children { get; set; } = new List<DirectoryData>(); 
        public List<MessageData> Messages { get; set; } = new List<MessageData>();
        public List<string> AvailableFlags { get; set; } = new List<string>();
    }

    public class MessageData : MessageResponse
    {
        public int UId { get; set; }
        public List<string> Flags { get; set; } = new List<string>();
    }

    public static class Storage
    {
        public static IDictionary<int, DirectoryData> Directories { get; } =
            new[]
            {
                new DirectoryData
                {
                    Id = 1,
                    Name = "INBOX",
                    AvailableFlags = new[] {"\\Deleted", "\\Unseen", "\\Important", "\\Recent"}.ToList(),
                },
                new DirectoryData
                {
                    Id = 2,
                    Name = "test/nesting",
                    AvailableFlags = new[] {"\\Deleted", "\\Unseen", "\\Important", "\\Recent"}.ToList()
                }
            }.ToDictionary(k => k.Id, v => v);



        static Storage()
        {
            Directories[1].Messages.Add(new MessageData
                {
                    UId = 1,
                    Body = HseBody0Plain,
                    Date = DateTime.Parse("Mon, 27 Apr 2020 15:03:37 +0000"),
                    Flags = new[] { "\\Recent" }.ToList(),
                    Headers = new Dictionary<string, string>
                    {
                        {"From", "=?UTF-8?B?0JLRi9GI0LrQsCDQtNC70Y8g0YHQstC+0LjRhQ==?= <community@hse.ru>"},
                        {"To", "mail <mail@ntarasov.ru>"},
                        {"Subject", @"=?UTF-8?B?0J7QsdGB0YPQttC00LXQvdC40LUg0Jo=?=
 =?UTF-8?B?0L7QtNC10LrRgdCwINGN0YLQuNC60Lgs?=
 =?UTF-8?B?INC60L7QvNC/0LXQvdGB0LDRhtC40Lgg?=
 =?UTF-8?B?0L3QsCDQv9C10YDQtdC10LfQtCDQuNC3?=
 =?UTF-8?B?INC+0LHRidC10LbQuNGC0LjRjywg0YE=?=
 =?UTF-8?B?0LDQudGCINGBINC40YHRgtC+0YDQuNGP?=
 =?UTF-8?B?0LzQuCDQviDQstC+0LnQvdC1INC4IA==?=
 =?UTF-8?B?0LzQvdC+0LPQvtC1INC00YDRg9Cz0L7QtQ==?="},
                        {"Date", "Mon, 27 Apr 2020 15:03:37 +0000"},
                        {"Message-Id", "<1255x51xxxx58oosx7920843@mail.yandex.ru>"},
                        {"Content-Transfer-Encoding", "quoted-printable"},
                        {"Content-Type", "text/html; charset=utf-8"},
                        {"MIME-Version", "1.0"}
                    }.Select(h => new MessageHeaderResponse
                    {
                        Key = h.Key,
                        Value = h.Value
                    }).ToArray()
                }
            );

            Directories[1].Messages.Add(new MessageData
            {
                  UId = 13,
                  Body = BodyHse0,
                  Date = DateTime.Parse("Wed, 3 Jun 2020 15:14:02 +0000"),
                  Flags = new[] { "\\Recent" }.ToList(),
                  Headers = new Dictionary<string, string>
                  {
                    {"From", "=?UTF-8?B?0JLRi9GI0LrQsCDQtNC70Y8g0YHQstC+0LjRhQ==?= <community@hse.ru>"},
                    {"To", "User <mail@ntarasov.ru>"},
                    {"Subject", @"=?UTF-8?B?0JHQu9C40LbQsNC50YjQuNC1INC00L3QuCDQvg==?=
                            =?UTF-8?B?0YLQutGA0YvRgtGL0YUg0LTQstC10YDQtdC5IA==?=
                        =?UTF-8?B?0Lgg0LLQtdCx0LjQvdCw0YDRiyDQvNCw0LPQuA==?=
                        =?UTF-8?B?0YHRgtGA0LDRgtGD0YDRiyDQndCY0KMg0JLQqNCt?="},
                    {"Date", "Wed, 3 Jun 2020 15:14:02 +0000"},
                    {"Message-Id", "<12ss92ooo084xxx3@mail.yandex.ru>"},
                    {"Content-Transfer-Encoding", "quoted-printable"},
                    {"Content-Type", "text/html; charset=utf-8"},
                    {"MIME-Version", "1.0"}
                  }.Select(h => new MessageHeaderResponse
                  {
                    Key = h.Key,
                    Value = h.Value
                  }).ToArray()
            });
            
            
            Directories[1].Messages.Add(new MessageData
            {
                UId = 14,
                Body = BodyHse1,
                Flags = new[] { "\\Recent" }.ToList(),
                Date = DateTime.Parse("Tue, 2 Jun 2020 16:10:18 +0000"),
                Headers = new Dictionary<string, string>
                {
                    {"From", "=?UTF-8?B?0JLRi9GI0LrQsCDQntC90LvQsNC50L0=?= <elearn@hse.ru>"},
                    {"To", "User <mail@ntarasov.ru>"},
                    {"Subject", @"=?UTF-8?B?0J/RgNC+0LrRgtC+0YDQuNC90LM6INCy0L4=?=
 =?UTF-8?B?0L/RgNC+0YHRiyDQuCDQvtGC0LLQtdGC0Ys=?="},
                    {"Date", "Tue, 2 Jun 2020 16:10:18 +0000"},
                    {"Message-Id", "<DQntC90Lv@mail.yandex.ru>"},
                    {"Content-Transfer-Encoding", "quoted-printable"},
                    {"Content-Type", "text/html; charset=utf-8"},
                    {"MIME-Version", "1.0"}
                }.Select(h => new MessageHeaderResponse
                {
                    Key = h.Key,
                    Value = h.Value
                }).ToArray()
            });
            
            Directories[1].Messages.Add(new MessageData
            {
                UId = 15,
                Body = BodyHse2,
                Flags = new[] { "\\Recent" }.ToList(),
                Date = DateTime.Parse("Mon, 3 Feb 2020 14:27:03 +0000"),
                Headers = new Dictionary<string, string>
                {
                    {"From", "=?UTF-8?B?0JLRi9GI0LrQsCDQtNC70Y8g0YHQstC+0LjRhQ==?= <community@hse.ru>"},
                    {"To", "User <mail@ntarasov.ru>"},
                    {"Subject", @"=?UTF-8?B?0J3QvtGH0L3QvtC1INC60LDRgtCw0L3QuNC1LA==?=
 =?UTF-8?B?INCo0LrQvtC70LAg0LbRg9GA0L3QsNC70LjRgdGC?=
 =?UTF-8?B?0LjQutC4LCDQndCw0YPRh9C90YvQtSDQsdC+0Lg=?=
 =?UTF-8?B?INC4INC80L3QvtCz0L7QtSDQtNGA0YPQs9C+0LU=?="},
                    {"Date", "Mon, 3 Feb 2020 14:27:03 +0000"},
                    {"Message-Id", "<DQntC90Lqekqweqv@mail.yandex.ru>"},
                    {"Content-Transfer-Encoding", "quoted-printable"},
                    {"Content-Type", "text/html; charset=utf-8"},
                    {"MIME-Version", "1.0"}
                }.Select(h => new MessageHeaderResponse
                {
                    Key = h.Key,
                    Value = h.Value
                }).ToArray()
            });
            
            
            Directories[1].Messages.Add(new MessageData
            {
                UId = 16,
                Body = BodyHse3,
                Flags = new[] { "\\Recent" }.ToList(),
                Date = DateTime.Parse("Sun, 31 May 2020 06:40:35 +0000"),
                Headers = new Dictionary<string, string>
                {
                    {"From", "=?UTF-8?B?0KbQtdC90YLRgCDRgNCw0LfQstC40YLQuA==?= =?UTF-8?B?0Y8g0LrQsNGA0YzQtdGA0Ysg0JLQqNCt?= <career@hse.ru>"},
                    {"To", "User <mail@ntarasov.ru>"},
                    {"Subject", @"=?UTF-8?B?0JzQvdC+0LPQviDQu9C40LTQtdGA0YHQutC40YUg0L8=?=
 =?UTF-8?B?0YDQvtCz0YDQsNC80Lwg0L3QsCDQu9C10YLQviArIA==?=
 =?UTF-8?B?0L7QvdC70LDQudC9LdGN0LrRgdC60YPRgNGB0LjRjyA=?=
 =?UTF-8?B?0L3QsCDQt9Cw0LLQvtC0IENvY2EtQ29sYQ==?="},
                    {"Date", "Sun, 31 May 2020 06:40:35 +0000"},
                    {"Message-Id", "<DQneeeeeetC90Lqeqweqv@mail.yandex.ru>"},
                    {"Content-Transfer-Encoding", "quoted-printable"},
                    {"Content-Type", "text/html; charset=utf-8"},
                    {"MIME-Version", "1.0"}
                }.Select(h => new MessageHeaderResponse
                {
                    Key = h.Key,
                    Value = h.Value
                }).ToArray()
            });
            
            Directories[1].Messages.Add(new MessageData
            {
                UId = 17,
                Body = BodyHse3,
                Flags = new[] { "\\Recent" }.ToList(),
                Date = DateTime.Parse("Wed, 6 May 2020 16:41:26 +0000"),
                Headers = new Dictionary<string, string>
                {
                    {"From", "=?UTF-8?B?0JLRi9GI0LrQsCDQtNC70Y8g0YHQstC+0LjRhQ==?= <community@hse.ru>"},
                    {"To", "User <mail@ntarasov.ru>"},
                    {"Subject", @"=?UTF-8?B?0KDQtdC20LjQvCDQuNC30L7Qu9GP0YbQuNC4?=
 =?UTF-8?B?INCyINC+0LHRidC10LbQuNGC0LjRj9GFLA==?=
 =?UTF-8?B?INC40YHRgtC+0YDQuNC4ICPQktCo0K3QvQ==?=
 =?UTF-8?B?0LDQutCw0YDQsNC90YLQuNC90LUsINC70LU=?=
 =?UTF-8?B?0LrRhtC40Y8g0L4g0YLQvtC8LCDQutCw?=
 =?UTF-8?B?0Log0LjRgdC/0L7Qu9GM0LfQvtCy0LDRgtGM?=
 =?UTF-8?B?INC90LDRiNGDINC/0LDQvNGP0YLRjCwg?=
 =?UTF-8?B?0Lgg0LzQvdC+0LPQvtC1INC00YDRg9Cz0L7QtQ==?="},
                    {"Date", "Wed, 6 May 2020 16:41:26 +0000"},
                    {"Message-Id", "<DQnWN6s-vgVAUf-MtC9@mail.yandex.ru>"},
                    {"Content-Transfer-Encoding", "quoted-printable"},
                    {"Content-Type", "text/html; charset=utf-8"},
                    {"MIME-Version", "1.0"}
                }.Select(h => new MessageHeaderResponse
                {
                    Key = h.Key,
                    Value = h.Value
                }).ToArray()
            });
        }

        private const string BodyHse0 = @"
<!DOCTYPE html>
<html>
<head>
<meta name=3D""viewport"" content=3D""width=3Ddevice-width, initial-scale=3D1""=
>
<title></title>

<style type=3D""text/css"">
/* ///////// CLIENT-SPECIFIC STYLES ///////// */
#outlook a{padding:0;} /* Force Outlook to provide a 'view in browser' mess=
age */
.ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail to d=
isplay emails at full width */
.ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font,=
 .ExternalClass td, .ExternalClass div {line-height: 100%;} /* Force Hotmai=
l to display normal line spacing */
body, table, td, p, a, li, blockquote{-webkit-text-size-adjust:100%; -ms-te=
xt-size-adjust:100%;} /* Prevent WebKit and Windows mobile changing default=
 text sizes */
table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;} /* Remove spacing be=
tween tables in Outlook 2007 and up */
img{-ms-interpolation-mode:bicubic;} /* Allow smoother rendering of resized=
 image in Internet Explorer */
/* ///////// RESET STYLES ///////// */
body{margin:0; padding:0;}
img{border:0; height:auto; line-height:100%; outline:none; text-decoration:=
none;}
table{border-collapse:collapse !important;}
table td { border-collapse: collapse !important;}
body, #bodyTable, #bodyCell{height:100% !important; margin:0; padding:0; wi=
dth:100% !important;}
#mailBody.mailBody .uni-block.button-block { display:block; } /* Specific u=
kr.net style*/
body {
margin: 0;
text-align: left;
}
pre {
white-space: pre;
white-space: pre-wrap;
word-wrap: break-word;
}
table.mhLetterPreview { width:100%; }
table {
mso-table-lspace:0pt;
mso-table-rspace:0pt;
}
img {
-ms-interpolation-mode:bicubic;
}
</style>

<style id=3D""media_css"" type=3D""text/css"">
@media all and (max-width: 480px), only screen and (max-device-width : 480p=
x) {
    body{width:100% !important; min-width:100% !important;} /* Prevent iOS =
Mail from adding padding to the body */
    table[class=3D'container-table'] {
       width:100% !important;
    }
    td.image-wrapper {
       padding: 0 !important;
    }
    td.image-wrapper, td.text-wrapper {
       display:block !important;
       width:100% !important;
       max-width:initial !important;
    }
    table[class=3D'wrapper1'] {
       table-layout: fixed !important;
       padding: 0 !important;
       max-width: 600px !important;
    }
    td[class=3D'wrapper-row'] {
       table-layout: fixed !important;
       box-sizing: border-box !important;
       width:100% !important;
       min-width:100% !important;
    }
    table[class=3D'wrapper2'] {
       table-layout: fixed !important;
       border: none !important;
       width: 100% !important;
       max-width: 600px !important;
       min-height: 520px !important;
    }
    div[class=3D'column-wrapper']{
       max-width:300px !important;
    }
    table.uni-block {
       max-width:100% !important;
       height:auto !important;
       min-height: auto !important;
    }
    table[class=3D'block-wrapper-inner-table'] {
       height:auto !important;
       min-height: auto !important;
    }
    td[class=3D'block-wrapper'] {
       height:auto !important;
       min-height: auto !important;
    }
    .submit-button-block .button-wrapper=20
{       height: auto !important;
       width: auto !important;
       min-height: initial !important;
       max-height: initial !important;
       min-width: initial !important;
       max-width: initial !important;
    }
    img[class=3D'image-element'] {
       height:auto !important;
       box-sizing: border-box !important;
    }
}
@media all and (max-width: 615px), only screen and (max-device-width : 615p=
x) {
    td[class=3D'wrapper-row'] {
       padding: 0 !important;
       margin: 0 !important;
    }
    .column {
       width:100% !important;
       max-width:100% !important;
    }
}
</style>
<meta http-equiv=3D""Content-Type"" content=3D""text/html;charset=3DUTF-8"">
</head>
<body width=3D""100%"" align=3D""center"">
<!--[if gte mso 9]>       <style type=3D""text/css"">           .uni-block im=
g {               display:block !important;           }       </style><![en=
dif]-->
<center>
<table cellpadding=3D""0"" cellspacing=3D""0"" align=3D""center"" width=3D""100%"" =
class=3D""container responsive"">
<tbody>
<tr>
<td>
<table cellpadding=3D""0"" cellspacing=3D""0"" align=3D""center"" class=3D""wrappe=
r1"" style=3D""width: 100%; box-sizing: border-box; background-color: rgb(224=
, 224, 186); float: left;"">
<tbody>
<tr>
<td class=3D""wrapper-row"" style=3D""padding: 25px;""><!--[if (gte mso 9)|(IE)=
]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" ali=
gn=3D""center""><tr><td><![endif]-->
<table cellpadding=3D""0"" cellspacing=3D""0"" class=3D""wrapper2"" align=3D""cent=
er"" style=3D""background-color: rgb(255, 255, 255); border-radius: 3px; max-=
width: 600px; width: 100%; border: none; margin: 0px auto; border-spacing: =
0px; border-collapse: collapse;"">
<tbody>
<tr>
<td width=3D""100%"" class=3D""wrapper-row"" style=3D""vertical-align: top; max-=
width: 600px; font-size: 0px; padding: 0px;""><!--[if (gte mso 9)|(IE)]><tab=
le cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""=
center""><tr><td><![endif]-->
<table class=3D""uni-block social-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: right; height: 0px;"" class=3D""block-w=
rapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 0px;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px; background-image: none; text-a=
lign: right;"" class=3D""content-wrapper""><span class=3D""networks-wrapper""><s=
pan class=3D""scl-button scl-inst""><a href=3D""https://unimail.hse.ru/ru/mail=
_link_tracker?hash=3D6s9b6doxpnn3dyjnr33pasja89w17rz1ua7fpnpgssets36zh6grxw=
mrh6ak18of5wqwig55rqjg66dowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0c=
HM6Ly93d3cuaW5zdGFncmFtLmNvbS9zdHVkbGlmZWhzZS8_dXRtX21lZGl1bT1lbWFpbCZ1dG1f=
c291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjM0NjYzNjcw&uid=3DMTMyMzY3NA=3D=3D=
"" target=3D""_blank""><img style=3D""max-height:64px;max-width:64px;"" src=3D""h=
ttp://unimail.hse.ru/v5/img/ico/scl/inst.png"" alt=3D""Instagram""></a></span>=
 <span class=3D""scl-button scl-vk""><a href=3D""https://unimail.hse.ru/ru/mai=
l_link_tracker?hash=3D6hzrg7ch8e91kyjnr33pasja89w17rz1ua7fpnpgssets36zh6grf=
899mqdcbtc7s95e6jqw3waezqeyztq9354zmy19xro34zj6hpe5r5gw4qgwa48ay&url=3DaHR0=
cHM6Ly92ay5jb20vc3R1ZGxpZmVfaHNlP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1Vbml=
TZW5kZXImdXRtX2NhbXBhaWduPTIzNDY2MzY3MA~~&uid=3DMTMyMzY3NA=3D=3D"" target=3D=
""_blank""><img style=3D""max-height:64px;max-width:64px;"" src=3D""http://unima=
il.hse.ru/v5/img/ico/scl/vk.png"" alt=3D""=D0=92=D0=BA=D0=BE=D0=BD=D1=82=D0=
=B0=D0=BA=D1=82=D0=B5""></a></span> <span class=3D""scl-button scl-custom""><a=
 href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6c9fxao67bpypqj=
nr33pasja89w17rz1ua7fpnpgssets36zh6greudzihbw6zjcsa1tk3b8zcu1noeyztq9354zmy=
19xro34zj6hpe5r5gw4qgwa48ay&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci8_dXRtX21lZ=
Gl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjM0NjYzNjcw&uid=
=3DMTMyMzY3NA=3D=3D"" target=3D""_blank""><img style=3D""max-height:64px;max-wi=
dth:64px;"" src=3D""http://unimail.hse.ru/v5/img/ico/scl/custom.png"" alt=3D""=
=D0=9C=D0=BE=D0=B9 =D1=81=D0=B0=D0=B9=D1=82""></a></span></span></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 91px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 20px 20px; vertical-align: top;=
 font-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.=
8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:center;""><span style=3D""font-size:24px;""><strong><=
span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""lin=
e-height:1.5;"">=D0=94=D0=BD=D0=B8 =D0=BE=D1=82=D0=BA=D1=80=D1=8B=D1=82=D1=
=8B=D1=85 =D0=B4=D0=B2=D0=B5=D1=80=D0=B5=D0=B9 =D0=B8 =D0=B2=D0=B5=D0=B1=D0=
=B8=D0=BD=D0=B0=D1=80=D1=8B</span></span></strong></span><br>
<span style=3D""font-size:20px;""><strong><span style=3D""font-family:Arial, H=
elvetica, sans-serif;""><span style=3D""line-height:1.5;"">=D0=BC=D0=B0=D0=B3=
=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=82=D1=83=D1=80=D1=8B =D0=9D=D0=98=D0=A3 =
=D0=92=D0=A8=D0=AD</span></span></strong></span></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block image-block"" width=3D""100%"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heig=
ht: auto; border-collapse: collapse; border-spacing: 0px; display: inline-t=
able; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; padding: 0px; height: 100=
%;"" class=3D""block-wrapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 412.95px; width: 100%; table-layout: fixe=
d; text-align: center; border-spacing: 0px; border-collapse: collapse; font=
-size: 0px;"">
<tbody>
<tr>
<td style=3D""width: auto; height: 100%; display: inline-table;"" class=3D""co=
ntent-wrapper"">
<table class=3D""content-box"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""=
0"" style=3D""display: inline-table; vertical-align: top; width: auto; height=
: 100%; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""vertical-align: middle;"">
<div class=3D""image-wrapper image-drop""><a class=3D""image-link"" href=3D""htt=
ps://unimail.hse.ru/ru/mail_link_tracker?hash=3D6udkxzfkm61n84jnr33pasja89w=
17rz1ua7fpnpgssets36zh6grb9q1f6booinbb319xfdcgzmr81dowuby7eb1cf5ge75tbx3h5j=
71sywopdfiu6auo&url=3DaHR0cHM6Ly9tYS5oc2UucnUvZG9kP3V0bV9tZWRpdW09ZW1haWwmd=
XRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzNDY2MzY3MA~~&uid=3DMTMyMzY3N=
A=3D=3D"" target=3D""_blank""><img class=3D""image-element"" src=3D""http://unima=
il.hse.ru/ru/user_file?resource=3Dhimg&user_id=3D1323674&name=3D6keqweineuh=
5ajpiu3azzngxco47cfqz5xza57gs13gbdibf1z9sftr3dc7zbg7p5r7bmu5us4kquy"" alt=3D=
""Some Image"" id=3D""gridster_block_423_main_img"" style=3D""font-size: small; =
border: none; width: 100%; max-width: 600px; height: auto; max-height: 400p=
x; outline: none; text-decoration: none;"" width=3D""600""></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 130px; width: 100%; table-layout: fixed; =
border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px; vertical-align: top; font-size=
: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; color=
: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<p><span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helve=
tica, sans-serif;""><span style=3D""font-size:16px;""><strong>=D0=94=D0=BE=D0=
=B1=D1=80=D1=8B=D0=B9 =D0=B4=D0=B5=D0=BD=D1=8C!</strong><br>
<br>
=D0=92 =D0=B8=D1=8E=D0=BD=D0=B5 =D0=B2 =D0=92=D1=8B=D1=88=D0=BA=D0=B5 =D0=
=BF=D1=80=D0=BE=D0=B9=D0=B4=D1=83=D1=82 =D0=94=D0=BD=D0=B8 =D0=BE=D1=82=D0=
=BA=D1=80=D1=8B=D1=82=D1=8B=D1=85 =D0=B4=D0=B2=D0=B5=D1=80=D0=B5=D0=B9 =D0=
=B8 =D0=B2=D0=B5=D0=B1=D0=B8=D0=BD=D0=B0=D1=80=D1=8B&nbsp; =D0=BC=D0=B0=D0=
=B3=D0=B8=D1=81=D1=82=D0=B5=D1=80=D1=81=D0=BA=D0=B8=D1=85 =D0=BF=D1=80=D0=
=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC. =D0=9F=D1=80=D0=B8=D1=85=D0=BE=D0=B4=D0=
=B8=D1=82=D0=B5, =D1=87=D1=82=D0=BE=D0=B1=D1=8B =D1=83=D0=B7=D0=BD=D0=B0=D1=
=82=D1=8C =D0=B2=D1=81=D0=B5 =D0=BF=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=
=BE=D1=81=D1=82=D0=B8 =D0=BE =D0=BF=D0=BE=D1=81=D1=82=D1=83=D0=BF=D0=BB=D0=
=B5=D0=BD=D0=B8=D0=B8 =D0=B8 =D0=BE=D0=B1=D1=83=D1=87=D0=B5=D0=BD=D0=B8=D0=
=B8, =D1=81=D1=82=D0=B0=D0=B6=D0=B8=D1=80=D0=BE=D0=B2=D0=BA=D0=B0=D1=85 =D0=
=B8 =D0=BF=D0=B5=D1=80=D1=81=D0=BF=D0=B5=D0=BA=D1=82=D0=B8=D0=B2=D0=B0=D1=
=85 =D1=82=D1=80=D1=83=D0=B4=D0=BE=D1=83=D1=81=D1=82=D1=80=D0=BE=D0=B9=D1=
=81=D1=82=D0=B2=D0=B0.</span></span></span></p>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""background-color:#ffffcc;"">4 =D0=B8=D1=8E=D0=BD=D1=8F, 19:00</span><b=
r>
<strong><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""font-size:16px;""><span style=3D""line-height:1.5;"">=D0=A8=D0=BA=D0=BE=
=D0=BB=D0=B0 =D1=84=D0=B8=D0=BD=D0=B0=D0=BD=D1=81=D0=BE=D0=B2</span></span>=
</span></strong><br>
<span style=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica=
, sans-serif;""><span style=3D""line-height:1.5;""><span style=3D""line-height:=
1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-ser=
if;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""font-size:16px;""><span style=3D""line-height:1.5;"">=D0=92=D0=B5=D0=B1=D0=
=B8=D0=BD=D0=B0=D1=80 =D0=BC=D0=B0=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=
=82=D1=83=D1=80=D1=8B:&nbsp;<a href=3D""https://unimail.hse.ru/ru/mail_link_=
tracker?hash=3D6yhz43eycsobiyjnr33pasja89w17rz1ua7fpnpgssets36zh6grkt8d57pr=
hwp1cakx6se78q98uedowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly9=
3d3cuaHNlLnJ1L21hL2NmLz91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJn=
V0bV9jYW1wYWlnbj0yMzQ2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0=
,127,255);"">=C2=AB=D0=9A=D0=BE=D1=80=D0=BF=D0=BE=D1=80=D0=B0=D1=82=D0=B8=D0=
=B2=D0=BD=D1=8B=D0=B5 =D1=84=D0=B8=D0=BD=D0=B0=D0=BD=D1=81=D1=8B=C2=BB</a><=
/span></span></span></span></span></span></span></span></span></span><br>
=C2=AB=D0=9A=D0=BE=D0=B4 =D1=81=D1=82=D0=BE=D0=B8=D0=BC=D0=BE=D1=81=D1=82=
=D0=B8 =D0=B1=D0=B8=D0=B7=D0=BD=D0=B5=D1=81=D0=B0. =D0=9A=D0=B0=D0=BA =D1=
=80=D0=B0=D1=81=D1=88=D0=B8=D1=84=D1=80=D0=BE=D0=B2=D0=B0=D1=82=D1=8C =D0=
=B8 =D0=B8=D1=81=D0=BF=D0=BE=D0=BB=D1=8C=D0=B7=D0=BE=D0=B2=D0=B0=D1=82=D1=
=8C?=C2=BB</span></span></span></span></span></span></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 50px; height:=
 50px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 50px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 20px; background-color: rgb(2=
55, 127, 0); height: 44.8px; min-height: 44.8px;""><a class=3D""mailbtn"" href=
=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6m5sbw9zno8o8ajnr33p=
asja89w17rz1ua7fpnpgssets36zh6grnw6ziokzug7z5cg6oyi1w45grrdowuby7eb1cf5ge75=
tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L3dlYmluYXIvYW5ub3VuY2=
VtZW50cy8yMTUyNDU4NDguaHRtbD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZ=
GVyJnV0bV9jYW1wYWlnbj0yMzQ2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blan=
k"" style=3D""width:100%;display:inline-block;text-decoration:none;""><span cl=
ass=3D""btn-inner"" style=3D""display: inline; font-size: 16px; font-family: A=
rial, Helvetica, sans-serif; line-height: 19.2px; color: rgb(255, 255, 255)=
; background-color: rgb(255, 127, 0); border: 0px; word-break: break-all;"">=
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</span></=
a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""background-color:#ffffcc;"">7 =D0=B8=D1=8E=D0=BD=D1=8F, 15:00</span><b=
r>
<strong><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""font-size:16px;""><span style=3D""line-height:1.5;"">=D0=A4=D0=B0=D0=BA=
=D1=83=D0=BB=D1=8C=D1=82=D0=B5=D1=82 =D0=BC=D0=B8=D1=80=D0=BE=D0=B2=D0=BE=
=D0=B9 =D1=8D=D0=BA=D0=BE=D0=BD=D0=BE=D0=BC=D0=B8=D0=BA=D0=B8 =D0=B8 =D0=BC=
=D0=B8=D1=80=D0=BE=D0=B2=D0=BE=D0=B9 =D0=BF=D0=BE=D0=BB=D0=B8=D1=82=D0=B8=
=D0=BA=D0=B8</span></span></span></strong><br>
=D0=94=D0=B5=D0=BD=D1=8C =D0=BE=D1=82=D0=BA=D1=80=D1=8B=D1=82=D1=8B=D1=85 =
=D0=B4=D0=B2=D0=B5=D1=80=D0=B5=D0=B9 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=
=D0=BC=D0=BC=D1=8B =D0=B4=D0=B2=D1=83=D1=85 =D0=B4=D0=B8=D0=BF=D0=BB=D0=BE=
=D0=BC=D0=BE=D0=B2:<br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D67a5znepzhgti=
ajnr33pasja89w17rz1ua7fpnpgssets36zh6grctzwwjtf9u1qn9tt4q1a6sz6rcdowuby7eb1=
cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L21hL2VwYmE_dXR=
tX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjM0NjYzNj=
cw&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=AD=D0=BA=D0=
=BE=D0=BD=D0=BE=D0=BC=D0=B8=D0=BA=D0=B0, =D0=BF=D0=BE=D0=BB=D0=B8=D1=82=D0=
=B8=D0=BA=D0=B0 =D0=B8 =D0=B1=D0=B8=D0=B7=D0=BD=D0=B5=D1=81 =D0=B2 =D0=90=
=D0=B7=D0=B8=D0=B8 / Economics, politics and business in Asia</a></span></s=
pan></span></span></span></span></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 50px; height:=
 50px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 50px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 20px; background-color: rgb(2=
55, 127, 0); height: 44.8px; min-height: 44.8px;""><a class=3D""mailbtn"" href=
=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6zyauxqq1w1hisjnr33p=
asja89w17rz1ua7fpnpgssets36zh6grekimuhysydxwgnmu5wjbko1kfqeyztq9354zmy19xro=
34zj6hpe5r5gw4qgwa48ay&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L21hL2VwYmEvYW5ub3VuY2=
VtZW50cy8zNjY2MzI1MDMuaHRtbD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZ=
GVyJnV0bV9jYW1wYWlnbj0yMzQ2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blan=
k"" style=3D""width:100%;display:inline-block;text-decoration:none;""><span cl=
ass=3D""btn-inner"" style=3D""display: inline; font-size: 16px; font-family: A=
rial, Helvetica, sans-serif; line-height: 19.2px; color: rgb(255, 255, 255)=
; background-color: rgb(255, 127, 0); border: 0px; word-break: break-all;"">=
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</span></=
a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-se=
rif;""><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><sp=
an style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-=
height:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><spa=
n style=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sa=
ns-serif;""><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;=
""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""=
line-height:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""background-color:#ffffcc;"">8 =D0=B8=D1=8E=D0=BD=D1=8F, 18:0=
0</span></span></span></span></span></span></span></span></span></span></sp=
an></span></span></span></span><br>
<strong><span style=3D""font-size:16px;""><span style=3D""font-family:Arial, H=
elvetica, sans-serif;""><span style=3D""line-height:1.5;""><span style=3D""line=
-height:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><sp=
an style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetica, =
sans-serif;"">=D0=98=D0=BD=D1=81=D1=82=D0=B8=D1=82=D1=83=D1=82 =D0=B4=D0=B5=
=D0=BC=D0=BE=D0=B3=D1=80=D0=B0=D1=84=D0=B8=D0=B8</span></span></span></span=
></span></span></span></strong><br>
<span style=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica=
, sans-serif;""><span style=3D""line-height:1.5;""><span style=3D""line-height:=
1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-ser=
if;"">=D0=94=D0=B5=D0=BD=D1=8C =D0=BE=D1=82=D0=BA=D1=80=D1=8B=D1=82=D1=8B=D1=
=85 =D0=B4=D0=B2=D0=B5=D1=80=D0=B5=D0=B9 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=
=B0=D0=BC=D0=BC=D1=8B:</span></span></span></span></span></span></span></sp=
an></span></span></span></span></span></span><br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6idjdid7rm6g1=
sjnr33pasja89w17rz1ua7fpnpgssets36zh6gr8royf3p1zbidfwq3az1se5hc9gdowuby7eb1=
cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L21hL3BkP3V0bV9=
tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzNDY2MzY3MA=
~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);""><span style=3D""f=
ont-size:16px;""><span style=3D""line-height:1.5;""><span style=3D""font-family=
:Arial, Helvetica, sans-serif;"">=D0=9D=D0=B0=D1=81=D0=B5=D0=BB=D0=B5=D0=BD=
=D0=B8=D0=B5 =D0=B8 =D1=80=D0=B0=D0=B7=D0=B2=D0=B8=D1=82=D0=B8=D0=B5 / Popu=
lation and development</span></span></span></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 50px; height:=
 50px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 50px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 20px; background-color: rgb(2=
55, 127, 0); height: 44.8px; min-height: 44.8px;""><a class=3D""mailbtn"" href=
=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6s6o3nmupizxfnjnr33p=
asja89w17rz1ua7fpnpgssets36zh6grq9y8e6wqp9cekobkz1tn66ijas5bxs7u9pcem3wknt3=
a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L21hL3BkL2Fubm91bmNlbW=
VudHMvMzY3NjA2NTQzLmh0bWw_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlc=
iZ1dG1fY2FtcGFpZ249MjM0NjYzNjcw&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank"" s=
tyle=3D""width:100%;display:inline-block;text-decoration:none;""><span class=
=3D""btn-inner"" style=3D""display: inline; font-size: 16px; font-family: Aria=
l, Helvetica, sans-serif; line-height: 19.2px; color: rgb(255, 255, 255); b=
ackground-color: rgb(255, 127, 0); border: 0px; word-break: break-all;"">=D0=
=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</span></a><=
/td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""background-color:#ffffcc;"">9 =D0=B8=D1=8E=D0=BD=D1=8F, 18:30</span><b=
r>
<span style=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica=
, sans-serif;""><span style=3D""line-height:1.5;""><span style=3D""line-height:=
1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-ser=
if;""><span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Hel=
vetica, sans-serif;""><span style=3D""background-color:#ffffff;""><strong>=D0=
=92=D1=8B=D1=81=D1=88=D0=B0=D1=8F =D1=88=D0=BA=D0=BE=D0=BB=D0=B0 =D1=83=D1=
=80=D0=B1=D0=B0=D0=BD=D0=B8=D1=81=D1=82=D0=B8=D0=BA=D0=B8 =D0=B8=D0=BC=D0=
=B5=D0=BD=D0=B8 =D0=90.=D0=90. =D0=92=D1=8B=D1=81=D0=BE=D0=BA=D0=BE=D0=B2=
=D1=81=D0=BA=D0=BE=D0=B3=D0=BE</strong></span></span></span><br>
=D0=94=D0=B5=D0=BD=D1=8C =D0=BE=D1=82=D0=BA=D1=80=D1=8B=D1=82=D1=8B=D1=85 =
=D0=B4=D0=B2=D0=B5=D1=80=D0=B5=D0=B9 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=
=D0=BC=D0=BC=D1=8B:</span></span></span></span></span></span></span></span>=
</span></span></span></span></span></span><br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D64mt41utrfzwy=
hjnr33pasja89w17rz1ua7fpnpgssets36zh6grnh3oofiqyybdyuugruris365upiymni3oot4=
gwcknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L21hL3RlY2hjaXR=
5Lz91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMz=
Q2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);""><span st=
yle=3D""font-size:16px;""><span style=3D""line-height:1.5;""><span style=3D""fon=
t-family:Arial, Helvetica, sans-serif;"">=D0=9F=D1=80=D0=BE=D1=82=D0=BE=D1=
=82=D0=B8=D0=BF=D0=B8=D1=80=D0=BE=D0=B2=D0=B0=D0=BD=D0=B8=D0=B5 =D0=B3=D0=
=BE=D1=80=D0=BE=D0=B4=D0=BE=D0=B2 =D0=B1=D1=83=D0=B4=D1=83=D1=89=D0=B5=D0=
=B3=D0=BE</span></span></span></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""background-color:#ffffcc;"">11 =D0=B8=D1=8E=D0=BD=D1=8F, 16:00</span><=
br>
<span style=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica=
, sans-serif;""><span style=3D""line-height:1.5;""><span style=3D""line-height:=
1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-ser=
if;""><span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Hel=
vetica, sans-serif;""><span style=3D""background-color:#ffffff;""><strong>=D0=
=A4=D0=B0=D0=BA=D1=83=D0=BB=D1=8C=D1=82=D0=B5=D1=82 =D0=B3=D1=83=D0=BC=D0=
=B0=D0=BD=D0=B8=D1=82=D0=B0=D1=80=D0=BD=D1=8B=D1=85 =D0=BD=D0=B0=D1=83=D0=
=BA</strong></span></span></span><br>
=D0=92=D0=B5=D0=B1=D0=B8=D0=BD=D0=B0=D1=80 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=
=D0=B0=D0=BC=D0=BC:</span></span></span></span></span></span></span></span>=
</span></span></span></span></span></span><br>
<span style=3D""font-size:16px;""><span style=3D""line-height:1.5;""><span styl=
e=3D""font-family:Arial, Helvetica, sans-serif;""><a href=3D""https://unimail.=
hse.ru/ru/mail_link_tracker?hash=3D6ztezn145bx3p1jnr33pasja89w17rz1ua7fpnpg=
ssets36zh6grm6odt9o5i4t355ex6q9zojknuxcfzqrh53f9cfwknt3a4jmx9fqz66eftcjthdg=
co&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L21hL3BoaWxvc29waGljYWwvP3V0bV9tZWRpdW09ZW=
1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzNDY2MzY3MA~~&uid=3DMT=
MyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=A4=D0=B8=D0=BB=D0=BE=D1=
=81=D0=BE=D1=84=D1=81=D0=BA=D0=B0=D1=8F =D0=B0=D0=BD=D1=82=D1=80=D0=BE=D0=
=BF=D0=BE=D0=BB=D0=BE=D0=B3=D0=B8=D1=8F</a><br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6s818oxuxgd7f=
gjnr33pasja89w17rz1ua7fpnpgssets36zh6grnzpa1rb634b6uko18wdwtobayqdowuby7eb1=
cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L21hL3JlbGhpc3Q=
vP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzND=
Y2MzY3MA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=A4=
=D0=B8=D0=BB=D0=BE=D1=81=D0=BE=D1=84=D0=B8=D1=8F =D0=B8 =D0=B8=D1=81=D1=82=
=D0=BE=D1=80=D0=B8=D1=8F =D1=80=D0=B5=D0=BB=D0=B8=D0=B3=D0=B8=D0=B8</a></sp=
an></span></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 69px; height:=
 69px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 69px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 20px; background-color: rgb(2=
55, 127, 0); height: 44.8px; min-height: 44.8px;""><a class=3D""mailbtn"" href=
=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6ppr76umkoyq4hjnr33p=
asja89w17rz1ua7fpnpgssets36zh6grjsdg78nfe4ww14rnppzth3htb1dowuby7eb1cf5ge75=
tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L3dlYmluYXIvYW5ub3VuY2=
VtZW50cy8yMTc4OTk0ODUuaHRtbD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZ=
GVyJnV0bV9jYW1wYWlnbj0yMzQ2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blan=
k"" style=3D""width:100%;display:inline-block;text-decoration:none;""><span cl=
ass=3D""btn-inner"" style=3D""display: inline; font-size: 16px; font-family: A=
rial, Helvetica, sans-serif; line-height: 19.2px; color: rgb(255, 255, 255)=
; background-color: rgb(255, 127, 0); border: 0px; word-break: break-all;"">=
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</span></=
a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""background-color:#ffffcc;"">15 =D0=B8=D1=8E=D0=BD=D1=8F, 19:00</span><=
/span></span></span></span></span></span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><strong>=D0=9C=D0=B5=D0=B6=D0=B4=D1=83=D0=BD=D0=B0=D1=80=D0=
=BE=D0=B4=D0=BD=D0=B0=D1=8F =D0=BB=D0=B0=D0=B1=D0=BE=D1=80=D0=B0=D1=82=D0=
=BE=D1=80=D0=B8=D1=8F =D0=BF=D1=80=D0=B8=D0=BA=D0=BB=D0=B0=D0=B4=D0=BD=D0=
=BE=D0=B3=D0=BE =D1=81=D0=B5=D1=82=D0=B5=D0=B2=D0=BE=D0=B3=D0=BE =D0=B0=D0=
=BD=D0=B0=D0=BB=D0=B8=D0=B7=D0=B0</strong></span></span><br>
<span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""li=
ne-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-family=
:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><span styl=
e=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""font-size:16=
px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-=
family:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><spa=
n style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=94=D0=B5=D0=BD=D1=
=8C =D0=BE=D1=82=D0=BA=D1=80=D1=8B=D1=82=D1=8B=D1=85 =D0=B4=D0=B2=D0=B5=D1=
=80=D0=B5=D0=B9 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC=D1=8B:</sp=
an></span></span></span></span></span></span></span></span></span></span></=
span></span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=
=3D6nbqrp1ou7pknsjnr33pasja89w17rz1ua7fpnpgssets36zh6grcoxjh3zjj3r913k477bf=
oik7h3y6gt5f1gmfwtyknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ=
1L21hL3NuYT91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYW=
lnbj0yMzQ2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=
=D0=9F=D1=80=D0=B8=D0=BA=D0=BB=D0=B0=D0=B4=D0=BD=D0=B0=D1=8F =D1=81=D1=82=
=D0=B0=D1=82=D0=B8=D1=81=D1=82=D0=B8=D0=BA=D0=B0 =D1=81 =D0=BC=D0=B5=D1=82=
=D0=BE=D0=B4=D0=B0=D0=BC=D0=B8 =D1=81=D0=B5=D1=82=D0=B5=D0=B2=D0=BE=D0=B3=
=D0=BE =D0=B0=D0=BD=D0=B0=D0=BB=D0=B8=D0=B7=D0=B0</a></span></span></span><=
/div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 69px; height:=
 69px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 69px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 20px; background-color: rgb(2=
55, 127, 0); height: 44.8px; min-height: 44.8px;""><a class=3D""mailbtn"" href=
=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6k5ariwxdkoo8gjnr33p=
asja89w17rz1ua7fpnpgssets36zh6grx8rdok7w7hbhj3zuygydgngt6zcfzqrh53f9cfwknt3=
a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9hbnIuaHNlLnJ1L2Fubm91bmNlbWVudHMvMz=
UwOTI4NzQ3Lmh0bWw_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY=
2FtcGFpZ249MjM0NjYzNjcw&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank"" style=3D""=
width:100%;display:inline-block;text-decoration:none;""><span class=3D""btn-i=
nner"" style=3D""display: inline; font-size: 16px; font-family: Arial, Helvet=
ica, sans-serif; line-height: 19.2px; color: rgb(255, 255, 255); background=
-color: rgb(255, 127, 0); border: 0px; word-break: break-all;"">=D0=A0=D0=B5=
=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</span></a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 121px; width: 100%; table-layout: fixed; =
border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""background-color:#ffffcc;"">16 =D0=B8=D1=8E=D0=BD=D1=8F, 18:30</span><=
/span></span></span></span></span></span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><strong>=D0=A4=D0=B0=D0=BA=D1=83=D0=BB=D1=8C=D1=82=D0=B5=D1=
=82 =D0=B3=D1=83=D0=BC=D0=B0=D0=BD=D0=B8=D1=82=D0=B0=D1=80=D0=BD=D1=8B=D1=
=85 =D0=BD=D0=B0=D1=83=D0=BA</strong></span></span><br>
<span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""li=
ne-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-family=
:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><span styl=
e=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""font-size:16=
px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-=
family:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><spa=
n style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=92=D0=B5=D0=B1=D0=
=B8=D0=BD=D0=B0=D1=80 =D0=BD=D0=BE=D0=B2=D0=BE=D0=B9 =D0=BF=D1=80=D0=BE=D0=
=B3=D1=80=D0=B0=D0=BC=D0=BC=D1=8B:</span></span></span></span></span></span=
></span></span></span></span></span></span></span></span><br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6kh5ek4jqqfqk=
1jnr33pasja89w17rz1ua7fpnpgssets36zh6grcyww6byjkncts8ib66gb81cm9wgftcrc1fox=
ceyknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L21hL213Lz91dG1=
fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMzQ2NjM2Nz=
A~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);""><span style=3D""f=
ont-size:16px;""><span style=3D""line-height:1.5;""><span style=3D""font-family=
:Arial, Helvetica, sans-serif;"">=C2=AB=D0=9C=D1=83=D1=81=D1=83=D0=BB=D1=8C=
=D0=BC=D0=B0=D0=BD=D1=81=D0=BA=D0=B8=D0=B5 =D0=BC=D0=B8=D1=80=D1=8B =D0=B2 =
=D0=A0=D0=BE=D1=81=D1=81=D0=B8=D0=B8 (=D0=98=D1=81=D1=82=D0=BE=D1=80=D0=B8=
=D1=8F =D0=B8 =D0=BA=D1=83=D0=BB=D1=8C=D1=82=D1=83=D1=80=D0=B0)=C2=BB</span=
></span></span></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 69px; height:=
 69px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 69px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 20px; background-color: rgb(2=
55, 127, 0); height: 44.8px; min-height: 44.8px;""><a class=3D""mailbtn"" href=
=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6pgnt6uz7a1ztwjnr33p=
asja89w17rz1ua7fpnpgssets36zh6grjrkgjnf6rxbn7g733yuu1548w99yzhx5t8ngiyhknt3=
a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L3dlYmluYXIvYW5ub3VuY2=
VtZW50cy8zNDUwODM0MDcuaHRtbD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZ=
GVyJnV0bV9jYW1wYWlnbj0yMzQ2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blan=
k"" style=3D""width:100%;display:inline-block;text-decoration:none;""><span cl=
ass=3D""btn-inner"" style=3D""display: inline; font-size: 16px; font-family: A=
rial, Helvetica, sans-serif; line-height: 19.2px; color: rgb(255, 255, 255)=
; background-color: rgb(255, 127, 0); border: 0px; word-break: break-all;"">=
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</span></=
a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 265px; width: 100%; table-layout: fixed; =
border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""background-color:#ffffcc;"">16 =D0=B8=D1=8E=D0=BD=D1=8F, 19:00</span><=
/span></span></span></span></span></span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><strong>=D0=A8=D0=BA=D0=BE=D0=BB=D0=B0 =D0=B4=D0=B8=D0=B7=
=D0=B0=D0=B9=D0=BD=D0=B0</strong></span></span><br>
<span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""li=
ne-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-family=
:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><span styl=
e=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""font-size:16=
px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-=
family:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><spa=
n style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=94=D0=B5=D0=BD=D1=
=8C =D0=BE=D1=82=D0=BA=D1=80=D1=8B=D1=82=D1=8B=D1=85 =D0=B4=D0=B2=D0=B5=D1=
=80=D0=B5=D0=B9 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC:</span></s=
pan></span></span></span></span></span></span></span></span></span></span><=
/span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=
=3D6r79ut5q55bsqsjnr33pasja89w17rz1ua7fpnpgssets36zh6grg9y5czypsf893oj5yad6=
hknftgdowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly9kZXNpZ24uaHN=
lLnJ1L21hL3Byb2dyYW0vY29tbXVuaWNhdGlvbj91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2=
U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMzQ2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" styl=
e=3D""color:rgb(0,127,255);"">=D0=9A=D0=BE=D0=BC=D0=BC=D1=83=D0=BD=D0=B8=D0=
=BA=D0=B0=D1=86=D0=B8=D0=BE=D0=BD=D0=BD=D1=8B=D0=B9 =D0=B8 =D1=86=D0=B8=D1=
=84=D1=80=D0=BE=D0=B2=D0=BE=D0=B9 =D0=B4=D0=B8=D0=B7=D0=B0=D0=B9=D0=BD</a><=
br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6x9prrbsph8c6=
gjnr33pasja89w17rz1ua7fpnpgssets36zh6gr8t6p5d3cazbrcpzhmn8supi65kdowuby7eb1=
cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly9kZXNpZ24uaHNlLnJ1L21hL3Byb2d=
yYW0vZGVzaWduP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbX=
BhaWduPTIzNDY2MzY3MA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255=
);"">=D0=94=D0=B8=D0=B7=D0=B0=D0=B9=D0=BD</a><br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D68tg3ebjdburr=
ajnr33pasja89w17rz1ua7fpnpgssets36zh6grfbsfd56bxew1cpajqkmkt3ttn1dowuby7eb1=
cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly9kZXNpZ24uaHNlLnJ1L21hL3Byb2d=
yYW0vZmFzaGlvbj91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW=
1wYWlnbj0yMzQ2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255=
);"">=D0=9C=D0=BE=D0=B4=D0=B0</a><br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6urur8ygx3ir4=
4jnr33pasja89w17rz1ua7fpnpgssets36zh6gryur56wnau7pk5m6e5mhg981rig3twcgsaro7=
6uhknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9kZXNpZ24uaHNlLnJ1L21hP3V0bV9=
tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzNDY2MzY3MC=
NhcnQ~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=9F=D1=80=
=D0=B0=D0=BA=D1=82=D0=B8=D0=BA=D0=B8 =D1=81=D0=BE=D0=B2=D1=80=D0=B5=D0=BC=
=D0=B5=D0=BD=D0=BD=D0=BE=D0=B3=D0=BE =D0=B8=D1=81=D0=BA=D1=83=D1=81=D1=81=
=D1=82=D0=B2=D0=B0</a><br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6ijknpfkdjan6=
sjnr33pasja89w17rz1ua7fpnpgssets36zh6gr8ezn8onawyty9jzy1awnpxuw165bxs7u9pce=
m3wknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9kZXNpZ24uaHNlLnJ1L21hP3V0bV9=
tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzNDY2MzY3MC=
NpbnQ~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=94=D0=B8=
=D0=B7=D0=B0=D0=B9=D0=BD =D0=B8=D0=BD=D1=82=D0=B5=D1=80=D1=8C=D0=B5=D1=80=
=D0=B0</a><br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D64k19h7j5pdwr=
1jnr33pasja89w17rz1ua7fpnpgssets36zh6grygj9113k8e9cjrmfe4km3i13yedowuby7eb1=
cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly9kZXNpZ24uaHNlLnJ1L21hP3V0bV9=
tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzNDY2MzY3MC=
NlZHU~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=A1=D0=BE=
=D0=B2=D1=80=D0=B5=D0=BC=D0=B5=D0=BD=D0=BD=D1=8B=D0=B9 =D0=B4=D0=B8=D0=B7=
=D0=B0=D0=B9=D0=BD =D0=B2 =D0=BF=D1=80=D0=B5=D0=BF=D0=BE=D0=B4=D0=B0=D0=B2=
=D0=B0=D0=BD=D0=B8=D0=B8 =D0=B8=D0=B7=D0=BE=D0=B1=D1=80=D0=B0=D0=B7=D0=B8=
=D1=82=D0=B5=D0=BB=D1=8C=D0=BD=D0=BE=D0=B3=D0=BE =D0=B8=D1=81=D0=BA=D1=83=
=D1=81=D1=81=D1=82=D0=B2=D0=B0 =D0=B8 =D1=82=D0=B5=D1=85=D0=BD=D0=BE=D0=BB=
=D0=BE=D0=B3=D0=B8=D0=B8 =D0=B2 =D1=88=D0=BA=D0=BE=D0=BB=D0=B5</a></span></=
span></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 69px; height:=
 69px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 69px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 20px; background-color: rgb(2=
55, 127, 0); height: 44.8px; min-height: 44.8px;""><a class=3D""mailbtn"" href=
=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6fdxpnesnmsfhyjnr33p=
asja89w17rz1ua7fpnpgssets36zh6grfeu9xz91pm4xz5gnsn8gfkbussdowuby7eb1cf5ge75=
tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly9oc2UtZGVzaWduLnRpbWVwYWQucnUvZXZlbn=
QvMTMxOTk4My8_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2Ftc=
GFpZ249MjM0NjYzNjcw&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank"" style=3D""widt=
h:100%;display:inline-block;text-decoration:none;""><span class=3D""btn-inner=
"" style=3D""display: inline; font-size: 16px; font-family: Arial, Helvetica,=
 sans-serif; line-height: 19.2px; color: rgb(255, 255, 255); background-col=
or: rgb(255, 127, 0); border: 0px; word-break: break-all;"">=D0=A0=D0=B5=D0=
=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</span></a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""background-color:#ffffcc;"">17 =D0=B8=D1=8E=D0=BD=D1=8F, 18:00</span><=
/span></span></span></span></span></span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><strong>=D0=A4=D0=B0=D0=BA=D1=83=D0=BB=D1=8C=D1=82=D0=B5=D1=
=82 =D0=B1=D0=B8=D0=B7=D0=BD=D0=B5=D1=81=D0=B0 =D0=B8 =D0=BC=D0=B5=D0=BD=D0=
=B5=D0=B4=D0=B6=D0=BC=D0=B5=D0=BD=D1=82=D0=B0</strong></span></span><br>
<span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""li=
ne-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-family=
:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><span styl=
e=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""font-size:16=
px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-=
family:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><spa=
n style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=92=D0=B5=D0=B1=D0=
=B8=D0=BD=D0=B0=D1=80 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC=D1=
=8B:</span></span></span></span></span></span></span></span></span></span><=
/span></span></span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=
=3D67yt6o7yh1685ejnr33pasja89w17rz1ua7fpnpgssets36zh6grg1skjnk8hpq7pzewifut=
kfh8psdowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ=
1L21hL3RvdXJpc20vP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2=
NhbXBhaWduPTIzNDY2MzY3MA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127=
,255);"">=C2=AB=D0=AD=D0=BA=D0=BE=D0=BD=D0=BE=D0=BC=D0=B8=D0=BA=D0=B0 =D0=B2=
=D0=BF=D0=B5=D1=87=D0=B0=D1=82=D0=BB=D0=B5=D0=BD=D0=B8=D0=B9: =D0=BC=D0=B5=
=D0=BD=D0=B5=D0=B4=D0=B6=D0=BC=D0=B5=D0=BD=D1=82 =D0=B2 =D0=B8=D0=BD=D0=B4=
=D1=83=D1=81=D1=82=D1=80=D0=B8=D0=B8 =D0=B3=D0=BE=D1=81=D1=82=D0=B5=D0=BF=
=D1=80=D0=B8=D0=B8=D0=BC=D1=81=D1=82=D0=B2=D0=B0 =D0=B8 =D1=82=D1=83=D1=80=
=D0=B8=D0=B7=D0=BC=D0=B5=C2=BB</a></span></span></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 69px; height:=
 69px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 69px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 20px; background-color: rgb(2=
55, 127, 0); height: 44.8px; min-height: 44.8px;""><a class=3D""mailbtn"" href=
=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6yry4k6rr8ytz1jnr33p=
asja89w17rz1ua7fpnpgssets36zh6grx74y75wrz1cthmoxt3h8tzkbbodowuby7eb1cf5ge75=
tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L3dlYmluYXIvYW5ub3VuY2=
VtZW50cy8yMjYxNTY0NjQuaHRtbD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZ=
GVyJnV0bV9jYW1wYWlnbj0yMzQ2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blan=
k"" style=3D""width:100%;display:inline-block;text-decoration:none;""><span cl=
ass=3D""btn-inner"" style=3D""display: inline; font-size: 16px; font-family: A=
rial, Helvetica, sans-serif; line-height: 19.2px; color: rgb(255, 255, 255)=
; background-color: rgb(255, 127, 0); border: 0px; word-break: break-all;"">=
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</span></=
a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""background-color:#ffffcc;"">17 =D0=B8=D1=8E=D0=BD=D1=8F, 19:30</span><=
/span></span></span></span></span></span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><strong>=D0=94=D0=B5=D0=BF=D0=B0=D1=80=D1=82=D0=B0=D0=BC=D0=
=B5=D0=BD=D1=82 =D0=BC=D0=B5=D0=B4=D0=B8=D0=B0</strong></span></span><br>
<span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""li=
ne-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-family=
:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><span styl=
e=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""font-size:16=
px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-=
family:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><spa=
n style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=94=D0=B5=D0=BD=D1=
=8C =D0=BE=D1=82=D0=BA=D1=80=D1=8B=D1=82=D1=8B=D1=85 =D0=B4=D0=B2=D0=B5=D1=
=80=D0=B5=D0=B9 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC:</span></s=
pan></span></span></span></span></span></span></span></span></span></span><=
/span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=
=3D6gn95dg4jmmw3gjnr33pasja89w17rz1ua7fpnpgssets36zh6grxkhfhirfwpyf13mcay8r=
efg91ydowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ=
1L21hL2RpZ2l0YWwvP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2=
NhbXBhaWduPTIzNDY2MzY3MA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127=
,255);"">=D0=A2=D1=80=D0=B0=D0=BD=D1=81=D0=BC=D0=B5=D0=B4=D0=B8=D0=B9=D0=BD=
=D0=BE=D0=B5 =D0=BF=D1=80=D0=BE=D0=B8=D0=B7=D0=B2=D0=BE=D0=B4=D1=81=D1=82=
=D0=B2=D0=BE =D0=B2 =D1=86=D0=B8=D1=84=D1=80=D0=BE=D0=B2=D1=8B=D1=85 =D0=B8=
=D0=BD=D0=B4=D1=83=D1=81=D1=82=D1=80=D0=B8=D1=8F=D1=85</a><br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D69cjaynzkqujd=
wjnr33pasja89w17rz1ua7fpnpgssets36zh6gr88hwqkzz5qsefx77uyzp8zo3f7uxfk9zpdzj=
snaknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L21hL21lZGlhLz9=
1dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMzQ2Nj=
M2NzA~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=9C=D0=B5=
=D0=BD=D0=B5=D0=B4=D0=B6=D0=BC=D0=B5=D0=BD=D1=82 =D0=B2 =D0=A1=D0=9C=D0=98<=
/a></span></span></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 69px; height:=
 69px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 69px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 20px; background-color: rgb(2=
55, 127, 0); height: 44.8px; min-height: 44.8px;""><a class=3D""mailbtn"" href=
=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D66s7tc5i4kmitkjnr33p=
asja89w17rz1ua7fpnpgssets36zh6grj73s1uxdq4h6m71as1m1eorgh4eyztq9354zmy19xro=
34zj6hpe5r5gw4qgwa48ay&url=3DaHR0cHM6Ly9oc2VtZWRpYS50aW1lcGFkLnJ1L2V2ZW50Lz=
EyMjg3MjgvP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBha=
WduPTIzNDY2MzY3MA~~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank"" style=3D""widt=
h:100%;display:inline-block;text-decoration:none;""><span class=3D""btn-inner=
"" style=3D""display: inline; font-size: 16px; font-family: Arial, Helvetica,=
 sans-serif; line-height: 19.2px; color: rgb(255, 255, 255); background-col=
or: rgb(255, 127, 0); border: 0px; word-break: break-all;"">=D0=A0=D0=B5=D0=
=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</span></a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 121px; width: 100%; table-layout: fixed; =
border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span sty=
le=3D""background-color:#ffffcc;"">18 =D0=B8=D1=8E=D0=BD=D1=8F, 18:30</span><=
/span></span></span></span></span></span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><strong>=D0=92=D1=8B=D1=81=D1=88=D0=B0=D1=8F =D1=88=D0=BA=
=D0=BE=D0=BB=D0=B0 =D1=8E=D1=80=D0=B8=D1=81=D0=BF=D1=80=D1=83=D0=B4=D0=B5=
=D0=BD=D1=86=D0=B8=D0=B8</strong></span></span><br>
<span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""li=
ne-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-family=
:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><span styl=
e=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""font-size:16=
px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=
=3D""line-height:1.5;""><span style=3D""line-height:1.5;""><span style=3D""font-=
family:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><spa=
n style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=92=D0=B5=D0=B1=D0=
=B8=D0=BD=D0=B0=D1=80 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC=D1=
=8B:</span></span></span></span></span></span></span></span></span></span><=
/span></span></span><br>
<span style=3D""line-height:1.5;""><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;""><a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=
=3D6k58sqrkyxj77rjnr33pasja89w17rz1ua7fpnpgssets36zh6grbyqaqqo6iaaqsxbrf6nx=
dsyczpcfzqrh53f9cfwknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ=
1L21hL2Zpbmxhdy8_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2=
FtcGFpZ249MjM0NjYzNjcw&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255=
);"">=C2=AB=D0=AE=D1=80=D0=B8=D1=81=D1=82 =D0=BC=D0=B8=D1=80=D0=BE=D0=B2=D0=
=BE=D0=B3=D0=BE =D1=84=D0=B8=D0=BD=D0=B0=D0=BD=D1=81=D0=BE=D0=B2=D0=BE=D0=
=B3=D0=BE =D1=80=D1=8B=D0=BD=D0=BA=D0=B0=C2=BB&nbsp;</a></span></span></spa=
n></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 69px; height:=
 69px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 69px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 20px; background-color: rgb(2=
55, 127, 0); height: 44.8px; min-height: 44.8px;""><a class=3D""mailbtn"" href=
=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D67jps3sxyhpdkrjnr33p=
asja89w17rz1ua7fpnpgssets36zh6grjpmuz8hot586hfxoj5qhso8ah65bxs7u9pcem3wknt3=
a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L3dlYmluYXIvYW5ub3VuY2=
VtZW50cy8yMjE1NDA2NzYuaHRtbD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZ=
GVyJnV0bV9jYW1wYWlnbj0yMzQ2NjM2NzA~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blan=
k"" style=3D""width:100%;display:inline-block;text-decoration:none;""><span cl=
ass=3D""btn-inner"" style=3D""display: inline; font-size: 16px; font-family: A=
rial, Helvetica, sans-serif; line-height: 19.2px; color: rgb(255, 255, 255)=
; background-color: rgb(255, 127, 0); border: 0px; word-break: break-all;"">=
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</span></=
a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 37.9625px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 37.9625px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse; min-height: 37.9625px;""=
>
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 37.9625px; min-he=
ight: 37.9625px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix cke_editable cke_editable_inline cke_contents_ltr ck=
e_show_borders"" style=3D""overflow-wrap: break-word; position: relative;"" ta=
bindex=3D""0"" spellcheck=3D""false"" role=3D""textbox"" aria-label=3D""false"" ari=
a-describedby=3D""cke_45""><span style=3D""font-size:16px""><span style=3D""font=
-family:Arial,Helvetica,sans-serif""><span style=3D""line-height:1.5""><span s=
tyle=3D""line-height:1.5""><span style=3D""font-family:Arial,Helvetica,sans-se=
rif""><span style=3D""line-height:1.5""><span style=3D""font-family:Arial,Helve=
tica,sans-serif""><span style=3D""background-color:#ffffcc"">18 =D0=B8=D1=8E=
=D0=BD=D1=8F, 19:00</span></span></span></span></span></span></span><br>
<span style=3D""line-height:1.5""><span style=3D""font-family:Arial,Helvetica,=
sans-serif""><strong>=D0=91=D0=B0=D0=BD=D0=BA=D0=BE=D0=B2=D1=81=D0=BA=D0=B8=
=D0=B9 =D0=B8=D0=BD=D1=81=D1=82=D0=B8=D1=82=D1=83=D1=82</strong></span></sp=
an><br>
<span style=3D""font-family:Arial,Helvetica,sans-serif""><span style=3D""line-=
height:1.5""><span style=3D""line-height:1.5""><span style=3D""font-family:Aria=
l,Helvetica,sans-serif""><span style=3D""line-height:1.5""><span style=3D""font=
-family:Arial,Helvetica,sans-serif""><span style=3D""font-size:16px""><span st=
yle=3D""font-family:Arial,Helvetica,sans-serif""><span style=3D""line-height:1=
.5""><span style=3D""line-height:1.5""><span style=3D""font-family:Arial,Helvet=
ica,sans-serif""><span style=3D""line-height:1.5""><span style=3D""font-family:=
Arial,Helvetica,sans-serif"">=D0=94=D0=B5=D0=BD=D1=8C =D0=BE=D1=82=D0=BA=D1=
=80=D1=8B=D1=82=D1=8B=D1=85 =D0=B4=D0=B2=D0=B5=D1=80=D0=B5=D0=B9 =D0=BF=D1=
=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC=D1=8B</span></span></span></span></=
span></span></span></span></span></span></span></span></span><br>
<span style=3D""line-height:1.5""><span style=3D""font-family:Arial,Helvetica,=
sans-serif""><a data-cke-saved-href=3D""https://www.hse.ru/ma/fin/"" href=3D""h=
ttps://unimail.hse.ru/ru/mail_link_tracker?hash=3D6bi3nfowpdcpurjnr33pasja8=
9w17rz1ua7fpnpgssets36zh6grehbppd45tacsy8itwbzsqrkigty6gt5f1gmfwtyknt3a4jmx=
9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L21hL2Zpbi8_dXRtX21lZGl1bT1=
lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjM0NjYzNjcw&uid=3DMTM=
yMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=A4=D0=B8=D0=BD=D0=B0=D0=
=BD=D1=81=D0=BE=D0=B2=D1=8B=D0=B9 =D0=B0=D0=BD=D0=B0=D0=BB=D0=B8=D1=82=D0=
=B8=D0=BA</a><br></span></span></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:14px;""><em><span style=3D""font-family:Arial, Helvetica, sans-ser=
if;""><span style=3D""line-height:1.5;"">=D0=95=D1=81=D0=BB=D0=B8 =D1=83 =D0=
=B2=D0=B0=D1=81 =D0=BF=D0=BE=D1=8F=D0=B2=D0=B8=D0=BB=D0=B8=D1=81=D1=8C =D0=
=B2=D0=BE=D0=BF=D1=80=D0=BE=D1=81=D1=8B =D0=B8=D0=BB=D0=B8 =D0=BF=D1=80=D0=
=B5=D0=B4=D0=BB=D0=BE=D0=B6=D0=B5=D0=BD=D0=B8=D1=8F, =D0=BD=D0=B0=D0=BF=D0=
=B8=D1=88=D0=B8=D1=82=D0=B5 =D0=BD=D0=B0=D0=BC: community@hse.ru.</span></s=
pan></em></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</center>
<table bgcolor=3D""white"" align=3D""left"" width=3D""100%""><tr><td><span style=
=3D""font-family: arial,helvetica,sans-serif; color: black; font-size: 12px;=
""><p style=3D""text-align: center; color: #bababa;"">=D0=A7=D1=82=D0=BE=D0=B1=
=D1=8B =D0=BE=D1=82=D0=BF=D0=B8=D1=81=D0=B0=D1=82=D1=8C=D1=81=D1=8F =D0=BE=
=D1=82 =D1=8D=D1=82=D0=BE=D0=B9 =D1=80=D0=B0=D1=81=D1=81=D1=8B=D0=BB=D0=BA=
=D0=B8, =D0=BF=D0=B5=D1=80=D0=B5=D0=B9=D0=B4=D0=B8=D1=82=D0=B5 =D0=BF=D0=BE=
 <a style=3D""color: #46a8c6;"" href=3D""https://unimail.hse.ru/ru/unsubscribe=
?hash=3D6er6qc88p8ypb3157fxz57m156w17rz1ua7fpnpgssets36zh6grf9rcdghp4eju4x9=
euguiwme1si85ox1u8n1qzyr#no_tracking"">=D1=81=D1=81=D1=8B=D0=BB=D0=BA=D0=B5<=
/a></p></span></td></tr></table><center><table><tr><td><img src=3D""https://=
unimail.hse.ru/ru/mail_read_tracker/1323674?hash=3D6rrc6w17y7q8aoiwgeupcxy5=
q8w17rz1ua7fpnpgssets36zh6grqodt9r56fw7sqp9bbmszibib9x9z1bcmncxy3ur"" width=
=3D""1"" height=3D""1"" alt=3D"""" title=3D"""" border=3D""0""></td></tr></table></ce=
nter></body>
</html>
";

        private const string BodyHse1 = @"<!DOCTYPE html>
<html>
<head>
<meta name=3D""viewport"" content=3D""width=3Ddevice-width, initial-scale=3D1""=
>
<title></title>

<style type=3D""text/css"">
/* ///////// CLIENT-SPECIFIC STYLES ///////// */
#outlook a{padding:0;} /* Force Outlook to provide a 'view in browser' mess=
age */
.ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail to d=
isplay emails at full width */
.ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font,=
 .ExternalClass td, .ExternalClass div {line-height: 100%;} /* Force Hotmai=
l to display normal line spacing */
body, table, td, p, a, li, blockquote{-webkit-text-size-adjust:100%; -ms-te=
xt-size-adjust:100%;} /* Prevent WebKit and Windows mobile changing default=
 text sizes */
table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;} /* Remove spacing be=
tween tables in Outlook 2007 and up */
img{-ms-interpolation-mode:bicubic;} /* Allow smoother rendering of resized=
 image in Internet Explorer */
/* ///////// RESET STYLES ///////// */
body{margin:0; padding:0;}
img{border:0; height:auto; line-height:100%; outline:none; text-decoration:=
none;}
table{border-collapse:collapse !important;}
table td { border-collapse: collapse !important;}
body, #bodyTable, #bodyCell{height:100% !important; margin:0; padding:0; wi=
dth:100% !important;}
#mailBody.mailBody .uni-block.button-block { display:block; } /* Specific u=
kr.net style*/
body {
margin: 0;
text-align: left;
}
pre {
white-space: pre;
white-space: pre-wrap;
word-wrap: break-word;
}
table.mhLetterPreview { width:100%; }
table {
mso-table-lspace:0pt;
mso-table-rspace:0pt;
}
img {
-ms-interpolation-mode:bicubic;
}
</style>

<style id=3D""media_css"" type=3D""text/css"">
@media all and (max-width: 480px), only screen and (max-device-width : 480p=
x) {
    body{width:100% !important; min-width:100% !important;} /* Prevent iOS =
Mail from adding padding to the body */
    table[class=3D'container-table'] {
       width:100% !important;
    }
    td.image-wrapper {
       padding: 0 !important;
    }
    td.image-wrapper, td.text-wrapper {
       display:block !important;
       width:100% !important;
       max-width:initial !important;
    }
    table[class=3D'wrapper1'] {
       table-layout: fixed !important;
       padding: 0 !important;
       max-width: 600px !important;
    }
    td[class=3D'wrapper-row'] {
       table-layout: fixed !important;
       box-sizing: border-box !important;
       width:100% !important;
       min-width:100% !important;
    }
    table[class=3D'wrapper2'] {
       table-layout: fixed !important;
       border: none !important;
       width: 100% !important;
       max-width: 600px !important;
       min-height: 520px !important;
    }
    div[class=3D'column-wrapper']{
       max-width:300px !important;
    }
    table.uni-block {
       max-width:100% !important;
       height:auto !important;
       min-height: auto !important;
    }
    table[class=3D'block-wrapper-inner-table'] {
       height:auto !important;
       min-height: auto !important;
    }
    td[class=3D'block-wrapper'] {
       height:auto !important;
       min-height: auto !important;
    }
    .submit-button-block .button-wrapper=20
{       height: auto !important;
       width: auto !important;
       min-height: initial !important;
       max-height: initial !important;
       min-width: initial !important;
       max-width: initial !important;
    }
    img[class=3D'image-element'] {
       height:auto !important;
       box-sizing: border-box !important;
    }
}
@media all and (max-width: 615px), only screen and (max-device-width : 615p=
x) {
    td[class=3D'wrapper-row'] {
       padding: 0 !important;
       margin: 0 !important;
    }
    .column {
       width:100% !important;
       max-width:100% !important;
    }
}
</style>
<meta http-equiv=3D""Content-Type"" content=3D""text/html;charset=3DUTF-8"">
</head>
<body width=3D""100%"" align=3D""center"">
<!--[if gte mso 9]>       <style type=3D""text/css"">           .uni-block im=
g {               display:block !important;           }       </style><![en=
dif]-->
<center>
<table cellpadding=3D""0"" cellspacing=3D""0"" align=3D""center"" width=3D""100%"" =
class=3D""container responsive"">
<tbody>
<tr>
<td>
<table cellpadding=3D""0"" cellspacing=3D""0"" align=3D""center"" class=3D""wrappe=
r1"" style=3D""width: 100%; box-sizing: border-box; background-color: rgb(234=
, 244, 255); float: left;"">
<tbody>
<tr>
<td class=3D""wrapper-row"" style=3D""padding: 25px;""><!--[if (gte mso 9)|(IE)=
]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" ali=
gn=3D""center""><tr><td><![endif]-->
<table cellpadding=3D""0"" cellspacing=3D""0"" class=3D""wrapper2"" align=3D""cent=
er"" style=3D""background-color: rgb(255, 255, 255); border-radius: 3px; max-=
width: 600px; width: 100%; border: none; margin: 0px auto; border-spacing: =
0px; border-collapse: collapse;"">
<tbody>
<tr>
<td width=3D""100%"" class=3D""wrapper-row"" style=3D""vertical-align: top; max-=
width: 600px; font-size: 0px; padding: 0px;""><!--[if (gte mso 9)|(IE)]><tab=
le cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""=
center""><tr><td><![endif]-->
<table class=3D""uni-block social-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: right; height: 0px;"" class=3D""block-w=
rapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 0px;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px; background-image: none; text-a=
lign: right;"" class=3D""content-wrapper""><span class=3D""networks-wrapper""><s=
pan class=3D""scl-button scl-fb""><a href=3D""https://unimail.hse.ru/ru/mail_l=
ink_tracker?hash=3D6r1e54jz51hhz1jnr33pasja89w17rz1ua7fpnpymggcf97cy46p363k=
arw1hxj9h8apmfoft963wwdowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM=
6Ly93d3cuZmFjZWJvb2suY29tL2hzZS5ydT91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW=
5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMzQ1OTkwNTY~&uid=3DMTMyMzY3NA=3D=3D"" target=
=3D""_blank""><img style=3D""max-height:64px;max-width:64px;"" src=3D""http://un=
imail.hse.ru/v5/img/ico/scl/fcb.png"" alt=3D""Facebook""></a></span> <span cla=
ss=3D""scl-button scl-inst""><a href=3D""https://unimail.hse.ru/ru/mail_link_t=
racker?hash=3D6kzpx46hhr6fcojnr33pasja89w17rz1ua7fpnpymggcf97cy46pwqmpy9m6r=
4bxffcq8xzp6ecisgdowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93=
d3cuaW5zdGFncmFtLmNvbS9oc2VfcnUvP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1Vbml=
TZW5kZXImdXRtX2NhbXBhaWduPTIzNDU5OTA1Ng~~&uid=3DMTMyMzY3NA=3D=3D"" target=3D=
""_blank""><img style=3D""max-height:64px;max-width:64px;"" src=3D""http://unima=
il.hse.ru/v5/img/ico/scl/inst.png"" alt=3D""Instagram""></a></span> <span clas=
s=3D""scl-button scl-vk""><a href=3D""https://unimail.hse.ru/ru/mail_link_trac=
ker?hash=3D63m8x35ygshs4gjnr33pasja89w17rz1ua7fpnpymggcf97cy46p9pu34ttbgam9=
36uroarhx7sw8ydowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly92ay5=
jb20vaHNlX3VuaXZlcnNpdHk_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlci=
Z1dG1fY2FtcGFpZ249MjM0NTk5MDU2&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank""><i=
mg style=3D""max-height:64px;max-width:64px;"" src=3D""http://unimail.hse.ru/v=
5/img/ico/scl/vk.png"" alt=3D""=D0=92=D0=BA=D0=BE=D0=BD=D1=82=D0=B0=D0=BA=D1=
=82=D0=B5""></a></span> <span class=3D""scl-button scl-yt""><a href=3D""https:/=
/unimail.hse.ru/ru/mail_link_tracker?hash=3D6gqp6id76mzfdqjnr33pasja89w17rz=
1ua7fpnpymggcf97cy46pawrwbq7pxs3drsb3uwwamenq66dowuby7eb1cf5ge75tbx3h5j71sy=
wopdfiu6auo&url=3DaHR0cHM6Ly93d3cueW91dHViZS5jb20vdXNlci9oc2U_dXRtX21lZGl1b=
T1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjM0NTk5MDU2&uid=3DM=
TMyMzY3NA=3D=3D"" target=3D""_blank""><img style=3D""max-height:64px;max-width:=
64px;"" src=3D""http://unimail.hse.ru/v5/img/ico/scl/yt.png"" alt=3D""Youtube"">=
</a></span> <span class=3D""scl-button scl-custom""><a href=3D""https://unimai=
l.hse.ru/ru/mail_link_tracker?hash=3D6aqtuzj4k7a99hjnr33pasja89w17rz1ua7fpn=
pymggcf97cy46p1u9hkdapcobjphumh6ybb7ks1adowuby7eb1cf5ge75tbx3h5j71sywopdfiu=
6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci8_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291=
cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjM0NTk5MDU2&uid=3DMTMyMzY3NA=3D=3D"" ta=
rget=3D""_blank""><img style=3D""max-height:64px;max-width:64px;"" src=3D""http:=
//unimail.hse.ru/v5/img/ico/scl/custom.png"" alt=3D""=D0=9C=D0=BE=D0=B9 =D1=
=81=D0=B0=D0=B9=D1=82""></a></span></span></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 69.2px; width: 100%; table-layout: fixed;=
 border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px; vertical-align: middle; font-s=
ize: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; co=
lor: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix cke_editable cke_editable_inline cke_contents_ltr ck=
e_show_borders"" style=3D""overflow-wrap: break-word; position: relative;"" ta=
bindex=3D""0"" spellcheck=3D""false"" role=3D""textbox"" aria-label=3D""false"" ari=
a-describedby=3D""cke_45"">
<div style=3D""text-align:center""><span style=3D""font-size:26px;""><span styl=
e=3D""font-family:Arial,Helvetica,sans-serif""><strong><span style=3D""line-he=
ight:1.5""><span style=3D""font-family:Arial,Helvetica,sans-serif"">=D0=9F=D1=
=80=D0=BE=D0=BA=D1=82=D0=BE=D1=80=D0=B8=D0=BD=D0=B3: =D0=B2=D0=BE=D0=BF=D1=
=80=D0=BE=D1=81=D1=8B =D0=B8 =D0=BE=D1=82=D0=B2=D0=B5=D1=82=D1=8B</span></s=
pan></strong></span></span><br></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block image-block"" width=3D""100%"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heig=
ht: auto; border-collapse: collapse; border-spacing: 0px; display: inline-t=
able; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; padding: 0px; height: 100=
%;"" class=3D""block-wrapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 404px; width: 100%; table-layout: fixed; =
text-align: center; border-spacing: 0px; border-collapse: collapse; font-si=
ze: 0px;"">
<tbody>
<tr>
<td style=3D""width: auto; height: 100%; display: inline-table;"" class=3D""co=
ntent-wrapper"">
<table class=3D""content-box"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""=
0"" style=3D""display: inline-table; vertical-align: top; width: auto; height=
: 100%; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""vertical-align: middle;"">
<div class=3D""image-wrapper image-drop""><a class=3D""image-link"" href=3D""jav=
ascript:;"" target=3D""_self""><img class=3D""image-element"" src=3D""http://unim=
ail.hse.ru/ru/user_file?resource=3Dhimg&user_id=3D1323674&name=3D6bnm8fywi8=
c4wxpiu3azzngxco47cfqz5xza57go1ayumsem5facm347ujwynpqa1fkpwj3twrfgry"" alt=
=3D""Some Image"" id=3D""gridster_block_419_main_img"" style=3D""font-size: smal=
l; border: none; width: 100%; max-width: 600px; height: auto; max-height: 4=
00px; outline: none; text-decoration: none;"" width=3D""600""></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px 5px; vertical-align: top; font=
-size: 16px; font-family: Arial, Helvetica, sans-serif; line-height: 19.2px=
; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:justify;""><span style=3D""line-height:1.5;""><span s=
tyle=3D""font-size:18px;""><strong><span style=3D""font-family:Arial, Helvetic=
a, sans-serif;"">=D0=94=D1=80=D1=83=D0=B7=D1=8C=D1=8F!</span></strong></span=
></span><br>
&nbsp;</div>
<div><span style=3D""line-height:1.5;""><span style=3D""font-size:16px;""><span=
 style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=97=D0=B0 =D0=BF=D1=
=80=D0=BE=D1=88=D0=BB=D1=83=D1=8E =D0=BD=D0=B5=D0=B4=D0=B5=D0=BB=D1=8E =D0=
=B1=D1=8B=D0=BB=D0=BE =D0=BC=D0=BD=D0=BE=D0=B3=D0=BE =D0=BE=D0=B1=D1=81=D1=
=83=D0=B6=D0=B4=D0=B5=D0=BD=D0=B8=D0=B9 =D1=8D=D0=BA=D0=B7=D0=B0=D0=BC=D0=
=B5=D0=BD=D0=BE=D0=B2 =D1=81 =D0=BF=D1=80=D0=BE=D0=BA=D1=82=D0=BE=D1=80=D0=
=B8=D0=BD=D0=B3=D0=BE=D0=BC, =D1=83=D0=BD=D0=B8=D0=B2=D0=B5=D1=80=D1=81=D0=
=B8=D1=82=D0=B5=D1=82 =D1=80=D0=B0=D0=B7=D0=BC=D0=B5=D1=81=D1=82=D0=B8=D0=
=BB =D0=B1=D0=BE=D0=BB=D1=8C=D1=88=D0=BE=D0=B5 =D0=BA=D0=BE=D0=BB=D0=B8=D1=
=87=D0=B5=D1=81=D1=82=D0=B2=D0=BE =D0=B8=D0=BD=D1=81=D1=82=D1=80=D1=83=D0=
=BA=D1=86=D0=B8=D0=B9 =D0=B8 =D1=80=D0=B5=D0=BA=D0=BE=D0=BC=D0=B5=D0=BD=D0=
=B4=D0=B0=D1=86=D0=B8=D0=B9 =D0=BF=D0=BE =D0=BF=D1=80=D0=BE=D1=85=D0=BE=D0=
=B6=D0=B4=D0=B5=D0=BD=D0=B8=D1=8E =D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D1=
=8D=D0=BA=D0=B7=D0=B0=D0=BC=D0=B5=D0=BD=D0=BE=D0=B2 =D0=B2 =D1=81=D0=B8=D1=
=81=D1=82=D0=B5=D0=BC=D0=B5 =C2=AB=D0=AD=D0=BA=D0=B7=D0=B0=D0=BC=D1=83=D1=
=81=C2=BB.&nbsp;<br>
<br>
=D0=9C=D1=8B =D0=BF=D0=BE=D0=BD=D0=B8=D0=BC=D0=B0=D0=B5=D0=BC, =D1=87=D1=82=
=D0=BE =D0=B2=D1=8B =D0=B7=D0=B0=D0=BD=D1=8F=D1=82=D1=8B =D0=BF=D0=BE=D0=B4=
=D0=B3=D0=BE=D1=82=D0=BE=D0=B2=D0=BA=D0=BE=D0=B9 =D0=BA =D1=81=D0=B5=D1=81=
=D1=81=D0=B8=D0=B8 =D0=B8 =D1=83 =D0=B2=D0=B0=D1=81 =D0=BC=D0=BE=D0=B3=D0=
=BB=D0=BE =D0=BD=D0=B5 =D0=B1=D1=8B=D1=82=D1=8C =D0=B2=D1=80=D0=B5=D0=BC=D0=
=B5=D0=BD=D0=B8 =D0=BE=D0=B7=D0=BD=D0=B0=D0=BA=D0=BE=D0=BC=D0=B8=D1=82=D1=
=8C=D1=81=D1=8F =D1=81=D0=BE =D0=B2=D1=81=D0=B5=D0=BC=D0=B8 =D1=81=D1=81=D1=
=8B=D0=BB=D0=BA=D0=B0=D0=BC=D0=B8 =D0=B8 =D0=BC=D0=B0=D1=82=D0=B5=D1=80=D0=
=B8=D0=B0=D0=BB=D0=B0=D0=BC=D0=B8.&nbsp;<br>
<br>
=D0=9F=D0=BE=D1=8D=D1=82=D0=BE=D0=BC=D1=83 =D0=92=D1=8B=D1=88=D0=BA=D0=B0 =
=D0=9E=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD =D1=81=D0=BE=D0=B2=D0=BC=D0=B5=D1=81=
=D1=82=D0=BD=D0=BE =D1=81 =D0=94=D0=B8=D1=80=D0=B5=D0=BA=D1=86=D0=B8=D0=B5=
=D0=B9 =D0=BE=D1=81=D0=BD=D0=BE=D0=B2=D0=BD=D1=8B=D1=85 =D0=BE=D0=B1=D1=80=
=D0=B0=D0=B7=D0=BE=D0=B2=D0=B0=D1=82=D0=B5=D0=BB=D1=8C=D0=BD=D1=8B=D1=85 =
=D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC =D1=81=D0=BE=D0=B1=D1=80=
=D0=B0=D0=BB=D0=B8 =D0=B4=D0=BB=D1=8F =D0=B2=D0=B0=D1=81 =D0=B2=D0=BE=D0=B5=
=D0=B4=D0=B8=D0=BD=D0=BE =D0=B2=D1=81=D0=B5, =D1=87=D1=82=D0=BE =D0=BF=D0=
=BE=D0=B7=D0=B2=D0=BE=D0=BB=D0=B8=D1=82 =D0=B2=D0=B0=D0=BC =D0=BF=D0=BE=D1=
=87=D1=83=D0=B2=D1=81=D1=82=D0=B2=D0=BE=D0=B2=D0=B0=D1=82=D1=8C =D1=81=D0=
=B5=D0=B1=D1=8F =D0=B1=D0=BE=D0=BB=D0=B5=D0=B5 =D0=BF=D0=BE=D0=B4=D0=B3=D0=
=BE=D1=82=D0=BE=D0=B2=D0=BB=D0=B5=D0=BD=D0=BD=D1=8B=D0=BC=D0=B8 =D0=BA =D0=
=BF=D1=80=D0=B5=D0=B4=D1=81=D1=82=D0=BE=D1=8F=D1=89=D0=B8=D0=BC =D1=8D=D0=
=BA=D0=B7=D0=B0=D0=BC=D0=B5=D0=BD=D0=B0=D1=86=D0=B8=D0=BE=D0=BD=D0=BD=D1=8B=
=D0=BC =D0=B8=D1=81=D0=BF=D1=8B=D1=82=D0=B0=D0=BD=D0=B8=D1=8F=D0=BC:</span>=
</span></span></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 167px; width: 100%; table-layout: fixed; =
border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px 5px; vertical-align: top; font=
-size: 16px; font-family: Arial, Helvetica, sans-serif; line-height: 19.2px=
; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<ul>
<li><a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6e6fxzfsu=
hpap6jnr33pasja89w17rz1ua7fpnpymggcf97cy46p76gig9wcfjx6nzpynyxfzi1xcodowuby=
7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly9lbGVhcm5pbmcuaHNlLnJ1L3B=
yb2N0b3JpbmdfaW5zdHJ1dGlvbj91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZG=
VyJnV0bV9jYW1wYWlnbj0yMzQ1OTkwNTY~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:r=
gb(0,127,255);""><span style=3D""line-height:1.5;""><span style=3D""font-size:1=
6px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=9F=D1=
=80=D0=B0=D0=B2=D0=B8=D0=BB=D0=B0 =D0=BF=D1=80=D0=BE=D0=B2=D0=B5=D0=B4=D0=
=B5=D0=BD=D0=B8=D1=8F =D1=8D=D0=BA=D0=B7=D0=B0=D0=BC=D0=B5=D0=BD=D0=B0 =D1=
=81 =D0=BF=D1=80=D0=BE=D0=BA=D1=82=D0=BE=D1=80=D0=B8=D0=BD=D0=B3=D0=BE=D0=
=BC</span></span></span></a></li>
<li><a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6yk97985h=
hr63qjnr33pasja89w17rz1ua7fpnpymggcf97cy46pwicpbg4jzeatcsoth75b6dy6sndowuby=
7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L3N0dWR5c3B=
yYXZrYS9kaXN0YW5jZV9zdHVkX2V4YW11cz91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW=
5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMzQ1OTkwNTY~&uid=3DMTMyMzY3NA=3D=3D"" style=3D=
""color:rgb(0,127,255);""><span style=3D""line-height:1.5;""><span style=3D""fon=
t-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=
=90=D0=BB=D0=B3=D0=BE=D1=80=D0=B8=D1=82=D0=BC =D0=BF=D0=BE=D0=B4=D0=B3=D0=
=BE=D1=82=D0=BE=D0=B2=D0=BA=D0=B8 =D0=B8 =D0=BF=D1=80=D0=BE=D1=85=D0=BE=D0=
=B6=D0=B4=D0=B5=D0=BD=D0=B8=D1=8F =D1=8D=D0=BA=D0=B7=D0=B0=D0=BC=D0=B5=D0=
=BD=D0=B0 =D1=81 =D0=BF=D1=80=D0=BE=D0=BA=D1=82=D0=BE=D1=80=D0=B8=D0=BD=D0=
=B3=D0=BE=D0=BC =D0=B2 =D1=81=D0=B8=D1=81=D1=82=D0=B5=D0=BC=D0=B5 =C2=ABExa=
mus=C2=BB</span></span></span></a></li>
<li><a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6am89hw9n=
1snc1jnr33pasja89w17rz1ua7fpnpymggcf97cy46paaz9brj16m3bxuw4r9pbwnekb6dowuby=
7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly9lbGVhcm5pbmcuaHNlLnJ1L3N=
0dWRlbnRfc3RlcHM_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2=
FtcGFpZ249MjM0NTk5MDU2&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255=
);""><span style=3D""line-height:1.5;""><span style=3D""font-size:16px;""><span =
style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=A0=D0=B0=D1=81=D1=
=88=D0=B8=D1=80=D0=B5=D0=BD=D0=BD=D1=8B=D0=B5 =D0=B8=D0=BD=D1=81=D1=82=D1=
=80=D1=83=D0=BA=D1=86=D0=B8=D0=B8 =D0=B4=D0=BB=D1=8F =D1=81=D1=82=D1=83=D0=
=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=B2 =D0=9D=D0=98=D0=A3 =D0=92=D0=A8=D0=AD</sp=
an></span></span></a></li>
<li><a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D68etzp4z3=
ejm5sjnr33pasja89w17rz1ua7fpnpymggcf97cy46p1sy1zfkpu8ai7gtb99kywsu51febi6a8=
anb87m9dtirakmdk5puoaf9gdgqkkoqko&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L3N0dWR5c3B=
yYXZrYS9kaXN0Y29udHJvbD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJn=
V0bV9jYW1wYWlnbj0yMzQ1OTkwNTY~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0=
,127,255);""><span style=3D""line-height:1.5;""><span style=3D""font-size:16px;=
""><span style=3D""font-family:Arial, Helvetica, sans-serif;"">=D0=90=D0=BB=D0=
=B3=D0=BE=D1=80=D0=B8=D1=82=D0=BC =D0=BF=D1=80=D0=B8 =D0=B2=D0=BE=D0=B7=D0=
=BD=D0=B8=D0=BA=D0=BD=D0=BE=D0=B2=D0=B5=D0=BD=D0=B8=D0=B8 =D1=82=D0=B5=D1=
=85=D0=BD=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D1=85 =D1=82=D1=80=D1=83=D0=
=B4=D0=BD=D0=BE=D1=81=D1=82=D0=B5=D0=B9</span></span></span></a></li>
</ul>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px 5px; vertical-align: top; font=
-size: 16px; font-family: Arial, Helvetica, sans-serif; line-height: 19.2px=
; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:justify;""><span style=3D""line-height:1.5;""><span s=
tyle=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-=
serif;""><span style=3D""line-height:1.5;""><span style=3D""font-size:18px;""><s=
pan style=3D""font-family:Arial, Helvetica, sans-serif;""><strong>=D0=A0=D0=
=BE=D0=BB=D0=B8=D0=BA=D0=B8</strong></span></span></span></span></span></sp=
an></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block container-block"" width=3D""100%"" border=3D""0"" cell=
spacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; =
height: auto; border-collapse: collapse; background-image: none; border-spa=
cing: 0px;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px; border: none;"" class=3D""block-wrap=
per"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width:100%;"" class=3D""content-wrapper"">
<table class=3D""container-table content-box"" border=3D""0"" cellspacing=3D""0""=
 cellpadding=3D""0"" id=3D""JColResizer0"" style=3D""width: 100%; height: 100%; =
border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr class=3D""container-row"">
<td class=3D""td-wrapper"" style=3D""font-size:0;vertical-align:top;"" align=3D=
""center""><!--[if (gte mso 9)|(IE)]><table width=3D""100%"" cellpadding=3D""0"" =
cellspacing=3D""0"" border=3D""0""><tr><td width=3D""290px"" valign=3D""top""><![en=
dif]-->
<table class=3D""column"" cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" wi=
dth=3D""100%"" style=3D""height: auto; vertical-align: top; display: inline-ta=
ble; max-width: 290px; border-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""sortable-container ui-sortable"" style=3D""vertical-align: top; =
width: 100%;"" align=3D""center"" valign=3D""top"">
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px; vertical-align: top; font-size=
: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; color=
: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:center;""><a href=3D""https://unimail.hse.ru/ru/mail=
_link_tracker?hash=3D6yuhs9i41xod6ojnr33pasja89w17rz1ua7fpnpymggcf97cy46pa6=
4czksu65dsb83wg4i1auf13iiymni3oot4gwcknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0c=
HM6Ly93d3cueW91dHViZS5jb20vd2F0Y2g_dj15cTVOREFwNFFwYyZ1dG1fbWVkaXVtPWVtYWls=
JnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMzQ1OTkwNTY~&uid=3DMTMyMzY3=
NA=3D=3D"" style=3D""color:rgb(0,127,255);""><strong><span style=3D""line-heigh=
t:1.5;""><span style=3D""font-size:16px;""><span style=3D""font-family:Arial, H=
elvetica, sans-serif;"">=D0=A1=D0=B8=D0=BD=D1=85=D1=80=D0=BE=D0=BD=D0=BD=D1=
=8B=D0=B9 =D0=BF=D1=80=D0=BE=D0=BA=D1=82=D0=BE=D1=80=D0=B8=D0=BD=D0=B3</spa=
n></span></span></strong></a></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table class=3D""uni-block video-block"" width=3D""100%"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heig=
ht: auto; border-collapse: collapse; border-spacing: 0px; display: inline-t=
able; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; padding: 0px; height: 100=
%;"" class=3D""block-wrapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 150px; width: 100%; table-layout: fixed; =
border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle;"" class=3D""content-wrapper=
"">
<table class=3D""content-box"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""=
0"" align=3D""center"" style=3D""table-layout: fixed; border-spacing: 0px; bord=
er-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""thumbnail-sector"" style=3D""vertical-align:top;"">
<div class=3D""link-wrapper"" style=3D""position: relative;""><a class=3D""image=
-link"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6uoonbmd9=
tqgjojnr33pasja89w17rz1ua7fpnpymggcf97cy46p3579zeygubp7gozzdi1y7zfz9juxfk9z=
pdzjsnaknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly95b3V0dS5iZS95cTVOREFwNFF=
wYz91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMz=
Q1OTkwNTY~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank"" style=3D""text-align: l=
eft;""><img class=3D""image-element"" src=3D""http://unimail.hse.ru/ru/user_fil=
e?resource=3Dhvt&user_id=3D1323674&name=3D6jtrc45snrcxoppiu3azzngxco385pj7i=
cjqngizz43bdo33ts15pczqk1qcpqu8rqhf17qjqwcai1zsp63s4gpeph3464ijckhidzq63tre=
ei3xrhcfo"" alt=3D""Some Video"" style=3D""width: auto; font-size: small; borde=
r: none; max-width: 200px; height: auto; max-height: 150px; outline: none; =
text-decoration: none;"" width=3D""200""></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td><td width=3D""289px"" valign=3D""top""><![endif]=
-->
<table class=3D""column"" cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" wi=
dth=3D""100%"" style=3D""height: auto; vertical-align: top; display: inline-ta=
ble; max-width: 289px; border-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""sortable-container ui-sortable"" style=3D""vertical-align: top; =
width: 100%;"" align=3D""center"" valign=3D""top"">
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8=
px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:center;""><a href=3D""https://unimail.hse.ru/ru/mail=
_link_tracker?hash=3D67kb133gw6c3n6jnr33pasja89w17rz1ua7fpnpymggcf97cy46ph1=
fmj69rsn69dgqq95oyxu4r74gftcrc1foxceyknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0c=
HM6Ly93d3cueW91dHViZS5jb20vd2F0Y2g_dj1EOUxIZ2lRQTdwMCZ1dG1fbWVkaXVtPWVtYWls=
JnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMzQ1OTkwNTY~&uid=3DMTMyMzY3=
NA=3D=3D"" style=3D""color:rgb(0,127,255);""><strong><span style=3D""font-famil=
y:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><span sty=
le=3D""font-size:16px;"">=D0=90=D1=81=D0=B8=D0=BD=D1=85=D1=80=D0=BE=D0=BD=D0=
=BD=D1=8B=D0=B9 =D0=BF=D1=80=D0=BE=D0=BA=D1=82=D0=BE=D1=80=D0=B8=D0=BD=D0=
=B3</span></span></span></strong></a></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<table class=3D""uni-block video-block"" width=3D""100%"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heig=
ht: auto; border-collapse: collapse; border-spacing: 0px; display: inline-t=
able; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; padding: 0px; height: 100=
%;"" class=3D""block-wrapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 150px; width: 100%; table-layout: fixed; =
border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle;"" class=3D""content-wrapper=
"">
<table class=3D""content-box"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""=
0"" align=3D""center"" style=3D""table-layout: fixed; border-spacing: 0px; bord=
er-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""thumbnail-sector"" style=3D""vertical-align:top;"">
<div class=3D""link-wrapper"" style=3D""position: relative;""><a class=3D""image=
-link"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6sk7q5cjo=
jmywrjnr33pasja89w17rz1ua7fpnpymggcf97cy46pugt5948hnaeeqana1yohyhmje3iymni3=
oot4gwcknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly95b3V0dS5iZS9EOUxIZ2lRQTd=
wMD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMz=
Q1OTkwNTY~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank"" style=3D""text-align: l=
eft;""><img class=3D""image-element"" src=3D""http://unimail.hse.ru/ru/user_fil=
e?resource=3Dhvt&user_id=3D1323674&name=3D6r4gsg1hmfne9ppiu3azzngxco575bauo=
nzf3z3w6x4phnk1fabd1o7wn51f57j5d6c5re8id454xymg9qg4ormz9pkrxbornhnkxym6adak=
p9ay16nso"" alt=3D""Some Video"" style=3D""width: auto; font-size: small; borde=
r: none; max-width: 200px; height: auto; max-height: 150px; outline: none; =
text-decoration: none;"" width=3D""200""></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 50.4px; width: 100%; table-layout: fixed;=
 border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px 5px; vertical-align: top; font=
-size: 16px; font-family: Arial, Helvetica, sans-serif; line-height: 19.2px=
; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:center;""><a href=3D""https://unimail.hse.ru/ru/mail=
_link_tracker?hash=3D653sedmcmtd4aejnr33pasja89w17rz1ua7fpnpymggcf97cy46poh=
aaugrgph866j5d4qse5kqeyynsnyzrngg11eoknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0c=
HM6Ly9kcml2ZS5nb29nbGUuY29tL2ZpbGUvZC8xV0dNM1N3WjlSLVBKVE9ZaTY4SmVoYm5pUlRC=
Wmt6R0Uvdmlldz91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1=
wYWlnbj0yMzQ1OTkwNTY~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255)=
;""><span style=3D""font-size:16px;""><span style=3D""line-height:1.5;""><span s=
tyle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-heig=
ht:1.5;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""><strong>=
=D0=94=D0=B5=D0=BC=D0=BE-=D0=BF=D1=80=D0=BE=D1=85=D0=BE=D0=B6=D0=B4=D0=B5=
=D0=BD=D0=B8=D0=B5 =D1=8D=D0=BA=D0=B7=D0=B0=D0=BC=D0=B5=D0=BD=D0=B0</strong=
></span></span></span></span></span></a></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px 5px; vertical-align: top; font=
-size: 16px; font-family: Arial, Helvetica, sans-serif; line-height: 19.2px=
; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:justify;""><span style=3D""line-height:1.5;""><span s=
tyle=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-=
serif;""><span style=3D""line-height:1.5;""><span style=3D""font-size:18px;""><s=
pan style=3D""font-family:Arial, Helvetica, sans-serif;""><strong>=D0=92=D0=
=BE=D0=BF=D1=80=D0=BE=D1=81-=D0=BE=D1=82=D0=B2=D0=B5=D1=82&nbsp;</strong></=
span></span><br>
<span style=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica=
, sans-serif;""><a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=
=3D6ruy43oo9s6ctgjnr33pasja89w17rz1ua7fpnpymggcf97cy46phpqcjje74o4guk3bz56b=
a7y38agftcrc1foxceyknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9lbGVhcm5pbmc=
uaHNlLnJ1L3Byb2N0b3JpbmdfZmFxX3N0dWRlbnQ_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cm=
NlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjM0NTk5MDU2&uid=3DMTMyMzY3NA=3D=3D"" styl=
e=3D""color:rgb(0,127,255);"">https://elearning.hse.ru/proctoring_faq_student=
</a><br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6z9r1p5juntxa=
1jnr33pasja89w17rz1ua7fpnpymggcf97cy46phgh8j9twcuiqdr8itg3q51dpti6ardjf311q=
9raknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L3N0dWR5c3ByYXZ=
rYS9kaXN0YW5jZV9zdHVkX3BycXVlc3Rpb25zP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT=
1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzNDU5OTA1Ng~~&uid=3DMTMyMzY3NA=3D=3D"" styl=
e=3D""color:rgb(0,127,255);"">https://www.hse.ru/studyspravka/distance_stud_p=
rquestions</a><br>
<br>
=D0=95=D1=81=D0=BB=D0=B8 =D1=83 =D0=B2=D0=B0=D1=81 =D0=BE=D1=81=D1=82=D0=B0=
=D0=BB=D0=B8=D1=81=D1=8C =D0=B2=D0=BE=D0=BF=D1=80=D0=BE=D1=81=D1=8B, =D0=BF=
=D0=B8=D1=88=D0=B8=D1=82=D0=B5: <a href=3D""mailto:elearn@hse.ru"" style=3D""c=
olor:rgb(0,127,255);"">elearn@hse.ru</a></span></span></span></span></span><=
/span></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 86.4px; heigh=
t: 86.4px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 86.4px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 30px; background-color: rgb(2=
52, 162, 83); height: 50.4px; min-height: 50.4px;""><a class=3D""mailbtn"" hre=
f=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6ac4fnk5xnje1kjnr33=
pasja89w17rz1ua7fpnpymggcf97cy46p6p7d15n38nnmxrnbz4fgt7bnniiymni3oot4gwcknt=
3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L3N0dWR5c3ByYXZrYS9ka=
XN0YW5jZWxlYXJuaW5nX3N0dWQ_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRl=
ciZ1dG1fY2FtcGFpZ249MjM0NTk5MDU2&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank"" =
style=3D""width:100%;display:inline-block;text-decoration:none;""><span class=
=3D""btn-inner"" style=3D""display: inline; font-size: 16px; font-family: Aria=
l, Helvetica, sans-serif; line-height: 19.2px; color: rgb(255, 255, 255); b=
ackground-color: rgb(252, 162, 83); border: 0px; word-break: break-all;"">=
=D0=91=D0=BE=D0=BB=D1=8C=D1=88=D0=B5 =D0=BF=D0=BE=D0=BB=D0=B5=D0=B7=D0=BD=
=D1=8B=D1=85 =D0=BC=D0=B0=D1=82=D0=B5=D1=80=D0=B8=D0=B0=D0=BB=D0=BE=D0=B2</=
span></a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 92px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px 5px; vertical-align: top; font=
-size: 16px; font-family: Arial, Helvetica, sans-serif; line-height: 19.2px=
; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:justify;""><span style=3D""line-height:1.5;""><span s=
tyle=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-=
serif;"">=D0=9E=D1=81=D1=82=D0=B0=D0=B5=D0=BC=D1=81=D1=8F =D0=BD=D0=B0 =D1=
=81=D0=B2=D1=8F=D0=B7=D0=B8,&nbsp;<br>
=D0=92=D1=81=D0=B5=D0=B3=D0=B4=D0=B0 =D0=B2=D0=B0=D1=88=D0=B8<br>
=D0=92=D1=8B=D1=88=D0=BA=D0=B0 =D0=9E=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD =D0=B8 =
=D0=94=D0=B8=D1=80=D0=B5=D0=BA=D1=86=D0=B8=D1=8F =D0=BE=D1=81=D0=BD=D0=BE=
=D0=B2=D0=BD=D1=8B=D1=85 =D0=BE=D0=B1=D1=80=D0=B0=D0=B7=D0=BE=D0=B2=D0=B0=
=D1=82=D0=B5=D0=BB=D1=8C=D0=BD=D1=8B=D1=85 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=
=D0=B0=D0=BC=D0=BC</span></span></span></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 10px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 10px; min-height:=
 10px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(25=
5, 255, 255); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</center>
<table bgcolor=3D""white"" align=3D""left"" width=3D""100%""><tr><td><span style=
=3D""font-family: arial,helvetica,sans-serif; color: black; font-size: 12px;=
""><p style=3D""text-align: center; color: #bababa;"">=D0=A7=D1=82=D0=BE=D0=B1=
=D1=8B =D0=BE=D1=82=D0=BF=D0=B8=D1=81=D0=B0=D1=82=D1=8C=D1=81=D1=8F =D0=BE=
=D1=82 =D1=8D=D1=82=D0=BE=D0=B9 =D1=80=D0=B0=D1=81=D1=81=D1=8B=D0=BB=D0=BA=
=D0=B8, =D0=BF=D0=B5=D1=80=D0=B5=D0=B9=D0=B4=D0=B8=D1=82=D0=B5 =D0=BF=D0=BE=
 <a style=3D""color: #46a8c6;"" href=3D""https://unimail.hse.ru/ru/unsubscribe=
?hash=3D6f5iytc3kc5b1x157fxz57m156w17rz1ua7fpnpymggcf97cy46p7wt8yfbsigsimqo=
8ou1jirhx7785ox1u8n1qzyr#no_tracking"">=D1=81=D1=81=D1=8B=D0=BB=D0=BA=D0=B5<=
/a></p></span></td></tr></table><center><table><tr><td><img src=3D""https://=
unimail.hse.ru/ru/mail_read_tracker/1323674?hash=3D6twbjcqnriwe3siwgeupcxy5=
q8w17rz1ua7fpnpymggcf97cy46poqbs1xbc5n4zfp9bbmszibib9x9z1bcmncxy3ur"" width=
=3D""1"" height=3D""1"" alt=3D"""" title=3D"""" border=3D""0""></td></tr></table></ce=
nter></body>
</html>";

        private const string BodyHse2 = @"<!DOCTYPE html><html><head><meta name=3D""viewport"" content=3D""width=3Ddevic=
e-width, initial-scale=3D1""><title></title><style type=3D""text/css"">/* ////=
///// CLIENT-SPECIFIC STYLES ///////// */#outlook a{padding:0;} /* Force Ou=
tlook to provide a 'view in browser' message */.ReadMsgBody{width:100%;} .E=
xternalClass{width:100%;} /* Force Hotmail to display emails at full width =
*/.ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass fon=
t, .ExternalClass td, .ExternalClass div {line-height: 100%;} /* Force Hotm=
ail to display normal line spacing */body, table, td, p, a, li, blockquote{=
-webkit-text-size-adjust:100%; -ms-text-size-adjust:100%;} /* Prevent WebKi=
t and Windows mobile changing default text sizes */table, td{mso-table-lspa=
ce:0pt; mso-table-rspace:0pt;} /* Remove spacing between tables in Outlook =
2007 and up */img{-ms-interpolation-mode:bicubic;} /* Allow smoother render=
ing of resized image in Internet Explorer *//* ///////// RESET STYLES /////=
//// */body{margin:0; padding:0;}img{border:0; height:auto; line-height:100=
%; outline:none; text-decoration:none;}table{border-collapse:collapse !impo=
rtant;}table td { border-collapse: collapse !important;}body, #bodyTable, #=
bodyCell{height:100% !important; margin:0; padding:0; width:100% !important=
;}#mailBody.mailBody .uni-block.button-block { display:block; } /* Specific=
 ukr.net style*/body {margin: 0;text-align: left;}pre {white-space: pre;whi=
te-space: pre-wrap;word-wrap: break-word;}table.mhLetterPreview { width:100=
%; }table {mso-table-lspace:0pt;mso-table-rspace:0pt;}img {-ms-interpolatio=
n-mode:bicubic;}</style><style id=3D""media_css"" type=3D""text/css"">@media al=
l and (max-width: 480px), only screen and (max-device-width : 480px) {    b=
ody{width:100% !important; min-width:100% !important;} /* Prevent iOS Mail =
from adding padding to the body */    table[class=3D'container-table'] {   =
    width:100% !important;    }    td.image-wrapper {       padding: 0 !imp=
ortant;    }    td.image-wrapper, td.text-wrapper {       display:block !im=
portant;       width:100% !important;       max-width:initial !important;  =
  }    table[class=3D'wrapper1'] {       table-layout: fixed !important;   =
    padding: 0 !important;       max-width: 600px !important;    }    td[cl=
ass=3D'wrapper-row'] {       table-layout: fixed !important;       box-sizi=
ng: border-box !important;       width:100% !important;       min-width:100=
% !important;    }    table[class=3D'wrapper2'] {       table-layout: fixed=
 !important;       border: none !important;       width: 100% !important;  =
     max-width: 600px !important;       min-height: 520px !important;    } =
   div[class=3D'column-wrapper']{       max-width:300px !important;    }   =
 table.uni-block {       max-width:100% !important;       height:auto !impo=
rtant;       min-height: auto !important;    }    table[class=3D'block-wrap=
per-inner-table'] {       height:auto !important;       min-height: auto !i=
mportant;    }    td[class=3D'block-wrapper'] {       height:auto !importan=
t;       min-height: auto !important;    }    .submit-button-block .button-=
wrapper {       height: auto !important;       width: auto !important;     =
  min-height: initial !important;       max-height: initial !important;    =
   min-width: initial !important;       max-width: initial !important;    }=
    img[class=3D'image-element'] {       height:auto !important;       box-=
sizing: border-box !important;    }}@media all and (max-width: 615px), only=
 screen and (max-device-width : 615px) {    td[class=3D'wrapper-row'] {    =
   padding: 0 !important;       margin: 0 !important;    }    .column {    =
   width:100% !important;       max-width:100% !important;    }}</style><me=
ta http-equiv=3D""Content-Type"" content=3D""text/html;charset=3DUTF-8""></head=
><body width=3D""100%"" align=3D""center""><!--[if gte mso 9]>       <style typ=
e=3D""text/css"">           .uni-block img {               display:block !imp=
ortant;           }       </style><![endif]--><center><table cellpadding=3D=
""0"" cellspacing=3D""0"" align=3D""center"" width=3D""100%"" class=3D""container re=
sponsive""><tbody><tr><td><table cellpadding=3D""0"" cellspacing=3D""0"" align=
=3D""center"" class=3D""wrapper1"" style=3D""width: 100%; box-sizing: border-box=
; background-color: rgb(177, 198, 219); float: left;""><tbody><tr><td class=
=3D""wrapper-row"" style=3D""padding: 25px;""><!--[if (gte mso 9)|(IE)]><table =
cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""cen=
ter""><tr><td><![endif]--><table cellpadding=3D""0"" cellspacing=3D""0"" class=
=3D""wrapper2"" align=3D""center"" style=3D""background-color: rgb(255, 255, 255=
); border-radius: 3px; max-width: 600px; width: 100%; border: none; margin:=
 0px auto; border-spacing: 0px; border-collapse: collapse;""><tbody><tr><td =
width=3D""100%"" class=3D""wrapper-row"" style=3D""vertical-align: top; max-widt=
h: 600px; font-size: 0px; padding: 0px;""><!--[if (gte mso 9)|(IE)]><table c=
ellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""cent=
er""><tr><td><![endif]--><table class=3D""uni-block social-block"" width=3D""10=
0%"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; =
table-layout: fixed; height: auto; border-collapse: collapse; border-spacin=
g: 0px; display: inline-table; vertical-align: top; font-size: medium;""><tb=
ody><tr><td style=3D""width: 100%; text-align: right; height: 46px;"" class=
=3D""block-wrapper"" valign=3D""top""><table class=3D""block-wrapper-inner-table=
"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""height: 100%; w=
idth: 100%; table-layout: fixed; border-spacing: 0px; border-collapse: coll=
apse; min-height: 46px;""><tbody><tr><td style=3D""width: 100%; padding: 5px =
10px; background-image: none; text-align: right;"" class=3D""content-wrapper""=
><span class=3D""networks-wrapper""><span class=3D""scl-button scl-inst""><a hr=
ef=3D""http://unimail.hse.ru/ru/mail_link_tracker?hash=3D66341k59ksujgkjnr33=
pasja89w17rz1ua7fpnpfyw64x53qjt31yqb6otd5o37krjf7pmqrsmwd7rdowuby7eb1cf5ge7=
5tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaW5zdGFncmFtLmNvbS9zdHVkbGlmZ=
WhzZS8_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249=
MjI2MTAzMjAw&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank""><img style=3D""max-he=
ight:64px;max-width:64px;"" src=3D""http://unimail.hse.ru/v5/img/ico/scl/inst=
.png"" alt=3D""Instagram""></a></span> <span class=3D""scl-button scl-vk""><a hr=
ef=3D""http://unimail.hse.ru/ru/mail_link_tracker?hash=3D6ymhr1yk1kfjhgjnr33=
pasja89w17rz1ua7fpnpfyw64x53qjt31xicm79mh756tjsfomt8cafy45gdowuby7eb1cf5ge7=
5tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly92ay5jb20vc3R1ZGxpZmVfaHNlP3V0bV9tZ=
WRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIyNjEwMzIwMA~~=
&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank""><img style=3D""max-height:64px;ma=
x-width:64px;"" src=3D""http://unimail.hse.ru/v5/img/ico/scl/vk.png"" alt=3D""=
=D0=92=D0=BA=D0=BE=D0=BD=D1=82=D0=B0=D0=BA=D1=82=D0=B5""></a></span> <span c=
lass=3D""scl-button scl-custom""><a href=3D""http://unimail.hse.ru/ru/mail_lin=
k_tracker?hash=3D6ntbtmiw68p8u4jnr33pasja89w17rz1ua7fpnpfyw64x53qjt31fzzqzq=
1rnz98651jy57cpbxe46dowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6L=
y93d3cuaHNlLnJ1L291ci8_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1=
dG1fY2FtcGFpZ249MjI2MTAzMjAw&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank""><img=
 style=3D""max-height:64px;max-width:64px;"" src=3D""http://unimail.hse.ru/v5/=
img/ico/scl/custom.png"" alt=3D""=D0=9C=D0=BE=D0=B9 =D1=81=D0=B0=D0=B9=D1=82""=
></a></span></span></td></tr></tbody></table></td></tr></tbody></table><!--=
[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)|(IE=
)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" al=
ign=3D""center""><tr><td><![endif]--><table class=3D""uni-block image-block"" w=
idth=3D""100%"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""wid=
th: 100%; table-layout: fixed; height: auto; border-collapse: collapse; bor=
der-spacing: 0px; display: inline-table; vertical-align: top; font-size: me=
dium;""><tbody><tr><td style=3D""width: 100%; background-image: none; padding=
: 5px 10px 20px 20px; height: 100%;"" class=3D""block-wrapper"" valign=3D""top""=
><table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" =
cellpadding=3D""0"" style=3D""height: 116.922px; width: 100%; table-layout: fi=
xed; text-align: center; border-spacing: 0px; border-collapse: collapse; fo=
nt-size: 0px;""><tbody><tr><td style=3D""width: auto; height: 100%; display: =
inline-table;"" class=3D""content-wrapper""><table class=3D""content-box"" borde=
r=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""display: inline-table;=
 vertical-align: top; width: auto; height: 100%; border-spacing: 0px; borde=
r-collapse: collapse;""><tbody><tr><td style=3D""vertical-align: middle;""><di=
v class=3D""image-wrapper image-drop""><a class=3D""image-link"" href=3D""http:/=
/unimail.hse.ru/ru/mail_link_tracker?hash=3D64d6fxboc8iqnrjnr33pasja89w17rz=
1ua7fpnpfyw64x53qjt31m8m46rs4itywq51jy57cpbxe46dowuby7eb1cf5ge75tbx3h5j71sy=
wopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci8_dXRtX21lZGl1bT1lbWFpbCZ1d=
G1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjI2MTAzMjAw&uid=3DMTMyMzY3NA=3D=
=3D"" target=3D""_blank""><img class=3D""image-element"" src=3D""http://unimail.h=
se.ru/ru/user_file?resource=3Dhimg&user_id=3D1323674&name=3D6jwsf8rjn9jaq3p=
iu3azzngxco47cfqz5xza57gibw4u5ycqngamjhppq9816zw4zzhmwomy54f9so"" alt=3D""Som=
e Image"" id=3D""gridster_block_386_main_img"" style=3D""font-size: small; bord=
er: none; width: auto; max-width: 135px; height: auto; max-height: 117px; o=
utline: none; text-decoration: none;"" width=3D""135""></a></div></td></tr></t=
body></table></td></tr></tbody></table></td></tr></tbody></table><!--[if (g=
te mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)|(IE)]><ta=
ble cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D=
""center""><tr><td><![endif]--><table class=3D""uni-block image-block"" width=
=3D""100%"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: =
100%; table-layout: fixed; height: auto; border-collapse: collapse; border-=
spacing: 0px; display: inline-table; vertical-align: top; font-size: medium=
;""><tbody><tr><td style=3D""width: 100%; background-image: none; padding: 0p=
x 0px 15px; height: 100%;"" class=3D""block-wrapper"" valign=3D""top""><table cl=
ass=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" cellpaddin=
g=3D""0"" style=3D""height: 205px; width: 100%; table-layout: fixed; text-alig=
n: center; border-spacing: 0px; border-collapse: collapse; font-size: 0px;""=
><tbody><tr><td style=3D""width: auto; height: 100%; display: inline-table;""=
 class=3D""content-wrapper""><table class=3D""content-box"" border=3D""0"" cellsp=
acing=3D""0"" cellpadding=3D""0"" style=3D""display: inline-table; vertical-alig=
n: top; width: auto; height: 100%; border-spacing: 0px; border-collapse: co=
llapse;""><tbody><tr><td style=3D""vertical-align: middle;""><div class=3D""ima=
ge-wrapper image-drop""><a class=3D""image-link"" href=3D""javascript:;"" target=
=3D""_self""><img class=3D""image-element"" src=3D""http://unimail.hse.ru/ru/use=
r_file?resource=3Dhimg&user_id=3D1323674&name=3D67rztaxrgyzpfzpiu3azzngxco4=
7cfqz5xza57gtg4u3hzh3n68mjhppq9816zw4zff4ri7fzsig74"" alt=3D""Some Image"" id=
=3D""gridster_block_440_main_img"" style=3D""font-size: small; border: none; w=
idth: 100%; max-width: 600px; height: auto; max-height: 200px; outline: non=
e; text-decoration: none;"" width=3D""600""></a></div></td></tr></tbody></tabl=
e></td></tr></tbody></table></td></tr></tbody></table><!--[if (gte mso 9)|(=
IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)|(IE)]><table cellpad=
ding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""center""><t=
r><td><![endif]--><table class=3D""uni-block text-block"" width=3D""100%"" bord=
er=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-la=
yout: fixed; height: auto; border-collapse: collapse; border-spacing: 0px; =
display: inline-table; vertical-align: top; font-size: medium;""><tbody><tr>=
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top""><table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=
=3D""0"" cellpadding=3D""0"" style=3D""height: 1234px; width: 100%; table-layout=
: fixed; border-spacing: 0px; border-collapse: collapse;""><tbody><tr><td st=
yle=3D""width: 100%; padding: 5px 30px; vertical-align: top; font-size: 14px=
; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; color: rgb(=
51, 51, 51);"" class=3D""content-wrapper""><div class=3D""clearfix cke_editable=
 cke_editable_inline cke_contents_ltr cke_show_borders"" style=3D""overflow-w=
rap: break-word; position: relative;"" tabindex=3D""0"" spellcheck=3D""false"" r=
ole=3D""textbox"" aria-label=3D""false"" aria-describedby=3D""cke_45""><p><span s=
tyle=3D""font-family:Arial,Helvetica,sans-serif""><span style=3D""line-height:=
1.5""><span style=3D""font-size:16px""><span style=3D""font-family:Arial,Helvet=
ica,sans-serif""><span style=3D""line-height:1.5""><span style=3D""font-size:16=
px""><span style=3D""font-family:Arial,Helvetica,sans-serif""><span style=3D""l=
ine-height:1.5""><span style=3D""font-size:16px""><strong>=C2=AB=D0=A2=D0=B8=
=D0=BF=D0=B8=D1=87=D0=BD=D0=B0=D1=8F =D0=92=D1=8B=D1=88=D0=BA=D0=B0=C2=BB =
=D1=80=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B7=D0=B0=D0=BB=D0=B0, =D1=87=D1=82=
=D0=BE =D0=B6=D0=B4=D0=B5=D1=82 =D1=83=D0=BD=D0=B8=D0=B2=D0=B5=D1=80=D1=81=
=D0=B8=D1=82=D0=B5=D1=82 =D0=B2 =D0=B1=D0=BB=D0=B8=D0=B6=D0=B0=D0=B9=D1=88=
=D0=B8=D0=B5 10 =D0=BB=D0=B5=D1=82</strong><br>=D0=92 =D0=BD=D0=BE=D0=B2=D0=
=BE=D0=BC <a data-cke-saved-href=3D""https://readymag.com/u45521095/strategy=
/"" href=3D""http://unimail.hse.ru/ru/mail_link_tracker?hash=3D6kmgrequxreiia=
jnr33pasja89w17rz1ua7fpnpfyw64x53qjt31y5utz3f1r4yc8a7h456ocju6bndowuby7eb1c=
f5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly9yZWFkeW1hZy5jb20vdTQ1NTIxMDk1=
L3N0cmF0ZWd5Lz91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1=
wYWlnbj0yMjYxMDMyMDA~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255)=
;"">=D1=81=D0=BF=D0=B5=D1=86=D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D0=B5</a> =
=D0=A6=D0=B5=D0=BD=D1=82=D1=80 =D0=B2=D0=BD=D1=83=D1=82=D1=80=D0=B5=D0=BD=
=D0=BD=D0=B5=D0=B3=D0=BE =D0=BC=D0=BE=D0=BD=D0=B8=D1=82=D0=BE=D1=80=D0=B8=
=D0=BD=D0=B3=D0=B0 =D0=BD=D0=B0=D0=B3=D0=BB=D1=8F=D0=B4=D0=BD=D0=BE =D1=80=
=D0=B0=D0=B7=D0=B1=D0=B8=D1=80=D0=B0=D0=B5=D1=82 =D0=9F=D1=80=D0=BE=D0=B3=
=D1=80=D0=B0=D0=BC=D0=BC=D1=83 =D1=80=D0=B0=D0=B7=D0=B2=D0=B8=D1=82=D0=B8=
=D1=8F =D0=92=D0=A8=D0=AD =D0=B4=D0=BE 2030 =D0=B3=D0=BE=D0=B4=D0=B0 =D0=B8=
 =D0=BE=D0=B1=D1=8A=D1=8F=D1=81=D0=BD=D1=8F=D0=B5=D1=82, =D0=BA=D0=B0=D0=BA=
=D0=B8=D0=B5 =D0=B7=D0=B0=D0=B4=D0=B0=D1=87=D0=B8 =D1=81=D1=82=D0=B0=D0=B2=
=D0=B8=D1=82 =D0=BF=D0=B5=D1=80=D0=B5=D0=B4 =D1=81=D0=BE=D0=B1=D0=BE=D0=B9 =
=D0=92=D1=8B=D1=88=D0=BA=D0=B0 =D0=BD=D0=B0 =D0=B1=D0=BB=D0=B8=D0=B6=D0=B0=
=D0=B9=D1=88=D0=B5=D0=B5 =D0=B4=D0=B5=D1=81=D1=8F=D1=82=D0=B8=D0=BB=D0=B5=
=D1=82=D0=B8=D0=B5.<br><br><span style=3D""font-family:Arial,Helvetica,sans-=
serif""><span style=3D""line-height:1.5""><span style=3D""font-size:16px""><span=
 style=3D""font-family:Arial,Helvetica,sans-serif""><span style=3D""line-heigh=
t:1.5""><span style=3D""font-size:16px""><span style=3D""font-family:Arial,Helv=
etica,sans-serif""><span style=3D""line-height:1.5""><span style=3D""font-size:=
16px""><strong>=D0=9F=D1=80=D0=BE=D1=88=D0=BB=D0=BE =D0=BF=D0=B5=D1=80=D0=B2=
=D0=BE=D0=B5 =D0=B7=D0=B0=D1=81=D0=B5=D0=B4=D0=B0=D0=BD=D0=B8=D0=B5 =D0=BD=
=D0=BE=D0=B2=D0=BE=D0=B3=D0=BE =D1=81=D0=BE=D0=B7=D1=8B=D0=B2=D0=B0 =D1=81=
=D1=82=D1=83=D0=B4=D1=81=D0=BE=D0=B2=D0=B5=D1=82=D0=B0&nbsp;</strong><br>=
=D0=9A=D0=B0=D0=BA =D1=83=D1=81=D1=82=D1=80=D0=BE=D0=B5=D0=BD =D0=B3=D0=BB=
=D0=B0=D0=B2=D0=BD=D1=8B=D0=B9 =D0=BE=D1=80=D0=B3=D0=B0=D0=BD =D1=81=D1=82=
=D1=83=D0=B4=D0=B5=D0=BD=D1=87=D0=B5=D1=81=D0=BA=D0=BE=D0=B3=D0=BE =D1=81=
=D0=B0=D0=BC=D0=BE=D1=83=D0=BF=D1=80=D0=B0=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=D1=
=8F =D0=B8 =D0=BA=D1=82=D0=BE =D0=B2=D0=BE=D0=B7=D0=B3=D0=BB=D0=B0=D0=B2=D0=
=B8=D0=BB =D0=BF=D1=80=D0=BE=D1=84=D0=B8=D0=BB=D1=8C=D0=BD=D1=8B=D0=B5 =D0=
=BA=D0=BE=D0=BC=D0=B8=D1=82=D0=B5=D1=82=D1=8B&nbsp;=E2=80=93 <a data-cke-sa=
ved-href=3D""https://www.hse.ru/our/news/337665644.html"" href=3D""http://unim=
ail.hse.ru/ru/mail_link_tracker?hash=3D6m3u77tunqi78kjnr33pasja89w17rz1ua7f=
pnpfyw64x53qjt31eb7rdrcmtb455fmg7bnd9cfesndowuby7eb1cf5ge75tbx3h5j71sywopdf=
iu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLzMzNzY2NTY0NC5odG1sP3V0bV=
9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIyNjEwMzIwM=
A~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255);"">=D1=87=D0=B8=
=D1=82=D0=B0=D0=B9=D1=82=D0=B5</a> =D0=BD=D0=B0 =D0=BD=D0=B0=D1=88=D0=B5=D0=
=BC =D0=BF=D0=BE=D1=80=D1=82=D0=B0=D0=BB=D0=B5.</span></span></span></span>=
</span></span></span></span></span><br><br><strong>=D0=9D=D0=B0 =D0=A4=D0=
=93=D0=9D =D0=BE=D1=82=D0=BA=D1=80=D0=BE=D0=B5=D1=82=D1=81=D1=8F =D0=BC=D0=
=B0=D0=B3=D0=B8=D1=81=D1=82=D0=B5=D1=80=D1=81=D0=BA=D0=B0=D1=8F =D0=BF=D1=
=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC=D0=B0 =C2=ABGermanica: =D0=B8=D1=81=
=D1=82=D0=BE=D1=80=D0=B8=D1=8F =D0=B8 =D1=81=D0=BE=D0=B2=D1=80=D0=B5=D0=BC=
=D0=B5=D0=BD=D0=BD=D0=BE=D1=81=D1=82=D1=8C=C2=BB</strong><br>=D0=9E=D0=BD=
=D0=B0 =D0=BD=D0=B0=D0=BF=D1=80=D0=B0=D0=B2=D0=BB=D0=B5=D0=BD=D0=B0 =D0=BD=
=D0=B0 =D0=BF=D0=BE=D0=B4=D0=B3=D0=BE=D1=82=D0=BE=D0=B2=D0=BA=D1=83 =D1=81=
=D0=BF=D0=B5=D1=86=D0=B8=D0=B0=D0=BB=D0=B8=D1=81=D1=82=D0=BE=D0=B2 =D0=BF=
=D0=BE =D0=93=D0=B5=D1=80=D0=BC=D0=B0=D0=BD=D0=B8=D0=B8, =D0=90=D0=B2=D1=81=
=D1=82=D1=80=D0=B8=D0=B8 =D0=B8 =D0=A8=D0=B2=D0=B5=D0=B9=D1=86=D0=B0=D1=80=
=D0=B8=D0=B8. =D0=A1=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D1=8B <a data-cke-=
saved-href=3D""https://www.hse.ru/news/edu/337245686.html"" href=3D""http://un=
imail.hse.ru/ru/mail_link_tracker?hash=3D6pedrg961t7cx6jnr33pasja89w17rz1ua=
7fpnpfyw64x53qjt31gactrcegicr3wnug89iaa1ki9edowuby7eb1cf5ge75tbx3h5j71sywop=
dfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L25ld3MvZWR1LzMzNzI0NTY4Ni5odG1sP3V0=
bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIyNjEwMzI=
wMA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=B1=D1=83=
=D0=B4=D1=83=D1=82 =D0=B8=D0=B7=D1=83=D1=87=D0=B0=D1=82=D1=8C</a> =D0=BF=D0=
=BE=D0=BB=D0=B8=D1=82=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D0=B5, =D1=8D=D0=
=BA=D0=BE=D0=BD=D0=BE=D0=BC=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D0=B5 =D0=
=B8 =D0=BF=D1=80=D0=B0=D0=B2=D0=BE=D0=B2=D1=8B=D0=B5 =D1=81=D0=B8=D1=81=D1=
=82=D0=B5=D0=BC=D1=8B =D0=BD=D0=B5=D0=BC=D0=B5=D1=86=D0=BA=D0=BE=D1=8F=D0=
=B7=D1=8B=D1=87=D0=BD=D1=8B=D1=85 =D1=81=D1=82=D1=80=D0=B0=D0=BD, =D0=B0 =
=D1=82=D0=B0=D0=BA=D0=B6=D0=B5 =D0=BE=D1=81=D0=B2=D0=B0=D0=B8=D0=B2=D0=B0=
=D1=82=D1=8C =D0=B8=D1=85 =D0=B8=D1=81=D1=82=D0=BE=D1=80=D0=B8=D1=8E =D0=B8=
 =D0=BA=D1=83=D0=BB=D1=8C=D1=82=D1=83=D1=80=D1=83. =D0=9E=D0=B1=D1=83=D1=87=
=D0=B5=D0=BD=D0=B8=D0=B5 =D0=BF=D0=BE=D1=81=D1=82=D1=80=D0=BE=D0=B5=D0=BD=
=D0=BE =D0=BD=D0=B0 =D1=80=D1=83=D1=81=D1=81=D0=BA=D0=BE=D0=BC =D0=B8 =D0=
=BD=D0=B5=D0=BC=D0=B5=D1=86=D0=BA=D0=BE=D0=BC.&nbsp;<br><br><strong>=D0=92 =
=D0=92=D1=8B=D1=88=D0=BA=D0=B5 =D0=BF=D1=80=D0=BE=D1=85=D0=BE=D0=B4=D0=B8=
=D1=82 =D1=81=D0=B1=D0=BE=D1=80 =D0=BC=D0=B0=D0=BA=D1=83=D0=BB=D0=B0=D1=82=
=D1=83=D1=80=D1=8B</strong><br>=D0=95=D1=81=D0=BB=D0=B8 =D1=85=D0=BE=D1=82=
=D0=B8=D1=82=D0=B5 =D1=81=D0=B4=D0=B0=D1=82=D1=8C =D0=B1=D1=83=D0=BC=D0=B0=
=D0=B3=D1=83, =D0=BA=D0=B0=D1=80=D1=82=D0=BE=D0=BD =D0=B8 =D0=BD=D0=B5=D0=
=BD=D1=83=D0=B6=D0=BD=D1=8B=D0=B5 =D0=B3=D0=B0=D0=B7=D0=B5=D1=82=D1=8B, =D0=
=B8=D1=89=D0=B8=D1=82=D0=B5 =D0=B0=D0=B4=D1=80=D0=B5=D1=81=D0=B0 =D0=B8 =D0=
=BF=D1=80=D0=B0=D0=B2=D0=B8=D0=BB=D0=B0 =D1=81=D0=B4=D0=B0=D1=87=D0=B8 =D0=
=B2 <a data-cke-saved-href=3D""https://www.hse.ru/our/news/337411925.html"" h=
ref=3D""http://unimail.hse.ru/ru/mail_link_tracker?hash=3D6o3ne1y4sh9fawjnr3=
3pasja89w17rz1ua7fpnpfyw64x53qjt31rezuaz74fijtqkcjshmt9de81rdowuby7eb1cf5ge=
75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLzMzNzQx=
MTkyNS5odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXB=
haWduPTIyNjEwMzIwMA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255)=
;"">=D0=BD=D0=B0=D1=88=D0=B5=D0=BC =D0=BC=D0=B0=D1=82=D0=B5=D1=80=D0=B8=D0=
=B0=D0=BB=D0=B5</a>. =D0=9A=D1=81=D1=82=D0=B0=D1=82=D0=B8, =D0=A8=D0=BA=D0=
=BE=D0=BB=D0=B0 =D0=B4=D0=B8=D0=B7=D0=B0=D0=B9=D0=BD=D0=B0 =D0=B8 =D1=84=D0=
=BE=D0=BD=D0=B4 =C2=AB=D0=9C=D0=B5=D1=80=D0=B8=D0=B4=D0=B8=D0=B0=D0=BD =D0=
=94=D0=BE=D0=B1=D1=80=D0=B0=C2=BB =D0=B2=D1=8B=D0=BF=D1=83=D1=81=D1=82=D0=
=B8=D0=BB=D0=B8 =D0=BA=D1=80=D1=83=D1=82=D0=BE=D0=B9 =D1=8D=D0=BA=D0=BE-=D0=
=BA=D0=B0=D0=BB=D0=B5=D0=BD=D0=B4=D0=B0=D1=80=D1=8C. =D0=9F=D0=BE=D0=BB=D1=
=83=D1=87=D0=B8=D1=82=D1=8C =D0=B5=D0=B3=D0=BE =D0=BC=D0=BE=D0=B6=D0=BD=D0=
=BE =D0=B2 =D0=BB=D1=8E=D0=B1=D0=BE=D0=B9 =D0=B4=D0=B5=D0=BD=D1=8C =D1=81 1=
1:00 =D0=B4=D0=BE 19:00 =D0=B2 =D0=B0=D1=83=D0=B4. =D0=9C407 =D0=BA=D0=BE=
=D1=80=D0=BF=D1=83=D1=81=D0=B0 =D0=BD=D0=B0 =D0=9F=D0=BE=D0=BA=D1=80=D0=BE=
=D0=B2=D0=BA=D0=B5.<br><br><strong>=D0=9F=D1=80=D0=B8=D0=B1=D0=BB=D0=B8=D0=
=B6=D0=B0=D1=8E=D1=82=D1=81=D1=8F II =D0=9D=D0=B0=D1=83=D1=87=D0=BD=D1=8B=
=D0=B5 =D0=B1=D0=BE=D0=B8 3 =D1=81=D0=B5=D0=B7=D0=BE=D0=BD=D0=B0</strong><b=
r>=D0=9F=D0=BE =D1=82=D1=80=D0=B0=D0=B4=D0=B8=D1=86=D0=B8=D0=B8 =D1=87=D0=
=B5=D1=82=D1=8B=D1=80=D0=B5 =D1=81=D0=BF=D0=B8=D0=BA=D0=B5=D1=80=D0=B0 <a d=
ata-cke-saved-href=3D""https://www.hse.ru/our/news/337695422.html"" href=3D""h=
ttp://unimail.hse.ru/ru/mail_link_tracker?hash=3D6yqfedfq4w4es1jnr33pasja89=
w17rz1ua7fpnpfyw64x53qjt31nbuff4edc8jqjogcp3pauggr9heyztq9354zmy19xro34zj6h=
pe5r5gw4qgwa48ay&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLzMzNzY5NTQyMi5o=
dG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTI=
yNjEwMzIwMA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D1=
=81=D1=80=D0=B0=D0=B7=D1=8F=D1=82=D1=81=D1=8F</a> =D0=B7=D0=B0 =D0=BC=D0=B5=
=D1=81=D1=82=D0=BE =D0=B2 =D1=84=D0=B8=D0=BD=D0=B0=D0=BB=D0=B5 =D0=B8 =D0=
=BF=D0=BE=D0=BB=D1=83=D1=87=D0=B0=D1=82 =D1=88=D0=B0=D0=BD=D1=81 =D0=B2=D1=
=8B=D0=B8=D0=B3=D1=80=D0=B0=D1=82=D1=8C =D1=82=D1=80=D0=B5=D0=B2=D0=B5=D0=
=BB-=D0=B3=D1=80=D0=B0=D0=BD=D1=82 =D0=B2 =D0=95=D0=B2=D1=80=D0=BE=D0=BF=D1=
=83. =D0=97=D1=80=D0=B8=D1=82=D0=B5=D0=BB=D0=B5=D0=B9 =D0=B6=D0=B4=D1=83=D1=
=82 =D0=B8=D0=BD=D1=82=D0=B5=D1=80=D0=B5=D1=81=D0=BD=D1=8B=D0=B5 =D0=B8=D1=
=81=D1=81=D0=BB=D0=B5=D0=B4=D0=BE=D0=B2=D0=B0=D0=BD=D0=B8=D1=8F =D0=BE =D1=
=81=D0=BE=D0=BE=D1=82=D0=BD=D0=BE=D1=88=D0=B5=D0=BD=D0=B8=D0=B8 =D0=BB=D0=
=B8=D1=87=D0=BD=D1=8B=D1=85 =D0=BA=D0=B0=D1=87=D0=B5=D1=81=D1=82=D0=B2 =D0=
=B8 =D0=BE=D0=BF=D0=BB=D0=B0=D1=82=D1=8B =D1=82=D1=80=D1=83=D0=B4=D0=B0, =
=D0=B2=D0=BB=D0=B8=D1=8F=D0=BD=D0=B8=D0=B8 =D0=B0=D0=BD=D1=82=D0=B8=D1=82=
=D0=B0=D0=B1=D0=B0=D1=87=D0=BD=D0=BE=D0=B9 =D1=80=D0=B5=D0=BA=D0=BB=D0=B0=
=D0=BC=D1=8B =D0=BD=D0=B0 =D0=BD=D0=B0=D1=88=D0=B5 =D0=BE=D1=82=D0=BD=D0=BE=
=D1=88=D0=B5=D0=BD=D0=B8=D0=B5 =D0=BA =D0=BA=D1=83=D1=80=D0=B5=D0=BD=D0=B8=
=D1=8E, =D1=8F=D0=B7=D1=8B=D0=BA=D0=B5 =D0=BC=D0=B5=D0=B4=D0=B8=D0=B0 =D0=
=B8 =D0=BF=D0=BE=D0=BB=D0=B8=D1=82=D0=B8=D0=BA=D0=B8, =D0=B0 =D1=82=D0=B0=
=D0=BA=D0=B6=D0=B5 =D0=B4=D0=B5=D0=B3=D1=83=D0=BC=D0=B0=D0=BD=D0=B8=D0=B7=
=D0=B0=D1=86=D0=B8=D0=B8 =D0=B8 =D1=81=D0=BE=D1=86=D0=B8=D0=B0=D0=BB=D1=8C=
=D0=BD=D0=BE=D0=B9 =D0=BF=D0=BE=D0=B4=D0=B4=D0=B5=D1=80=D0=B6=D0=BA=D0=B5 =
=C2=AB=D0=B3=D1=80=D1=8F=D0=B7=D0=BD=D1=8B=D1=85=C2=BB =D1=80=D0=B0=D0=B1=
=D0=BE=D1=87=D0=B8=D1=85.<br><br><strong>=D0=9A=D1=83=D1=80=D0=B0=D1=82=D0=
=BE=D1=80=D1=8B =D0=B6=D0=B4=D1=83=D1=82 =D0=B2=D1=81=D0=B5=D1=85 =D0=BD=D0=
=B0 =D0=9B=D0=AC=D0=94=D0=A3</strong><br>=D0=92 =D0=BD=D0=BE=D1=87=D1=8C =
=D1=81 7 =D0=BD=D0=B0 8 =D1=84=D0=B5=D0=B2=D1=80=D0=B0=D0=BB=D1=8F =D0=B2 =
=D0=BB=D0=B5=D0=B4=D0=BE=D0=B2=D0=BE=D0=BC =D0=BA=D0=BE=D0=BC=D0=BF=D0=BB=
=D0=B5=D0=BA=D1=81=D0=B5 =C2=AB=D0=9C=D0=BE=D1=80=D0=BE=D0=B7=D0=BE=D0=B2=
=D0=BE=C2=BB =D0=BF=D1=80=D0=BE=D0=B9=D0=B4=D0=B5=D1=82 =D0=BC=D0=B0=D1=81=
=D1=81=D0=BE=D0=B2=D0=BE=D0=B5 =D0=BA=D0=B0=D1=82=D0=B0=D0=BD=D0=B8=D0=B5 =
=D0=B4=D0=BB=D1=8F =D1=81=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=B2, =
=D0=B2=D1=8B=D0=BF=D1=83=D1=81=D0=BA=D0=BD=D0=B8=D0=BA=D0=BE=D0=B2 =D0=B8 =
=D1=81=D0=BE=D1=82=D1=80=D1=83=D0=B4=D0=BD=D0=B8=D0=BA=D0=BE=D0=B2 =D0=92=
=D1=8B=D1=88=D0=BA=D0=B8. =D0=92 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=
=D0=BC=D0=B5: =D0=B2=D0=B5=D1=87=D0=B5=D1=80=D0=B8=D0=BD=D0=BA=D0=B0 =D1=81=
=D0=BE =D1=81=D0=B2=D0=B5=D1=82=D0=BE=D0=BC=D1=83=D0=B7=D1=8B=D0=BA=D0=BE=
=D0=B9, =D0=BA=D0=BE=D0=BD=D0=BA=D1=83=D1=80=D1=81=D1=8B =D0=B8 =D0=BC=D0=
=B0=D1=81=D1=82=D0=B5=D1=80-=D0=BA=D0=BB=D0=B0=D1=81=D1=81=D1=8B =D0=BE=D1=
=82 =D1=84=D0=B8=D0=B3=D1=83=D1=80=D0=B8=D1=81=D1=82=D0=BE=D0=B2 =D0=B8 =D1=
=85=D0=BE=D0=BA=D0=BA=D0=B5=D0=B8=D1=81=D1=82=D0=BE=D0=B2. =D0=9A=D0=BE=D0=
=BB=D0=B8=D1=87=D0=B5=D1=81=D1=82=D0=B2=D0=BE =D0=BC=D0=B5=D1=81=D1=82 =D0=
=BE=D0=B3=D1=80=D0=B0=D0=BD=D0=B8=D1=87=D0=B5=D0=BD=D0=BE, =D0=BE=D1=81=D1=
=82=D0=B0=D0=BB=D0=B0=D1=81=D1=8C =D0=BF=D0=BE=D1=81=D0=BB=D0=B5=D0=B4=D0=
=BD=D1=8F=D1=8F =D0=B2=D0=BE=D0=BB=D0=BD=D0=B0 =D1=80=D0=B5=D0=B3=D0=B8=D1=
=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D0=B8: =D1=87=D1=82=D0=BE=D0=B1=D1=8B =D1=
=83=D1=81=D0=BF=D0=B5=D1=82=D1=8C, =D1=81=D0=BB=D0=B5=D0=B4=D0=B8=D1=82=D0=
=B5 =D0=B7=D0=B0 =D0=BE=D0=B1=D0=BD=D0=BE=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=D1=
=8F=D0=BC=D0=B8 <a data-cke-saved-href=3D""https://vk.com/curatorled"" href=
=3D""http://unimail.hse.ru/ru/mail_link_tracker?hash=3D6ao4qdnc5r4qnejnr33pa=
sja89w17rz1ua7fpnpfyw64x53qjt31yx9eeo4j8og6k1wq3xsyrjyhzb6ardjf311q9raknt3a=
4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly92ay5jb20vY3VyYXRvcmxlZD91dG1fbWVkaXV=
tPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMjYxMDMyMDA~&uid=
=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=B2 =D0=B3=D1=80=D1=
=83=D0=BF=D0=BF=D0=B5</a>.&nbsp;<br><br><strong>=D0=A1 10 =D1=84=D0=B5=D0=
=B2=D1=80=D0=B0=D0=BB=D1=8F =D1=81=D1=82=D0=B0=D1=80=D1=82=D1=83=D0=B5=D1=
=82 =D0=A8=D0=BA=D0=BE=D0=BB=D0=B0 =D0=B6=D1=83=D1=80=D0=BD=D0=B0=D0=BB=D0=
=B8=D1=81=D1=82=D0=B8=D0=BA=D0=B8 HSE Press</strong><br>=D0=A3=D1=87=D0=B0=
=D1=81=D1=82=D0=BD=D0=B8=D0=BA=D0=B8 =D1=81=D0=BC=D0=BE=D0=B3=D1=83=D1=82 =
=D1=83=D0=B7=D0=BD=D0=B0=D1=82=D1=8C =D0=B1=D0=BE=D0=BB=D1=8C=D1=88=D0=B5 =
=D0=BE =D1=82=D0=B5=D0=BB=D0=B5=D0=B2=D0=B8=D0=B7=D0=B8=D0=BE=D0=BD=D0=BD=
=D0=BE=D0=B9 =D0=B6=D1=83=D1=80=D0=BD=D0=B0=D0=BB=D0=B8=D1=81=D1=82=D0=B8=
=D0=BA=D0=B5, =D0=BD=D0=B0=D1=83=D1=87=D0=BF=D0=BE=D0=BF=D0=B5, =D0=B3=D1=
=80=D0=B0=D0=BC=D0=BE=D1=82=D0=BD=D0=BE=D0=BC =D1=84=D0=B0=D0=BA=D1=82=D1=
=87=D0=B5=D0=BA=D0=B8=D0=BD=D0=B3=D0=B5 =D0=B8 =D0=B0=D0=BD=D0=B0=D0=BB=D0=
=B8=D0=B7=D0=B5 =D0=B1=D0=BE=D0=BB=D1=8C=D1=88=D0=B8=D1=85 =D0=B4=D0=B0=D0=
=BD=D0=BD=D1=8B=D1=85. =D0=A1=D1=80=D0=B5=D0=B4=D0=B8 =D1=81=D0=BF=D0=B8=D0=
=BA=D0=B5=D1=80=D0=BE=D0=B2: =D0=B6=D1=83=D1=80=D0=BD=D0=B0=D0=BB=D0=B8=D1=
=81=D1=82=D1=8B =C2=AB=D0=94=D0=BE=D0=B6=D0=B4=D1=8F=C2=BB, =C2=AB=D0=9D=D0=
=BE=D0=B2=D0=BE=D0=B9 =D0=B3=D0=B0=D0=B7=D0=B5=D1=82=D1=8B=C2=BB, =C2=AB=D0=
=A1=D1=82=D1=80=D0=B0=D0=B4=D0=B0=D1=8E=D1=89=D0=B5=D0=B3=D0=BE =D0=A1=D1=
=80=D0=B5=D0=B4=D0=BD=D0=B5=D0=B2=D0=B5=D0=BA=D0=BE=D0=B2=D1=8C=D1=8F=C2=BB=
 =D0=B8 =D0=B4=D1=80=D1=83=D0=B3=D0=B8=D1=85 =D1=81=D0=BE=D0=B2=D1=80=D0=B5=
=D0=BC=D0=B5=D0=BD=D0=BD=D1=8B=D1=85 =D0=BC=D0=B5=D0=B4=D0=B8=D0=B0. =D0=9F=
=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=BE=D1=81=D1=82=D0=B8 <a data-cke-sa=
ved-href=3D""https://www.hse.ru/our/news/339017836.html"" href=3D""http://unim=
ail.hse.ru/ru/mail_link_tracker?hash=3D6yx7itti9kzmqnjnr33pasja89w17rz1ua7f=
pnpfyw64x53qjt31noaja6iz6a1aqy56bgz4d3ru4cgftcrc1foxceyknt3a4jmx9fqz66eftcj=
thdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLzMzOTAxNzgzNi5odG1sP3V0bV=
9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIyNjEwMzIwM=
A~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D1=82=D1=83=D1=
=82</a>.<br><br><strong>20 =D1=84=D0=B5=D0=B2=D1=80=D0=B0=D0=BB=D1=8F =D0=
=B2 =D0=9A=D0=A6 =D0=BF=D1=80=D0=BE=D0=B9=D0=B4=D0=B5=D1=82 =D0=BA=D0=BE=D0=
=BD=D1=86=D0=B5=D1=80=D1=82 =D0=BA=D0=BB=D0=B0=D1=81=D1=81=D0=B8=D1=87=D0=
=B5=D1=81=D0=BA=D0=BE=D0=B9 =D0=BC=D1=83=D0=B7=D1=8B=D0=BA=D0=B8 =D1=81 =D0=
=B3=D1=80=D0=B0=D1=84=D1=84=D0=B8=D1=82=D0=B8-=D0=B8=D0=BD=D1=81=D1=82=D0=
=B0=D0=BB=D0=BB=D1=8F=D1=86=D0=B8=D1=8F=D0=BC=D0=B8</strong><br>=D0=9F=D1=
=80=D0=B8=D1=85=D0=BE=D0=B4=D0=B8=D1=82=D0=B5 =D0=BF=D0=BE=D1=81=D0=BB=D1=
=83=D1=88=D0=B0=D1=82=D1=8C =D1=88=D0=B5=D0=B4=D0=B5=D0=B2=D1=80=D1=8B =D0=
=BC=D1=83=D0=B7=D1=8B=D0=BA=D0=B8 =D0=B1=D0=B0=D1=80=D0=BE=D0=BA=D0=BA=D0=
=BE, =D0=BD=D0=B5=D0=B0=D0=BF=D0=BE=D0=BB=D0=B8=D1=82=D0=B0=D0=BD=D1=81=D0=
=BA=D0=B8=D0=B5 =D0=BF=D0=B5=D1=81=D0=BD=D0=B8 =D0=B8 =D1=81=D0=B0=D0=BC=D1=
=8B=D0=B5 =D0=B8=D0=B7=D0=B2=D0=B5=D1=81=D1=82=D0=BD=D1=8B=D0=B5 =D0=B0=D1=
=80=D0=B8=D0=B8 =D0=B8=D0=B7 =D0=BE=D0=BF=D0=B5=D1=80 =D0=9C=D0=BE=D1=86=D0=
=B0=D1=80=D1=82=D0=B0, =D0=91=D0=B5=D0=BB=D0=BB=D0=B8=D0=BD=D0=B8, =D0=91=
=D0=B8=D0=B7=D0=B5, =D0=A7=D0=B0=D0=B9=D0=BA=D0=BE=D0=B2=D1=81=D0=BA=D0=BE=
=D0=B3=D0=BE. =D0=92 =D0=BF=D1=80=D0=B5=D0=B4=D0=B4=D0=B2=D0=B5=D1=80=D0=B8=
=D0=B8 =D0=BA=D0=BE=D0=BD=D1=86=D0=B5=D1=80=D1=82=D0=B0 =D0=BC=D1=8B <a dat=
a-cke-saved-href=3D""https://www.hse.ru/our/news/337903179.html"" href=3D""htt=
p://unimail.hse.ru/ru/mail_link_tracker?hash=3D6ycatnh7m3ros1jnr33pasja89w1=
7rz1ua7fpnpfyw64x53qjt31cte46ff3xqu468aapwqk36ag7mcfzqrh53f9cfwknt3a4jmx9fq=
z66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLzMzNzkwMzE3OS5odG=
1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIyN=
jEwMzIwMA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=BF=
=D0=BE=D0=B3=D0=BE=D0=B2=D0=BE=D1=80=D0=B8=D0=BB=D0=B8</a> =D1=81 =D1=85=D1=
=83=D0=B4=D0=BE=D0=B6=D0=B5=D1=81=D1=82=D0=B2=D0=B5=D0=BD=D0=BD=D1=8B=D0=BC=
&nbsp;=D1=80=D1=83=D0=BA=D0=BE=D0=B2=D0=BE=D0=B4=D0=B8=D1=82=D0=B5=D0=BB=D0=
=B5=D0=BC =D0=B8 =D0=BC=D1=83=D0=B7=D1=8B=D0=BA=D0=B0=D0=BD=D1=82=D0=B0=D0=
=BC=D0=B8 =D0=9E=D1=80=D0=BA=D0=B5=D1=81=D1=82=D1=80=D0=B0 =D0=92=D0=A8=D0=
=AD.</span></span></span></span></span></span></span></span></span></p></di=
v></td></tr></tbody></table></td></tr></tbody></table><!--[if (gte mso 9)|(=
IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)|(IE)]><table cellpad=
ding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""center""><t=
r><td><![endif]--><table class=3D""uni-block line-block"" width=3D""100%"" bord=
er=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-la=
yout: fixed; height: auto; border-collapse: collapse; border-spacing: 0px; =
display: inline-table; vertical-align: top; font-size: medium; min-height: =
10px;""><tbody><tr><td style=3D""width: 100%; background-image: none; height:=
 100%; vertical-align: middle; min-height: auto; font-size: medium;"" class=
=3D""block-wrapper"" valign=3D""top""><table class=3D""block-wrapper-inner-table=
"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""height: 10px; w=
idth: 100%; table-layout: fixed; border-spacing: 0px; border-collapse: coll=
apse; min-height: 10px;""><tbody><tr><td style=3D""width: 100%; vertical-alig=
n: middle; height: 10px; min-height: 10px;"" class=3D""content-wrapper""><tabl=
e border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; ta=
ble-layout: fixed; border-spacing: 0; border-collapse: collapse; font-size:=
 0;""><tbody><tr><td class=3D""separator-line"" style=3D""width: 100%; backgrou=
nd-color: rgb(255, 255, 255); height: 1px; min-height: 1px; max-height: 1px=
; line-height: 1px;"">&nbsp;</td></tr></tbody></table></td></tr></tbody></ta=
ble></td></tr></tbody></table><!--[if (gte mso 9)|(IE)]></td></tr></table><=
![endif]--><!--[if (gte mso 9)|(IE)]><table cellpadding=3D""0"" cellspacing=
=3D""0"" border=3D""0"" width=3D""600"" align=3D""center""><tr><td><![endif]--><tab=
le class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspacing=
=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; height:=
 auto; border-collapse: collapse; border-spacing: 0px; display: inline-tabl=
e; vertical-align: top; font-size: medium;""><tbody><tr><td style=3D""width: =
100%; background-image: none; min-height: 50px; height: 50px;"" class=3D""blo=
ck-wrapper"" valign=3D""middle""><table class=3D""block-wrapper-inner-table"" bo=
rder=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""height: 100%; width=
: 100%; table-layout: fixed; border-spacing: 0px; border-collapse: collapse=
; min-height: 50px;""><tbody><tr><td style=3D""width: 100%; text-align: cente=
r;"" class=3D""content-wrapper""><table class=3D""valign-wrapper"" border=3D""0"" =
cellspacing=3D""0"" cellpadding=3D""0"" style=3D""display: inline-table; width: =
auto; border-spacing: 0px; border-collapse: collapse;""><tbody><tr><td class=
=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""border: non=
e; border-radius: 10px; padding: 0px 30px; background-color: rgb(0, 95, 129=
); height: 50px; min-height: 50px;""><a class=3D""mailbtn"" href=3D""http://uni=
mail.hse.ru/ru/mail_link_tracker?hash=3D6gtnc1n65htskrjnr33pasja89w17rz1ua7=
fpnpfyw64x53qjt31xnh9ushfms1o3eqpbuhsxnyq7dy6gt5f1gmfwtyknt3a4jmx9fqz66eftc=
jthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci8_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc=
291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjI2MTAzMjAw&uid=3DMTMyMzY3NA=3D=3D""=
 target=3D""_blank"" style=3D""width:100%;display:inline-block;text-decoration=
:none;""><span class=3D""btn-inner"" style=3D""display: inline; font-size: 16px=
; font-family: Arial, Helvetica, sans-serif; line-height: 19.2px; color: rg=
b(255, 255, 255); background-color: rgb(0, 95, 129); border: 0px; word-brea=
k: break-all;"">=D0=92=D1=81=D0=B5 =D0=BD=D0=BE=D0=B2=D0=BE=D1=81=D1=82=D0=
=B8</span></a></td></tr></tbody></table></td></tr></tbody></table></td></tr=
></tbody></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!=
--[if (gte mso 9)|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=
=3D""0"" width=3D""600"" align=3D""center""><tr><td><![endif]--><table class=3D""u=
ni-block line-block"" width=3D""100%"" border=3D""0"" cellspacing=3D""0"" cellpadd=
ing=3D""0"" style=3D""width: 100%; table-layout: fixed; height: auto; border-c=
ollapse: collapse; border-spacing: 0px; display: inline-table; vertical-ali=
gn: top; font-size: medium; min-height: 10px;""><tbody><tr><td style=3D""widt=
h: 100%; background-image: none; height: 100%; vertical-align: middle; min-=
height: auto; font-size: medium;"" class=3D""block-wrapper"" valign=3D""top""><t=
able class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" cel=
lpadding=3D""0"" style=3D""height: 10px; width: 100%; table-layout: fixed; bor=
der-spacing: 0px; border-collapse: collapse; min-height: 10px;""><tbody><tr>=
<td style=3D""width: 100%; vertical-align: middle; height: 10px; min-height:=
 10px;"" class=3D""content-wrapper""><table border=3D""0"" cellspacing=3D""0"" cel=
lpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; border-spacing: 0=
; border-collapse: collapse; font-size: 0;""><tbody><tr><td class=3D""separat=
or-line"" style=3D""width: 100%; background-color: rgb(255, 255, 255); height=
: 1px; min-height: 1px; max-height: 1px; line-height: 1px;"">&nbsp;</td></tr=
></tbody></table></td></tr></tbody></table></td></tr></tbody></table><!--[i=
f (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)|(IE)]=
><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" alig=
n=3D""center""><tr><td><![endif]--><table class=3D""uni-block image-block"" wid=
th=3D""100%"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width=
: 100%; table-layout: fixed; height: auto; border-collapse: collapse; borde=
r-spacing: 0px; display: inline-table; vertical-align: top; font-size: medi=
um;""><tbody><tr><td style=3D""width: 100%; background-image: none; padding: =
0px 0px 15px; height: 100%;"" class=3D""block-wrapper"" valign=3D""top""><table =
class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" cellpadd=
ing=3D""0"" style=3D""height: 205px; width: 100%; table-layout: fixed; text-al=
ign: center; border-spacing: 0px; border-collapse: collapse; font-size: 0px=
;""><tbody><tr><td style=3D""width: auto; height: 100%; display: inline-table=
;"" class=3D""content-wrapper""><table class=3D""content-box"" border=3D""0"" cell=
spacing=3D""0"" cellpadding=3D""0"" style=3D""display: inline-table; vertical-al=
ign: top; width: auto; height: 100%; border-spacing: 0px; border-collapse: =
collapse;""><tbody><tr><td style=3D""vertical-align: middle;""><div class=3D""i=
mage-wrapper image-drop""><a class=3D""image-link"" href=3D""javascript:;"" targ=
et=3D""_self""><img class=3D""image-element"" src=3D""http://unimail.hse.ru/ru/u=
ser_file?resource=3Dhimg&user_id=3D1323674&name=3D6ff4b61a3tnr79piu3azzngxc=
o47cfqz5xza57gs5u5ro3d9bmjkxt7ktpsx7aui4zuku35zh4nnbc"" alt=3D""Some Image"" i=
d=3D""gridster_block_438_main_img"" style=3D""font-size: small; border: none; =
width: 100%; max-width: 600px; height: auto; max-height: 200px; outline: no=
ne; text-decoration: none;"" width=3D""600""></a></div></td></tr></tbody></tab=
le></td></tr></tbody></table></td></tr></tbody></table><!--[if (gte mso 9)|=
(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)|(IE)]><table cellpa=
dding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""center""><=
tr><td><![endif]--><table class=3D""uni-block text-block"" width=3D""100%"" bor=
der=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-l=
ayout: fixed; height: auto; border-collapse: collapse; border-spacing: 0px;=
 display: inline-table; vertical-align: top; font-size: medium;""><tbody><tr=
><td style=3D""width: 100%; background-color: rgb(255, 255, 255); background=
-image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=
=3D""top""><table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacin=
g=3D""0"" cellpadding=3D""0"" style=3D""height: 1570px; width: 100%; table-layou=
t: fixed; border-spacing: 0px; border-collapse: collapse;""><tbody><tr><td s=
tyle=3D""width: 100%; padding: 5px 30px; vertical-align: top; font-size: 14p=
x; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; color: rgb=
(51, 51, 51);"" class=3D""content-wrapper""><div class=3D""clearfix cke_editabl=
e cke_editable_inline cke_contents_ltr cke_show_borders"" style=3D""overflow-=
wrap: break-word; position: relative;"" tabindex=3D""0"" spellcheck=3D""false"" =
role=3D""textbox"" aria-label=3D""false"" aria-describedby=3D""cke_86""><br><span=
 style=3D""font-family:Arial,Helvetica,sans-serif""><span style=3D""font-size:=
16px""><span style=3D""line-height:1.5""><span style=3D""background-color:#ffff=
cc"">5 =D1=84=D0=B5=D0=B2=D1=80=D0=B0=D0=BB=D1=8F =D0=B2 19:00&nbsp;</span><=
br><strong>=D0=9E=D1=82=D0=BA=D1=80=D1=8B=D1=82=D0=B0=D1=8F =D0=BB=D0=B5=D0=
=BA=D1=86=D0=B8=D1=8F =C2=AB=D0=98=D1=81=D1=82=D0=BE=D1=80=D0=B8=D1=8F C=D0=
=BE=D0=B2=D1=80=D0=B5=D0=BC=D0=B5=D0=BD=D0=BD=D0=BE=D0=B9 =D0=9C=D1=83=D0=
=B7=D1=8B=D0=BA=D0=B8=C2=BB&nbsp;</strong><br>=D0=90=D0=BD=D0=B0=D1=82=D0=
=BE=D0=BB=D0=B8=D0=B9 =D0=90=D0=B9=D1=81 =D1=80=D0=B0=D1=81=D1=81=D0=BA=D0=
=B0=D0=B6=D0=B5=D1=82, =D1=87=D1=82=D0=BE =D0=BF=D0=BE=D1=81=D0=BB=D1=83=D0=
=B6=D0=B8=D0=BB=D0=BE =D1=81=D1=82=D0=B8=D0=BC=D1=83=D0=BB=D0=BE=D0=BC =D0=
=B4=D0=BB=D1=8F =D0=BF=D0=BE=D1=8F=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=D1=8F =D1=
=80=D0=BE=D0=BA=D0=B0, =D1=85=D0=B8=D0=BF-=D1=85=D0=BE=D0=BF=D0=B0 =D0=B8 =
=D0=B4=D0=B8=D1=81=D0=BA=D0=BE, =D1=87=D0=B5=D0=BC =D1=8D=D1=82=D0=B8 =D1=
=8F=D1=80=D0=BA=D0=B8=D0=B5 =D0=B6=D0=B0=D0=BD=D1=80=D1=8B =D0=B8 =D1=81=D1=
=82=D0=B8=D0=BB=D0=B8 =D1=80=D0=B0=D0=B7=D0=BB=D0=B8=D1=87=D0=BD=D1=8B =D0=
=BC=D0=B5=D0=B6=D0=B4=D1=83 =D1=81=D0=BE=D0=B1=D0=BE=D0=B9 =D0=B8 =D0=BA=D0=
=B0=D0=BA =D0=BD=D0=B0=D0=BF=D1=80=D0=B0=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=D1=
=8F XX =D0=B2=D0=B5=D0=BA=D0=B0 =D1=81=D0=B2=D1=8F=D0=B7=D0=B0=D0=BD=D1=8B =
=D0=B2 =D0=B5=D0=B4=D0=B8=D0=BD=D1=83=D1=8E =D0=B8=D1=81=D1=82=D0=BE=D1=80=
=D0=B8=D1=8E =D0=BC=D1=83=D0=B7=D1=8B=D0=BA=D0=B8. =D0=A2=D0=B0=D0=BA=D0=B6=
=D0=B5 =D1=81=D0=BB=D1=83=D1=88=D0=B0=D1=82=D0=B5=D0=BB=D0=B8 =D0=BB=D0=B5=
=D0=BA=D1=86=D0=B8=D0=B8 =D0=BE=D0=B1=D1=81=D1=83=D0=B4=D1=8F=D1=82 =D1=82=
=D0=B5=D1=85=D0=BD=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D0=B5 =D0=BE=D1=81=
=D0=BE=D0=B1=D0=B5=D0=BD=D0=BD=D0=BE=D1=81=D1=82=D0=B8 =D1=81=D1=82=D0=B8=
=D0=BB=D0=B5=D0=B9, =D1=87=D1=82=D0=BE=D0=B1=D1=8B =D0=BB=D1=83=D1=87=D1=88=
=D0=B5 =D0=BE=D0=BF=D1=80=D0=B5=D0=B4=D0=B5=D0=BB=D1=8F=D1=82=D1=8C =D0=B8=
=D1=85 =D1=80=D0=B0=D0=B7=D0=BB=D0=B8=D1=87=D0=B8=D1=8F =D0=BD=D0=B0 =D1=81=
=D0=BB=D1=83=D1=85.&nbsp;<br>=D0=90=D0=B4=D1=80=D0=B5=D1=81: =D0=9C=D0=B0=
=D0=BB=D0=B0=D1=8F =D0=9F=D0=B8=D0=BE=D0=BD=D0=B5=D1=80=D1=81=D0=BA=D0=B0=
=D1=8F, 12<br><a data-cke-saved-href=3D""https://hse-design.timepad.ru/event=
/1235118"" href=3D""http://unimail.hse.ru/ru/mail_link_tracker?hash=3D6rpwhwz=
xj6opj1jnr33pasja89w17rz1ua7fpnpfyw64x53qjt31dsf91ooxsgqx1hn6qt9rpxbnr8cfzq=
rh53f9cfwknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9oc2UtZGVzaWduLnRpbWVwY=
WQucnUvZXZlbnQvMTIzNTExOD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVy=
JnV0bV9jYW1wYWlnbj0yMjYxMDMyMDA~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb=
(0,127,255);"">=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=
=D1=8F</a><br><br><span style=3D""background-color:#ffffcc"">5 =D1=84=D0=B5=
=D0=B2=D1=80=D0=B0=D0=BB=D1=8F =D0=B2 17:00</span><br><strong>=D0=AD=D0=BA=
=D0=BE-=D0=B1=D0=B8=D0=B7=D0=BD=D0=B5=D1=81 =D0=B2 =D0=A0=D0=BE=D1=81=D1=81=
=D0=B8=D0=B8: =D0=BA=D0=B5=D0=B9=D1=81 =D0=B1=D1=80=D0=B5=D0=BD=D0=B4=D0=B0=
 =D0=BE=D1=80=D0=B3=D0=B0=D0=BD=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=BE=D0=B9 =
=D0=BE=D0=B4=D0=B5=D0=B6=D0=B4=D1=8B</strong><br>=D0=9E=D1=81=D0=BD=D0=BE=
=D0=B2=D0=B0=D1=82=D0=B5=D0=BB=D1=8C =D1=8D=D0=BA=D0=BE-=D0=B1=D1=80=D0=B5=
=D0=BD=D0=B4=D0=B0 Petrichor =D0=90=D0=BD=D0=BD=D0=B0 =D0=9F=D0=BE=D0=B3=D0=
=BE=D0=B4=D0=B8=D0=BD=D0=B0 =D1=80=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B6=D0=
=B5=D1=82, =D0=BA=D0=B0=D0=BA =D1=81=D0=BE=D0=B7=D0=B4=D0=B0=D1=82=D1=8C =
=D0=B1=D0=B8=D0=B7=D0=BD=D0=B5=D1=81 =D1=81 =D0=BD=D0=B0=D0=B8=D0=BC=D0=B5=
=D0=BD=D1=8C=D1=88=D0=B8=D0=BC =D1=83=D1=89=D0=B5=D1=80=D0=B1=D0=BE=D0=BC =
=D0=B4=D0=BB=D1=8F =D0=BF=D1=80=D0=B8=D1=80=D0=BE=D0=B4=D1=8B. =D0=A3=D1=87=
=D0=B0=D1=81=D1=82=D0=BD=D0=B8=D0=BA=D0=B8 =D0=BB=D0=B5=D0=BA=D1=86=D0=B8=
=D0=B8 =D1=81=D0=BC=D0=BE=D0=B3=D1=83=D1=82 =D1=83=D0=B7=D0=BD=D0=B0=D1=82=
=D1=8C =D0=B1=D0=BE=D0=BB=D1=8C=D1=88=D0=B5 =D0=BE=D0=B1 =D0=BE=D1=80=D0=B3=
=D0=B0=D0=BD=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D1=85 =D1=82=D0=BA=D0=B0=
=D0=BD=D1=8F=D1=85, =D0=BF=D0=BE=D1=83=D1=87=D0=B0=D1=81=D1=82=D0=B2=D0=BE=
=D0=B2=D0=B0=D1=82=D1=8C =D0=B2 =D0=B8=D0=BD=D1=82=D0=B5=D1=80=D0=B0=D0=BA=
=D1=82=D0=B8=D0=B2=D0=B5 =D0=B8 =D0=BF=D0=BE=D0=BE=D0=B1=D1=89=D0=B0=D1=82=
=D1=8C=D1=81=D1=8F =D1=81 =D0=B1=D0=BB=D0=BE=D0=B3=D0=B5=D1=80=D0=BE=D0=BC =
=D0=B8=D0=B7 =D1=81=D1=84=D0=B5=D1=80=D1=8B =D0=BC=D0=BE=D0=B4=D1=8B.<br>=
=D0=90=D0=B4=D1=80=D0=B5=D1=81: =D0=9F=D0=BE=D0=BA=D1=80=D0=BE=D0=B2=D1=81=
=D0=BA=D0=B8=D0=B9 =D0=B1-=D1=80, 11, =D0=B0=D1=83=D0=B4. R306<br><a data-c=
ke-saved-href=3D""https://greenhse.timepad.ru/event/1241046/"" href=3D""http:/=
/unimail.hse.ru/ru/mail_link_tracker?hash=3D6k8kw1ueu9cpp4jnr33pasja89w17rz=
1ua7fpnpfyw64x53qjt31coxjh3zjj3r911cywgaid86cif6ardjf311q9raknt3a4jmx9fqz66=
eftcjthdgco&url=3DaHR0cHM6Ly9ncmVlbmhzZS50aW1lcGFkLnJ1L2V2ZW50LzEyNDEwNDYvP=
3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIyNjEw=
MzIwMA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=A0=D0=
=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</a><br><br><span =
style=3D""background-color:#ffffcc"">5 =D1=84=D0=B5=D0=B2=D1=80=D0=B0=D0=BB=
=D1=8F =D0=B2 18:00</span><br><strong>=D0=9B=D0=B5=D0=BA=D1=86=D0=B8=D1=8F =
=C2=AB=D0=9C=D0=B5=D0=BD=D1=8F=D1=8E=D1=89=D0=B0=D1=8F=D1=81=D1=8F =D0=9A=
=D1=83=D0=B1=D0=B0=C2=BB</strong><br>=D0=A0=D0=B0=D0=B7=D0=B3=D0=BE=D0=B2=
=D0=BE=D1=80 =D0=BF=D0=BE=D0=B9=D0=B4=D0=B5=D1=82 =D0=BE =D0=BA=D1=83=D0=B1=
=D0=B8=D0=BD=D0=BE-=D0=B0=D0=BC=D0=B5=D1=80=D0=B8=D0=BA=D0=B0=D0=BD=D1=81=
=D0=BA=D0=B8=D1=85 =D0=BE=D1=82=D0=BD=D0=BE=D1=88=D0=B5=D0=BD=D0=B8=D1=8F=
=D1=85, =D1=81=D0=BE=D0=B1=D1=8B=D1=82=D0=B8=D1=8F=D1=85 =D0=B2 =D0=92=D0=
=B5=D0=BD=D0=B5=D1=81=D1=83=D1=8D=D0=BB=D0=B5 =D0=B8 =D0=BF=D0=BE=D0=B2=D1=
=81=D0=B5=D0=B4=D0=BD=D0=B5=D0=B2=D0=BD=D0=BE=D0=B9 =D0=B6=D0=B8=D0=B7=D0=
=BD=D0=B8 =D0=BA=D1=83=D0=B1=D0=B8=D0=BD=D1=86=D0=B5=D0=B2. =D0=92=D0=B5=D0=
=B4=D1=83=D1=89=D0=B8=D0=B9 =D0=BB=D0=B5=D0=BA=D1=86=D0=B8=D0=B8 =E2=80=93 =
<span style=3D""font-family:Arial,Helvetica,sans-serif""><span style=3D""font-=
size:16px""><span style=3D""line-height:1.5"">=D0=9C=D0=B0=D0=BA=D1=81=D0=B8=
=D0=BC =D0=9B=D1=8B=D1=81=D0=B5=D0=BD=D0=BA=D0=BE,&nbsp;</span></span></spa=
n>=D1=81=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82 =D0=B2=D1=82=D0=BE=D1=80=D0=BE=
=D0=B3=D0=BE =D0=BA=D1=83=D1=80=D1=81=D0=B0 =D0=BC=D0=B0=D0=B3=D0=B8=D1=81=
=D1=82=D1=80=D0=B0=D1=82=D1=83=D1=80=D1=8B,&nbsp; =D0=B8=D1=81=D1=81=D0=BB=
=D0=B5=D0=B4=D0=BE=D0=B2=D0=B0=D1=82=D0=B5=D0=BB=D1=8C, =D1=81=D0=BF=D0=B5=
=D1=86=D0=B8=D0=B0=D0=BB=D0=B8=D0=B7=D0=B8=D1=80=D1=83=D1=8E=D1=89=D0=B8=D0=
=B9=D1=81=D1=8F =D0=BD=D0=B0 =D0=B8=D0=B7=D1=83=D1=87=D0=B5=D0=BD=D0=B8=D0=
=B8 =D0=9A=D1=83=D0=B1=D1=8B.<br>=D0=90=D0=B4=D1=80=D0=B5=D1=81: =D0=9F=D0=
=BE=D0=BA=D1=80=D0=BE=D0=B2=D1=81=D0=BA=D0=B8=D0=B9 =D0=B1-=D1=80, 11, =D0=
=B0=D1=83=D0=B4. S319<br><a data-cke-saved-href=3D""https://hselatinamerica.=
timepad.ru/event/1245746/"" href=3D""http://unimail.hse.ru/ru/mail_link_track=
er?hash=3D6wwof1ufjcpki6jnr33pasja89w17rz1ua7fpnpfyw64x53qjt31rt77pg7fxco6h=
g8ezf4eba588p6ardjf311q9raknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9oc2Vs=
YXRpbmFtZXJpY2EudGltZXBhZC5ydS9ldmVudC8xMjQ1NzQ2Lz91dG1fbWVkaXVtPWVtYWlsJnV=
0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMjYxMDMyMDA~&uid=3DMTMyMzY3NA=
=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=
=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</a><br><br><span style=3D""background-colo=
r:#ffffcc"">6 =D1=84=D0=B5=D0=B2=D1=80=D0=B0=D0=BB=D1=8F =D0=B2 19:00</span>=
<br><strong>=D0=9B=D0=B5=D0=BA=D1=86=D0=B8=D1=8F =C2=AB=D0=A3=D0=BF=D1=80=
=D0=B0=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=D0=B5 =D1=80=D0=B5=D0=BF=D1=83=D1=82=
=D0=B0=D1=86=D0=B8=D0=B5=D0=B9 =D0=B2 =D0=B8=D0=BD=D1=82=D0=B5=D1=80=D0=BD=
=D0=B5=D1=82=D0=B5=C2=BB</strong><br>=D0=97=D0=B0=D1=87=D0=B5=D0=BC =D1=83=
=D0=BF=D1=80=D0=B0=D0=B2=D0=BB=D1=8F=D1=82=D1=8C =D1=80=D0=B5=D0=BF=D1=83=
=D1=82=D0=B0=D1=86=D0=B8=D0=B5=D0=B9, =D0=BA=D0=B0=D0=BA=D0=B8=D0=B5 =D1=81=
=D1=83=D1=89=D0=B5=D1=81=D1=82=D0=B2=D1=83=D1=8E=D1=82 =D0=B8=D0=BD=D1=81=
=D1=82=D1=80=D1=83=D0=BC=D0=B5=D0=BD=D1=82=D1=8B =D0=B4=D0=BB=D1=8F =D0=BA=
=D0=BE=D1=80=D1=80=D0=B5=D0=BA=D1=82=D0=B8=D1=80=D0=BE=D0=B2=D0=BA=D0=B8 =
=D0=B8 =D1=84=D0=BE=D1=80=D0=BC=D0=B8=D1=80=D0=BE=D0=B2=D0=B0=D0=BD=D0=B8=
=D1=8F =D0=B8=D0=BD=D1=84=D0=BE=D1=80=D0=BC=D0=B0=D1=86=D0=B8=D0=BE=D0=BD=
=D0=BD=D0=BE=D0=B3=D0=BE =D1=84=D0=BE=D0=BD=D0=B0 =D0=B8 =D0=BA=D0=B0=D0=BA=
 =D0=91=D0=BE=D0=BB=D1=8C=D1=88=D0=BE=D0=B9 =D0=91=D1=80=D0=B0=D1=82 =D1=81=
=D0=BB=D0=B5=D0=B4=D0=B8=D1=82 =D0=B7=D0=B0 =D0=BA=D0=B0=D0=B6=D0=B4=D1=8B=
=D0=BC =D0=B2=D0=B0=D1=88=D0=B8=D0=BC =D0=BB=D0=B0=D0=B9=D0=BA=D0=BE=D0=BC =
=D0=B8 =D0=BA=D0=BE=D0=BC=D0=BC=D0=B5=D0=BD=D1=82=D0=B0=D1=80=D0=B8=D0=B5=
=D0=BC? =D0=9E=D0=B1 =D1=8D=D1=82=D0=BE=D0=BC =D1=80=D0=B0=D1=81=D1=81=D0=
=BA=D0=B0=D0=B6=D0=B5=D1=82 =D0=92=D0=BB=D0=B0=D0=B4=D0=B8=D1=81=D0=BB=D0=
=B0=D0=B2 =D0=A1=D0=B8=D0=BD=D1=87=D1=83=D0=B3=D0=BE=D0=B2, =D1=80=D1=83=D0=
=BA=D0=BE=D0=B2=D0=BE=D0=B4=D0=B8=D1=82=D0=B5=D0=BB=D1=8C =D0=B4=D0=B5=D0=
=BF=D0=B0=D1=80=D1=82=D0=B0=D0=BC=D0=B5=D0=BD=D1=82=D0=B0 =D1=80=D0=B5=D0=
=BA=D0=BB=D0=B0=D0=BC=D1=8B =D0=B2 =D1=81=D0=BE=D1=86=D0=B8=D0=B0=D0=BB=D1=
=8C=D0=BD=D1=8B=D1=85 =D0=BC=D0=B5=D0=B4=D0=B8=D0=B0 =D0=B8 =D1=83=D0=BF=D1=
=80=D0=B0=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=D1=8F =D1=80=D0=B5=D0=BF=D1=83=D1=
=82=D0=B0=D1=86=D0=B8=D0=B5=D0=B9, =D0=BA=D0=BE=D0=BC=D0=BF=D0=B0=D0=BD=D0=
=B8=D1=8F =C2=AB=D0=90=D1=88=D0=BC=D0=B0=D0=BD=D0=BE=D0=B2 =D0=B8 =D0=BF=D0=
=B0=D1=80=D1=82=D0=BD=D0=B5=D1=80=D1=8B=C2=BB.<br>=D0=90=D0=B4=D1=80=D0=B5=
=D1=81: =D1=83=D0=BB. =D0=A2=D1=80=D0=B8=D1=84=D0=BE=D0=BD=D0=BE=D0=B2c=D0=
=BA=D0=B0=D1=8F, =D0=B4.57, =D1=81=D1=82=D1=80. 1 (=D1=81=D1=82. =D0=BC=D0=
=B5=D1=82=D1=80=D0=BE =D0=A0=D0=B8=D0=B6=D1=81=D0=BA=D0=B0=D1=8F), =D0=B0=
=D1=83=D0=B4. 507.<br><a data-cke-saved-href=3D""https://hsbi.hse.ru/events/=
open_lectures/upravlenie-reputatsiey-v-internete/#.XjfYhWgzaUl"" href=3D""htt=
p://unimail.hse.ru/ru/mail_link_tracker?hash=3D6fp51b9aq74ufkjnr33pasja89w1=
7rz1ua7fpnpfyw64x53qjt31ng1kg41qhh3e91u87yeu5d5iw1nsnyzrngg11eoknt3a4jmx9fq=
z66eftcjthdgco&url=3DaHR0cHM6Ly9oc2JpLmhzZS5ydS9ldmVudHMvb3Blbl9sZWN0dXJlcy=
91cHJhdmxlbmllLXJlcHV0YXRzaWV5LXYtaW50ZXJuZXRlLz91dG1fbWVkaXVtPWVtYWlsJnV0b=
V9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMjYxMDMyMDAjLlhqZlloV2d6YVVs&uid=
=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=A0=D0=B5=D0=B3=D0=
=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</a><br><br><span style=3D""bac=
kground-color:#ffffcc"">7 =D1=84=D0=B5=D0=B2=D1=80=D0=B0=D0=BB=D1=8F =D0=B2 =
18:10&nbsp;</span><br><strong>=D0=9B=D0=B5=D0=BA=D1=86=D0=B8=D1=8F =C2=AB=
=D0=93=D1=80=D0=B5=D1=87=D0=B5=D1=81=D0=BA=D0=B0=D1=8F =D0=91=D0=B8=D0=B1=
=D0=BB=D0=B8=D1=8F: =D0=B4=D1=80=D0=B5=D0=B2=D0=BD=D0=B5=D0=B2=D0=BE=D1=81=
=D1=82=D0=BE=D1=87=D0=BD=D1=8B=D0=B9 =D1=82=D0=B5=D0=BA=D1=81=D1=82 =D0=B2 =
=D0=B0=D0=BD=D1=82=D0=B8=D1=87=D0=BD=D0=BE=D0=BC =D0=BA=D0=BE=D0=BD=D1=82=
=D0=B5=D0=BA=D1=81=D1=82=D0=B5=C2=BB&nbsp;</strong><br>=D0=A0=D0=B5=D1=87=
=D1=8C =D0=BF=D0=BE=D0=B9=D0=B4=D0=B5=D1=82 =D0=BE =D1=82=D1=80=D1=83=D0=B4=
=D0=BD=D0=BE=D1=81=D1=82=D1=8F=D1=85 =D0=BF=D0=B5=D1=80=D0=B5=D0=B2=D0=BE=
=D0=B4=D0=B0 =D0=B8 =D0=B4=D0=B8=D1=81=D1=82=D0=B0=D0=BD=D1=86=D0=B8=D0=B8 =
=D0=BC=D0=B5=D0=B6=D0=B4=D1=83 =D0=B4=D0=B2=D1=83=D0=BC=D1=8F =D0=BA=D1=83=
=D0=BB=D1=8C=D1=82=D1=83=D1=80=D0=B0=D0=BC=D0=B8. =D0=92=D1=81=D1=82=D1=80=
=D0=B5=D1=87=D0=B0 =D1=81=D0=BE=D1=81=D1=82=D0=BE=D0=B8=D1=82=D1=81=D1=8F =
=D0=B2 =D1=80=D0=B0=D0=BC=D0=BA=D0=B0=D1=85 =D1=86=D0=B8=D0=BA=D0=BB=D0=B0 =
=C2=AB=D0=92=D0=BE=D1=81=D0=B5=D0=BC=D1=8C =D0=BB=D0=B5=D0=BA=D1=86=D0=B8=
=D0=B9 =D0=BE =D0=92=D0=BE=D1=81=D1=82=D0=BE=D0=BA=D0=B5=C2=BB, =D0=BE=D1=
=80=D0=B3=D0=B0=D0=BD=D0=B8=D0=B7=D0=BE=D0=B2=D0=B0=D0=BD=D0=BD=D0=BE=D0=B3=
=D0=BE =D0=98=D0=BD=D1=81=D1=82=D0=B8=D1=82=D1=83=D1=82=D0=BE=D0=BC =D0=BA=
=D0=BB=D0=B0=D1=81=D1=81=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=BE=D0=B3=D0=BE =
=D0=92=D0=BE=D1=81=D1=82=D0=BE=D0=BA=D0=B0 =D0=B8 =D0=B0=D0=BD=D1=82=D0=B8=
=D1=87=D0=BD=D0=BE=D1=81=D1=82=D0=B8 =D0=B8 =D0=A4=D0=B0=D0=BA=D1=83=D0=BB=
=D1=8C=D1=82=D0=B5=D1=82=D0=BE=D0=BC =D0=BA=D0=BE=D0=BC=D0=BF=D1=8C=D1=8E=
=D1=82=D0=B5=D1=80=D0=BD=D1=8B=D1=85 =D0=BD=D0=B0=D1=83=D0=BA.&nbsp;<br>=D0=
=90=D0=B4=D1=80=D0=B5=D1=81: =D0=9F=D0=BE=D0=BA=D1=80=D0=BE=D0=B2=D1=81=D0=
=BA=D0=B8=D0=B9 =D0=B1-=D1=80, 11, =D0=B0=D1=83=D0=B4. R404<br><a data-cke-=
saved-href=3D""https://iocs.hse.ru/announcements/337698993.html"" href=3D""htt=
p://unimail.hse.ru/ru/mail_link_tracker?hash=3D6fhhjfbcqz11jkjnr33pasja89w1=
7rz1ua7fpnpfyw64x53qjt31jrkgjnf6rxbn7mjsdjkwdhi6ui9yzhx5t8ngiyhknt3a4jmx9fq=
z66eftcjthdgco&url=3DaHR0cHM6Ly9pb2NzLmhzZS5ydS9hbm5vdW5jZW1lbnRzLzMzNzY5OD=
k5My5odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBha=
WduPTIyNjEwMzIwMA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);""=
>=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</a>&nbs=
p;<br><br><span style=3D""background-color:#ffffcc"">8 =D1=84=D0=B5=D0=B2=D1=
=80=D0=B0=D0=BB=D1=8F =D0=B2 18:00</span><br><strong>=D0=9F=D1=80=D0=BE=D1=
=81=D0=BC=D0=BE=D1=82=D1=80 =D0=B8 =D0=BE=D0=B1=D1=81=D1=83=D0=B6=D0=B4=D0=
=B5=D0=BD=D0=B8=D0=B5 =D1=84=D0=B8=D0=BB=D1=8C=D0=BC=D0=B0 =C2=AB=D0=9C=D0=
=BD=D0=B5 =D0=B4=D0=B2=D0=B0=D0=B4=D1=86=D0=B0=D1=82=D1=8C =D0=BB=D0=B5=D1=
=82=C2=BB</strong><br>=D0=9E=D0=B4=D0=BD=D0=B0 =D0=B8=D0=B7 =D0=B3=D0=BB=D0=
=B0=D0=B2=D0=BD=D1=8B=D1=85 =D0=BA=D0=B0=D1=80=D1=82=D0=B8=D0=BD =C2=AB=D1=
=81=D0=BE=D0=B2=D0=B5=D1=82=D1=81=D0=BA=D0=BE=D0=B9 =D0=BD=D0=BE=D0=B2=D0=
=BE=D0=B9 =D0=B2=D0=BE=D0=BB=D0=BD=D1=8B=C2=BB =D0=BF=D1=80=D0=BE =D0=BE=D1=
=82=D1=82=D0=B5=D0=BF=D0=B5=D0=BB=D1=8C =D0=B8 =D0=BF=D0=BE=D0=BA=D0=BE=D0=
=BB=D0=B5=D0=BD=D0=B8=D0=B5 =D0=BC=D0=BE=D0=BB=D0=BE=D0=B4=D1=8B=D1=85 =D0=
=BB=D1=8E=D0=B4=D0=B5=D0=B9, =D0=BA=D0=BE=D1=82=D0=BE=D1=80=D0=BE=D0=B5 =D1=
=81=D0=BE=D0=BC=D0=BD=D0=B5=D0=B2=D0=B0=D0=B5=D1=82=D1=81=D1=8F =D0=B8 =D0=
=B8=D1=89=D0=B5=D1=82 =D1=81=D0=B2=D0=BE=D0=B5 =D0=BC=D0=B5=D1=81=D1=82=D0=
=BE =D0=B2 =D0=B6=D0=B8=D0=B7=D0=BD=D0=B8.<br>=D0=90=D0=B4=D1=80=D0=B5=D1=
=81: =D0=A1=D1=82=D0=B0=D1=80=D0=B0=D1=8F =D0=91=D0=B0=D1=81=D0=BC=D0=B0=D0=
=BD=D0=BD=D0=B0=D1=8F, 21/4, =D0=B0=D1=83=D0=B4. 501<br><a data-cke-saved-h=
ref=3D""https://docs.google.com/forms/d/e/1FAIpQLSf77gdzts82XWzZsPoYwslgORUT=
8MqItv4ZdondiU0A7Wd_qQ/viewform"" href=3D""http://unimail.hse.ru/ru/mail_link=
_tracker?hash=3D6hob3dpczusnahjnr33pasja89w17rz1ua7fpnpfyw64x53qjt31bmx3zef=
bxboprg84rwou8r53sbcfzqrh53f9cfwknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly=
9kb2NzLmdvb2dsZS5jb20vZm9ybXMvZC9lLzFGQUlwUUxTZjc3Z2R6dHM4MlhXelpzUG9Zd3NsZ=
09SVVQ4TXFJdHY0WmRvbmRpVTBBN1dkX3FRL3ZpZXdmb3JtP3V0bV9tZWRpdW09ZW1haWwmdXRt=
X3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIyNjEwMzIwMA~~&uid=3DMTMyMzY3NA=
=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=
=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</a><br><br><span style=3D""background-colo=
r:#ffffcc"">8-9 =D1=84=D0=B5=D0=B2=D1=80=D0=B0=D0=BB=D1=8F</span><br><strong=
>=D0=9F=D0=BE=D1=85=D0=BE=D0=B4 =D1=81 =D0=A2=D1=83=D1=80=D0=BA=D0=BB=D1=83=
=D0=B1=D0=BE=D0=BC =D0=92=D0=A8=D0=AD</strong><br>=D0=92=D1=8B=D1=85=D0=BE=
=D0=B4=D0=BD=D1=8B=D0=B5 =D0=BD=D0=B0 =D0=BF=D1=80=D0=B8=D1=80=D0=BE=D0=B4=
=D0=B5 =D0=B2 =D0=BE=D1=82=D0=BB=D0=B8=D1=87=D0=BD=D0=BE=D0=B9 =D0=BA=D0=BE=
=D0=BC=D0=BF=D0=B0=D0=BD=D0=B8=D0=B8. =D0=9F=D0=B0=D0=BB=D0=B0=D1=82=D0=BA=
=D0=B0, =D0=BA=D0=BE=D1=81=D1=82=D0=B5=D1=80, =D0=BF=D0=BE=D1=85=D0=BE=D0=
=B4=D0=BD=D1=8B=D0=B5 =D0=B8=D1=81=D1=82=D0=BE=D1=80=D0=B8=D0=B8 =E2=80=93 =
=D0=B2=D1=81=D0=B5 =D0=BF=D0=BE-=D0=BD=D0=B0=D1=81=D1=82=D0=BE=D1=8F=D1=89=
=D0=B5=D0=BC=D1=83! =D0=95=D1=81=D0=BB=D0=B8 =D1=85=D0=BE=D1=82=D0=B8=D1=82=
=D0=B5 =D1=85=D0=BE=D1=80=D0=BE=D1=88=D0=B5=D0=BD=D1=8C=D0=BA=D0=BE =D0=BF=
=D0=BE=D0=B4=D0=B3=D0=BE=D1=82=D0=BE=D0=B2=D0=B8=D1=82=D1=8C=D1=81=D1=8F, =
=D0=BF=D1=80=D0=B8=D1=85=D0=BE=D0=B4=D0=B8=D1=82=D0=B5 =D0=BD=D0=B0 <a data=
-cke-saved-href=3D""https://vk.com/tkhse"" href=3D""http://unimail.hse.ru/ru/m=
ail_link_tracker?hash=3D6iih8tt5p7nxwrjnr33pasja89w17rz1ua7fpnpfyw64x53qjt3=
1kez4erx16ofrrwwmo7einm1u4gdowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaH=
R0cHM6Ly92ay5jb20vdGtoc2U_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlc=
iZ1dG1fY2FtcGFpZ249MjI2MTAzMjAw&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(=
0,127,255);"">=D0=BB=D0=B5=D0=BA=D1=86=D0=B8=D1=8E</a> =D0=A2=D1=83=D1=80=D0=
=BA=D0=BB=D1=83=D0=B1=D0=B0 3 =D1=84=D0=B5=D0=B2=D1=80=D0=B0=D0=BB=D1=8F =
=D0=B2 19:00&nbsp;<br>=D0=90=D0=B4=D1=80=D0=B5=D1=81: &nbsp;=D0=9F=D0=BE=D0=
=B4=D0=BC=D0=BE=D1=81=D0=BA=D0=BE=D0=B2=D1=8C=D0=B5, =D0=BF=D0=BB. =D0=A1=
=D0=B0=D0=BD=D0=B0=D1=82=D0=BE=D1=80=D0=BD=D0=B0=D1=8F<br><a data-cke-saved=
-href=3D""https://vk.com/tkhse?w=3Dwall-3102426_5809"" href=3D""http://unimail=
.hse.ru/ru/mail_link_tracker?hash=3D6sotfwar9dfw61jnr33pasja89w17rz1ua7fpnp=
fyw64x53qjt31raq4ych9zom8bo3556m3r5r8uhnsnyzrngg11eoknt3a4jmx9fqz66eftcjthd=
gco&url=3DaHR0cHM6Ly92ay5jb20vdGtoc2U_dz13YWxsLTMxMDI0MjZfNTgwOSZ1dG1fbWVka=
XVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMjYxMDMyMDA~&uid=
=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=A0=D0=B5=D0=B3=D0=
=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</a></span></span></span></div=
></td></tr></tbody></table></td></tr></tbody></table><!--[if (gte mso 9)|(I=
E)]></td></tr></table><![endif]--><!--[if (gte mso 9)|(IE)]><table cellpadd=
ing=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""center""><tr=
><td><![endif]--><table class=3D""uni-block button-block"" width=3D""100%"" bor=
der=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-l=
ayout: fixed; height: auto; border-collapse: collapse; border-spacing: 0px;=
 display: inline-table; vertical-align: top; font-size: medium;""><tbody><tr=
><td style=3D""width: 100%; background-image: none; min-height: 50px; height=
: 50px;"" class=3D""block-wrapper"" valign=3D""middle""><table class=3D""block-wr=
apper-inner-table"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=
=3D""height: 100%; width: 100%; table-layout: fixed; border-spacing: 0px; bo=
rder-collapse: collapse; min-height: 50px;""><tbody><tr><td style=3D""width: =
100%; text-align: center;"" class=3D""content-wrapper""><table class=3D""valign=
-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""display=
: inline-table; width: auto; border-spacing: 0px; border-collapse: collapse=
;""><tbody><tr><td class=3D""button-wrapper"" align=3D""center"" valign=3D""middl=
e"" style=3D""border: none; border-radius: 10px; padding: 0px 30px; backgroun=
d-color: rgb(0, 95, 129); height: 50px; min-height: 50px;""><a class=3D""mail=
btn"" href=3D""http://unimail.hse.ru/ru/mail_link_tracker?hash=3D6cyosd5333g3=
d6jnr33pasja89w17rz1ua7fpnpfyw64x53qjt31yur56wnau7pk564ymmamdght5ucfzqrh53f=
9cfwknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLz=
MzOTAwODYwMC5odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX=
2NhbXBhaWduPTIyNjEwMzIwMA~~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank"" style=
=3D""width:100%;display:inline-block;text-decoration:none;""><span class=3D""b=
tn-inner"" style=3D""display: inline; font-size: 16px; font-family: Arial, He=
lvetica, sans-serif; line-height: 19.2px; color: rgb(255, 255, 255); backgr=
ound-color: rgb(0, 95, 129); border: 0px; word-break: break-all;"">=D0=92=D1=
=81=D0=B5 =D1=81=D0=BE=D0=B1=D1=8B=D1=82=D0=B8=D1=8F</span></a></td></tr></=
tbody></table></td></tr></tbody></table></td></tr></tbody></table><!--[if (=
gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)|(IE)]><t=
able cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=
=3D""center""><tr><td><![endif]--><table class=3D""uni-block line-block"" width=
=3D""100%"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: =
100%; table-layout: fixed; height: auto; border-collapse: collapse; border-=
spacing: 0px; display: inline-table; vertical-align: top; font-size: medium=
; min-height: 10px;""><tbody><tr><td style=3D""width: 100%; background-image:=
 none; height: 100%; vertical-align: middle; min-height: auto; font-size: m=
edium;"" class=3D""block-wrapper"" valign=3D""top""><table class=3D""block-wrappe=
r-inner-table"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""he=
ight: 10px; width: 100%; table-layout: fixed; border-spacing: 0px; border-c=
ollapse: collapse; min-height: 10px;""><tbody><tr><td style=3D""width: 100%; =
vertical-align: middle; height: 10px; min-height: 10px;"" class=3D""content-w=
rapper""><table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""wi=
dth: 100%; table-layout: fixed; border-spacing: 0; border-collapse: collaps=
e; font-size: 0;""><tbody><tr><td class=3D""separator-line"" style=3D""width: 1=
00%; background-color: rgb(255, 255, 255); height: 1px; min-height: 1px; ma=
x-height: 1px; line-height: 1px;"">&nbsp;</td></tr></tbody></table></td></tr=
></tbody></table></td></tr></tbody></table><!--[if (gte mso 9)|(IE)]></td><=
/tr></table><![endif]--><!--[if (gte mso 9)|(IE)]><table cellpadding=3D""0"" =
cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""center""><tr><td><![en=
dif]--><table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" ce=
llspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed=
; height: auto; border-collapse: collapse; border-spacing: 0px; display: in=
line-table; vertical-align: top; font-size: medium;""><tbody><tr><td style=
=3D""width: 100%; background-color: rgb(255, 255, 255); background-image: no=
ne; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D""top""><ta=
ble class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" cell=
padding=3D""0"" style=3D""height: 67px; width: 100%; table-layout: fixed; bord=
er-spacing: 0px; border-collapse: collapse;""><tbody><tr><td style=3D""width:=
 100%; padding: 5px 30px 20px; vertical-align: top; font-size: 14px; font-f=
amily: Tahoma, Geneva, sans-serif; line-height: 16.8px; color: rgb(51, 51, =
51);"" class=3D""content-wrapper""><div class=3D""clearfix"" style=3D""overflow-w=
rap: break-word;""><span style=3D""font-size:14px;""><em><span style=3D""font-f=
amily:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;"">=D0=
=9F=D0=BE=D1=8F=D0=B2=D0=B8=D0=BB=D0=B8=D1=81=D1=8C =D0=B2=D0=BE=D0=BF=D1=
=80=D0=BE=D1=81=D1=8B =D0=B8=D0=BB=D0=B8 =D0=BF=D1=80=D0=B5=D0=B4=D0=BB=D0=
=BE=D0=B6=D0=B5=D0=BD=D0=B8=D1=8F? =D0=9F=D0=B8=D1=88=D0=B8=D1=82=D0=B5: co=
mmunity@hse.ru<br>=D0=98=D0=BD=D1=81=D1=82=D1=80=D1=83=D0=BA=D1=86=D0=B8=D1=
=8F, =D0=BA=D0=B0=D0=BA =D0=BF=D0=BE=D0=BF=D0=B0=D1=81=D1=82=D1=8C =D0=B2 =
=D0=BA=D0=B0=D0=BB=D0=B5=D0=BD=D0=B4=D0=B0=D1=80=D1=8C =D0=B8 =D0=B4=D0=B0=
=D0=B9=D0=B4=D0=B6=D0=B5=D1=81=D1=82 <span style=3D""font-size:14px;""><em><s=
pan style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line=
-height:1.5;"">STUDLIFE</span></span></em></span>,&nbsp;<a href=3D""http://un=
imail.hse.ru/ru/mail_link_tracker?hash=3D6ha96pbfombfq1jnr33pasja89w17rz1ua=
7fpnpfyw64x53qjt31n97rm4qu9r5atahynhtpxa1ae9cfzqrh53f9cfwknt3a4jmx9fqz66eft=
cjthdgco&url=3DaHR0cHM6Ly92ay5jb20vQHN0dWRsaWZlX2hzZS1mYXE_dXRtX21lZGl1bT1l=
bWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjI2MTAzMjAw&uid=3DMTMy=
MzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D1=82=D1=83=D1=82</a>.</span=
></span></em></span></div></td></tr></tbody></table></td></tr></tbody></tab=
le><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr></tbod=
y></table><!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td></tr=
></tbody></table></td></tr></tbody></table></center><table bgcolor=3D""white=
"" align=3D""left"" width=3D""100%""><tr><td><span style=3D""font-family: arial,h=
elvetica,sans-serif; color: black; font-size: 12px;""><p style=3D""text-align=
: center; color: #bababa;"">=D0=A7=D1=82=D0=BE=D0=B1=D1=8B =D0=BE=D1=82=D0=
=BF=D0=B8=D1=81=D0=B0=D1=82=D1=8C=D1=81=D1=8F =D0=BE=D1=82 =D1=8D=D1=82=D0=
=BE=D0=B9 =D1=80=D0=B0=D1=81=D1=81=D1=8B=D0=BB=D0=BA=D0=B8, =D0=BF=D0=B5=D1=
=80=D0=B5=D0=B9=D0=B4=D0=B8=D1=82=D0=B5 =D0=BF=D0=BE <a style=3D""color: #46=
a8c6;"" href=3D""http://unimail.hse.ru/ru/unsubscribe?hash=3D6piubajd7d6gn815=
7fxz57m156w17rz1ua7fpnpfyw64x53qjt318g3ggqu5cz11zsjozspcmjaf1b85ox1u8n1qzyr=
#no_tracking"">=D1=81=D1=81=D1=8B=D0=BB=D0=BA=D0=B5</a></p></span></td></tr>=
</table><center><table><tr><td><img src=3D""http://unimail.hse.ru/ru/mail_re=
ad_tracker/1323674?hash=3D6fr4c6s66aoa31iwgeupcxy5q8w17rz1ua7fpnpfyw64x53qj=
t31qodt9r56fw7sqp9bbmszibib9x9z1bcmncxy3ur"" width=3D""1"" height=3D""1"" alt=3D=
"""" title=3D"""" border=3D""0""></td></tr></table></center></body></html>";
        
        private const string BodyHse3 = @"
<!DOCTYPE html>
<html>
<head>
<meta name=3D""viewport"" content=3D""width=3Ddevice-width, initial-scale=3D1""=
>
<title></title>

<style type=3D""text/css"">
/* ///////// CLIENT-SPECIFIC STYLES ///////// */
#outlook a{padding:0;} /* Force Outlook to provide a 'view in browser' mess=
age */
.ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail to d=
isplay emails at full width */
.ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font,=
 .ExternalClass td, .ExternalClass div {line-height: 100%;} /* Force Hotmai=
l to display normal line spacing */
body, table, td, p, a, li, blockquote{-webkit-text-size-adjust:100%; -ms-te=
xt-size-adjust:100%;} /* Prevent WebKit and Windows mobile changing default=
 text sizes */
table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;} /* Remove spacing be=
tween tables in Outlook 2007 and up */
img{-ms-interpolation-mode:bicubic;} /* Allow smoother rendering of resized=
 image in Internet Explorer */
/* ///////// RESET STYLES ///////// */
body{margin:0; padding:0;}
img{border:0; height:auto; line-height:100%; outline:none; text-decoration:=
none;}
table{border-collapse:collapse !important;}
table td { border-collapse: collapse !important;}
body, #bodyTable, #bodyCell{height:100% !important; margin:0; padding:0; wi=
dth:100% !important;}
#mailBody.mailBody .uni-block.button-block { display:block; } /* Specific u=
kr.net style*/
body {
margin: 0;
text-align: left;
}
pre {
white-space: pre;
white-space: pre-wrap;
word-wrap: break-word;
}
table.mhLetterPreview { width:100%; }
table {
mso-table-lspace:0pt;
mso-table-rspace:0pt;
}
img {
-ms-interpolation-mode:bicubic;
}
</style>

<style id=3D""media_css"" type=3D""text/css"">
@media all and (max-width: 480px), only screen and (max-device-width : 480p=
x) {
    body{width:100% !important; min-width:100% !important;} /* Prevent iOS =
Mail from adding padding to the body */
    table[class=3D'container-table'] {
       width:100% !important;
    }
    td.image-wrapper {
       padding: 0 !important;
    }
    td.image-wrapper, td.text-wrapper {
       display:block !important;
       width:100% !important;
       max-width:initial !important;
    }
    table[class=3D'wrapper1'] {
       table-layout: fixed !important;
       padding: 0 !important;
       max-width: 600px !important;
    }
    td[class=3D'wrapper-row'] {
       table-layout: fixed !important;
       box-sizing: border-box !important;
       width:100% !important;
       min-width:100% !important;
    }
    table[class=3D'wrapper2'] {
       table-layout: fixed !important;
       border: none !important;
       width: 100% !important;
       max-width: 600px !important;
       min-height: 520px !important;
    }
    div[class=3D'column-wrapper']{
       max-width:300px !important;
    }
    table.uni-block {
       max-width:100% !important;
       height:auto !important;
       min-height: auto !important;
    }
    table[class=3D'block-wrapper-inner-table'] {
       height:auto !important;
       min-height: auto !important;
    }
    td[class=3D'block-wrapper'] {
       height:auto !important;
       min-height: auto !important;
    }
    .submit-button-block .button-wrapper=20
{       height: auto !important;
       width: auto !important;
       min-height: initial !important;
       max-height: initial !important;
       min-width: initial !important;
       max-width: initial !important;
    }
    img[class=3D'image-element'] {
       height:auto !important;
       box-sizing: border-box !important;
    }
}
@media all and (max-width: 615px), only screen and (max-device-width : 615p=
x) {
    td[class=3D'wrapper-row'] {
       padding: 0 !important;
       margin: 0 !important;
    }
    .column {
       width:100% !important;
       max-width:100% !important;
    }
}
</style>
<meta http-equiv=3D""Content-Type"" content=3D""text/html;charset=3DUTF-8"">
</head>
<body width=3D""100%"" align=3D""center"">
<!--[if gte mso 9]>       <style type=3D""text/css"">           .uni-block im=
g {               display:block !important;           }       </style><![en=
dif]-->
<center>
<table cellpadding=3D""0"" cellspacing=3D""0"" align=3D""center"" width=3D""100%"" =
class=3D""container responsive"">
<tbody>
<tr>
<td>
<table cellpadding=3D""0"" cellspacing=3D""0"" align=3D""center"" class=3D""wrappe=
r1"" style=3D""width: 100%; box-sizing: border-box; background-color: rgb(246=
, 246, 246); float: left;"">
<tbody>
<tr>
<td class=3D""wrapper-row"" style=3D""padding: 25px;""><!--[if (gte mso 9)|(IE)=
]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" ali=
gn=3D""center""><tr><td><![endif]-->
<table cellpadding=3D""0"" cellspacing=3D""0"" class=3D""wrapper2"" align=3D""cent=
er"" style=3D""background-color: rgb(255, 255, 255); border-radius: 3px; max-=
width: 600px; width: 100%; border: none; margin: 0px auto; border-spacing: =
0px; border-collapse: collapse;"">
<tbody>
<tr>
<td width=3D""100%"" class=3D""wrapper-row"" style=3D""vertical-align: top; max-=
width: 600px; font-size: 0px; padding: 0px;""><!--[if (gte mso 9)|(IE)]><tab=
le cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""=
center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 44px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 10px 10px 20px; vertical-align: mid=
dle; font-size: 20px; font-family: Arial, Helvetica, sans-serif; line-heigh=
t: 24px; color: rgb(34, 34, 34);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:center;""><span style=3D""font-size:24px;""><strong>=
=D0=92=D1=81=D0=B5=D0=BC =D0=BF=D1=80=D0=B8=D0=B2=D0=B5=D1=82!</strong></sp=
an></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix cke_editable cke_editable_inline cke_contents_ltr ck=
e_show_borders"" style=3D""overflow-wrap: break-word; position: relative;"" ta=
bindex=3D""0"" spellcheck=3D""false"" role=3D""textbox"" aria-label=3D""false"" ari=
a-describedby=3D""cke_45"">
<p><span style=3D""line-height:1.5;""><span style=3D""font-size: 16px; backgro=
und-color: transparent;"">=D0=97=D0=BD=D0=B0=D0=B5=D0=BC, =D1=81=D0=B5=D0=B9=
=D1=87=D0=B0=D1=81 =D1=81=D0=B0=D0=BC=D1=8B=D0=B9 =D0=B7=D0=B0=D0=B2=D0=B0=
=D0=BB =D1=81 =D0=BA=D1=83=D1=80=D1=81=D0=B0=D1=87=D0=B0=D0=BC=D0=B8 =D0=B8=
 =D0=B4=D0=B8=D1=81=D0=B5=D1=80=D0=B0=D0=BC=D0=B8, =D0=BF=D0=BE=D1=8D=D1=82=
=D0=BE=D0=BC=D1=83 =D0=B2=D1=81=D1=91 =D0=BF=D0=BE =D0=BC=D0=B8=D0=BD=D0=B8=
=D0=BC=D1=83=D0=BC=D1=83: =D0=B4=D0=B0=D0=B9=D0=B4=D0=B6=D0=B5=D1=81=D1=82 =
=D1=81=D1=82=D0=B0=D0=B6=D0=B8=D1=80=D0=BE=D0=B2=D0=BE=D0=BA, =D0=BA=D0=B0=
=D0=BB=D0=B5=D0=BD=D0=B4=D0=B0=D1=80=D1=8C =D0=BC=D0=B5=D1=80=D0=BE=D0=BF=
=D1=80=D0=B8=D1=8F=D1=82=D0=B8=D0=B9 =D0=B8 =D0=BD=D0=B5=D1=81=D0=BA=D0=BE=
=D0=BB=D1=8C=D0=BA=D0=BE =D0=BF=D0=BE=D0=BB=D0=B5=D0=B7=D0=BD=D1=8B=D1=85 =
=D1=81=D1=82=D0=B0=D1=82=D0=B5=D0=B9 =D0=BF=D0=BE=D0=B4 =D0=BA=D0=BE=D0=BD=
=D0=B5=D1=86. =D0=92=D1=81=D1=91! =D0=A3=D0=B4=D0=B0=D1=87=D0=B8 :)</span><=
/span></p>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 10px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 10px; min-height:=
 10px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(169, 205, 242); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 29px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix cke_editable cke_editable_inline cke_contents_ltr ck=
e_show_borders"" style=3D""overflow-wrap: break-word; position: relative;"" ta=
bindex=3D""0"" spellcheck=3D""false"" role=3D""textbox"" aria-label=3D""false"" ari=
a-describedby=3D""cke_1008"">
<div style=3D""text-align:center""><span style=3D""font-size:18px""><strong>=D0=
=A1=D0=B2=D0=B5=D0=B6=D0=B8=D0=B5 =D0=B2=D0=B0=D0=BA=D0=B0=D0=BD=D1=81=D0=
=B8=D0=B8 =D0=B4=D0=BB=D1=8F =D0=B2=D1=8B=D1=88=D0=BA=D0=B8=D0=BD=D1=86=D0=
=B5=D0=B2</strong></span></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block image-block"" width=3D""100%"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heig=
ht: auto; border-collapse: collapse; border-spacing: 0px; display: inline-t=
able; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; padding: 0px; height: 100=
%;"" class=3D""block-wrapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 364px; width: 100%; table-layout: fixed; =
text-align: center; border-spacing: 0px; border-collapse: collapse; font-si=
ze: 0px;"">
<tbody>
<tr>
<td style=3D""width: auto; height: 100%; display: inline-table;"" class=3D""co=
ntent-wrapper"">
<table class=3D""content-box"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""=
0"" style=3D""display: inline-table; vertical-align: top; width: auto; height=
: 100%; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""vertical-align: top;"">
<div class=3D""image-wrapper image-drop""><a class=3D""image-link"" href=3D""htt=
ps://unimail.hse.ru/ru/mail_link_tracker?hash=3D6gyeoar8xexif4jnr33pasja89w=
17rz1ua7fpnpnbncejhw71pmuwno6fq76buco8kf5nt81u6rbkeeyztq9354zmy19xro34zj6hp=
e5r5gw4qgwa48ay&url=3DaHR0cHM6Ly9jYXJlZXIuaHNlLnJ1L25ld3MvMzY4NTEzODczLmh0b=
Ww~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank""><img class=3D""image-element"" =
src=3D""http://unimail.hse.ru/ru/user_file?resource=3Dhimg&user_id=3D1323674=
&name=3D6srge4s8gei7f8piu3azzngxco58kpscfzn7jz9isn5pnynhitfu4njjfz76ecdwidr=
rysajqqc6fs"" alt=3D""Some Image"" style=3D""font-size: small; border: none; wi=
dth: 100%; max-width: 600px; height: auto; max-height: 364px; outline: none=
; text-decoration: none;"" id=3D""gridster_block_308_main_img"" width=3D""600"">=
</a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix cke_editable cke_editable_inline cke_contents_ltr ck=
e_show_borders"" style=3D""overflow-wrap: break-word; position: relative;"" ta=
bindex=3D""0"" spellcheck=3D""false"" role=3D""textbox"" aria-label=3D""false"" ari=
a-describedby=3D""cke_718""><span style=3D""line-height:1.5""><span style=3D""fo=
nt-size:16px""><a data-cke-saved-href=3D""http://career.hse.ru/news/368513873=
.html"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6wd4p5q7u=
dzepcjnr33pasja89w17rz1ua7fpnpnbncejhw71pmuzexa6i4cymxt5h1u7n9eyjm5ikdowuby=
7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cDovL2NhcmVlci5oc2UucnUvbmV3cy8=
zNjg1MTM4NzMuaHRtbA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 2=
55);"">=D0=9E=D1=87=D0=B5=D0=BD=D1=8C =D0=BC=D0=BD=D0=BE=D0=B3=D0=BE =D0=BB=
=D0=B8=D0=B4=D0=B5=D1=80=D1=81=D0=BA=D0=B8=D1=85 =D0=BF=D1=80=D0=BE=D0=B3=
=D1=80=D0=B0=D0=BC=D0=BC</a> =D0=BE=D1=82 =D0=90=D1=82=D0=BE=D0=BD, Uniqlo,=
 =D0=9C=D0=BE=D1=81=D0=BA=D0=BE=D0=B2=D1=81=D0=BA=D0=BE=D0=B9 =D0=91=D0=B8=
=D1=80=D0=B6=D0=B8, Ward Howell =D0=B8 =D0=92=D0=A2=D0=91, =D1=82=D0=B0=D0=
=BA=D0=B6=D0=B5 =D0=B5=D1=81=D1=82=D1=8C =D0=B2=D0=B0=D0=BA=D0=B0=D0=BD=D1=
=81=D0=B8=D0=B8 =D1=8D=D0=BA=D1=81=D0=BF=D0=B5=D1=80=D1=82=D0=B0 =D0=B2 =D0=
=A1=D1=87=D0=B5=D1=82=D0=BD=D1=83=D1=8E =D0=9F=D0=B0=D0=BB=D0=B0=D1=82=D1=
=83, =D0=B0=D0=BD=D0=B0=D0=BB=D0=B8=D1=82=D0=B8=D0=BA=D0=B0 =D0=B2 Google, =
=D0=BA=D0=BE=D0=BD=D1=81=D1=83=D0=BB=D1=8C=D1=82=D0=B0=D0=BD=D1=82=D0=B0 =
=D0=B2 BCG, =D0=B0=D1=81=D1=81=D0=B8=D1=81=D1=82=D0=B5=D0=BD=D1=82=D0=B0 =
=D0=BD=D0=B0 =D0=BA=D0=BE=D0=BD=D1=84=D0=B5=D1=80=D0=B5=D0=BD=D1=86=D0=B8=
=D1=8E =C2=ABGenR=C2=BB =D0=B8 =D1=81=D1=82=D0=B0=D0=B6=D1=91=D1=80=D0=B0 =
=D0=B2 =D0=9C=D0=BE=D1=81=D0=BA=D0=BE=D0=B2=D1=81=D0=BA=D0=B8=D0=B9 =D1=86=
=D0=B5=D0=BD=D1=82=D1=80 =D0=9A=D0=B0=D1=80=D0=BD=D0=B5=D0=B3=D0=B8, =D1=80=
=D0=B0=D0=B7=D1=80=D0=B0=D0=B1=D0=BE=D1=82=D1=87=D0=B8=D0=BA=D0=B0 =D0=B2 S=
aaS-=D1=81=D0=B5=D1=80=D0=B2=D0=B8=D1=81 =D0=94=D0=BE=D0=BC=D0=B8=D0=BB=D0=
=B5=D0=BD=D0=B4 =D0=B8 =D0=BC=D0=BD=D0=BE=D0=B3=D0=BE=D0=B5 =D0=B4=D1=80=D1=
=83=D0=B3=D0=BE=D0=B5.</span></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 10px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 10px; min-height:=
 10px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(169, 205, 242); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 26px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:center;""><span style=3D""font-size:18px;""><strong>=
=D0=9B=D0=B5=D0=BA=D1=86=D0=B8=D0=B8 =D0=B8 =D0=BA=D0=BE=D0=BD=D0=BA=D1=83=
=D1=80=D1=81=D1=8B =D0=B2 =D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D1=84=D0=BE=
=D1=80=D0=BC=D0=B0=D1=82=D0=B5</strong></span></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block image-block"" width=3D""100%"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heig=
ht: auto; border-collapse: collapse; border-spacing: 0px; display: inline-t=
able; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; padding: 0px; height: 100=
%;"" class=3D""block-wrapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 340px; width: 100%; table-layout: fixed; =
text-align: center; border-spacing: 0px; border-collapse: collapse; font-si=
ze: 0px;"">
<tbody>
<tr>
<td style=3D""width: auto; height: 100%; display: inline-table;"" class=3D""co=
ntent-wrapper"">
<table class=3D""content-box"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""=
0"" style=3D""display: inline-table; vertical-align: top; width: auto; height=
: 100%; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""vertical-align: top;"">
<div class=3D""image-wrapper image-drop""><a class=3D""image-link"" href=3D""htt=
ps://unimail.hse.ru/ru/mail_link_tracker?hash=3D685trqw48xromejnr33pasja89w=
17rz1ua7fpnpnbncejhw71pmu95zazo8yy7hyk3o4ctkzfufeeweyztq9354zmy19xro34zj6hp=
e5r5gw4qgwa48ay&url=3DaHR0cDovL3ZrLmNvbS9AaHNlX2NhcmVlci1kYWlkemhlc3Qta2FyZ=
XJueWgtc29ieXRpaQ~~&uid=3DMTMyMzY3NA=3D=3D"" target=3D""_blank""><img class=3D=
""image-element"" src=3D""http://unimail.hse.ru/ru/user_file?resource=3Dhimg&u=
ser_id=3D1323674&name=3D68wsf46nji8xs7piu3azzngxco3frpc6uggbsnbg7s55x97j94f=
ng61jb35bwkmxd1n8yna6c1s1dhq5wbj48ioedue5zhokrccaasby"" alt=3D""Some Image"" s=
tyle=3D""font-size: small; border: none; width: 100%; max-width: 600px; heig=
ht: auto; max-height: 335px; outline: none; text-decoration: none;"" id=3D""g=
ridster_block_302_main_img"" width=3D""600""></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix cke_editable cke_editable_inline cke_contents_ltr ck=
e_show_borders"" style=3D""overflow-wrap: break-word; position: relative;"" ta=
bindex=3D""0"" spellcheck=3D""false"" role=3D""textbox"" aria-label=3D""false"" ari=
a-describedby=3D""cke_86"">
<p><span style=3D""font-size:16px;""><span style=3D""line-height:1.5;"">=E2=80=
=A2 =D0=9D=D0=B0=D1=87=D0=B0=D0=BB=D0=B8=D1=81=D1=8C <a data-cke-saved-href=
=3D""http://plant.coca-colahellenic.ru/bookings/web"" href=3D""https://unimail=
.hse.ru/ru/mail_link_tracker?hash=3D6udjyztd7jt1hhjnr33pasja89w17rz1ua7fpnp=
nbncejhw71pmutherkgn7o6jprw6jzoc9g61aiweyztq9354zmy19xro34zj6hpe5r5gw4qgwa4=
8ay&url=3DaHR0cDovL3BsYW50LmNvY2EtY29sYWhlbGxlbmljLnJ1L2Jvb2tpbmdzL3dlYg~~&=
uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255);"">=D0=BE=D0=BD=D0=
=BB=D0=B0=D0=B9=D0=BD-=D1=8D=D0=BA=D1=81=D0=BA=D1=83=D1=80=D1=81=D0=B8=D0=
=B8 =D0=BD=D0=B0 =D0=B7=D0=B0=D0=B2=D0=BE=D0=B4 Coca-Cola HBC =D0=A0=D0=BE=
=D1=81=D1=81=D0=B8=D1=8F</a><br>
<br>
=D0=92=D1=82=D0=BE=D1=80=D0=BD=D0=B8=D0=BA, 2 =D0=B8=D1=8E=D0=BD=D1=8F:<br>
=E2=80=A2 =D0=92 16:00, <a data-cke-saved-href=3D""http://vk.com/wall-679680=
50_1484"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6uyiteu=
iub775kjnr33pasja89w17rz1ua7fpnpnbncejhw71pmut7nri64ogp9pdpa36om15iy5undowu=
by7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cDovL3ZrLmNvbS93YWxsLTY3OTY4M=
DUwXzE0ODQ~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255);"">=D0=
=9F=D1=80=D1=8F=D0=BC=D0=BE=D0=B9 =D1=8D=D1=84=D0=B8=D1=80 =D1=81 =D0=AE=D1=
=80=D0=B8=D0=B5=D0=BC =D0=9B=D0=B5=D0=B2=D0=B8=D1=82=D0=B0=D1=81=D0=BE=D0=
=BC</a>, =D1=80=D0=B5=D1=81=D1=82=D0=BE=D1=80=D0=B0=D1=82=D0=BE=D1=80=D0=BE=
=D0=BC =D0=B8 =D0=B3=D0=B5=D0=BD=D0=B4=D0=B8=D1=80=D0=B5=D0=BA=D1=82=D0=BE=
=D1=80=D0=BE=D0=BC Black Star Burger<br>
=E2=80=A2 =D0=92 19:00, =D0=B2=D0=B5=D0=B1=D0=B8=D0=BD=D0=B0=D1=80 Changell=
enge =C2=AB<a data-cke-saved-href=3D""http://clck.ru/NhT9E"" href=3D""https://=
unimail.hse.ru/ru/mail_link_tracker?hash=3D6sa9gcsb7wopfqjnr33pasja89w17rz1=
ua7fpnpnbncejhw71pmuatdsxutkigkq99xyjjawa5tw8adowuby7eb1cf5ge75tbx3h5j71syw=
opdfiu6auo&url=3DaHR0cDovL2NsY2sucnUvTmhUOUU~&uid=3DMTMyMzY3NA=3D=3D"" style=
=3D""color: rgb(0, 127, 255);"">=D0=AD=D0=BA=D1=81=D0=B5=D0=BB=D1=8C, =D1=84=
=D0=B8=D0=BD=D0=B0=D0=BD=D1=81=D1=8B =D0=B8 =D0=BF=D1=80=D0=B5=D0=B7=D0=B5=
=D0=BD=D1=82=D0=B0=D1=86=D0=B8=D0=B8</a>=C2=BB<br>
<br>
=D0=A1=D1=80=D0=B5=D0=B4=D0=B0, 3 =D0=B8=D1=8E=D0=BD=D1=8F:<br>
=E2=80=A2 =D0=A1=D1=82=D0=B0=D1=80=D1=82=D1=83=D0=B5=D1=82 <a data-cke-save=
d-href=3D""http://onlinelectures.tb.ru"" href=3D""https://unimail.hse.ru/ru/ma=
il_link_tracker?hash=3D6isxiu8zsm8yqcjnr33pasja89w17rz1ua7fpnpnbncejhw71pmu=
uz3dhpu3wtr7ucp875ra1j4wsyeyztq9354zmy19xro34zj6hpe5r5gw4qgwa48ay&url=3DaHR=
0cDovL29ubGluZWxlY3R1cmVzLnRiLnJ1&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: r=
gb(0, 127, 255);"">=D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D0=BB=D0=B5=D0=BA=
=D1=82=D0=BE=D1=80=D0=B8=D0=B9 =D0=A2=D0=B8=D0=BD=D1=8C=D0=BA=D0=BE=D1=84=
=D1=84</a>. =D0=A2=D0=B5=D0=BC=D1=8B: =D0=98=D0=BD=D1=84=D0=BE=D1=80=D0=BC=
=D0=B0=D1=86=D0=B8=D0=BE=D0=BD=D0=BD=D0=B0=D1=8F =D0=B1=D0=B5=D0=B7=D0=BE=
=D0=BF=D0=B0=D1=81=D0=BD=D0=BE=D1=81=D1=82=D1=8C, =D0=A1=D0=B8=D1=81=D1=82=
=D0=B5=D0=BC=D0=BD=D1=8B=D0=B9 =D0=B0=D0=BD=D0=B0=D0=BB=D0=B8=D0=B7, =D0=94=
=D0=B8=D0=B7=D0=B0=D0=B9=D0=BD, =D0=9F=D1=80=D0=BE=D0=B4=D1=83=D0=BA=D1=82=
=D0=BE=D0=B2=D0=B0=D1=8F =D0=B0=D0=BD=D0=B0=D0=BB=D0=B8=D1=82=D0=B8=D0=BA=
=D0=B0<br>
=E2=80=A2 =D0=92 19:00, <a data-cke-saved-href=3D""http://nes.timepad.ru/eve=
nt/1319453"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D61us=
ujpikoj9yajnr33pasja89w17rz1ua7fpnpnbncejhw71pmuhn6cqcym3sz5ihezconndj84a6e=
yztq9354zmy19xro34zj6hpe5r5gw4qgwa48ay&url=3DaHR0cDovL25lcy50aW1lcGFkLnJ1L2=
V2ZW50LzEzMTk0NTM~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255)=
;"">=D0=A1=D1=82=D1=80=D0=B0=D1=82=D0=B5=D0=B3=D0=B8=D1=87=D0=B5=D1=81=D0=BA=
=D0=B8=D0=B9 =D0=BA=D0=BE=D0=BD=D1=81=D0=B0=D0=BB=D1=82=D0=B8=D0=BD=D0=B3 B=
ain&amp;Company</a>: =D0=BA=D0=B5=D0=B9=D1=81=D1=8B =D1=81=D1=82=D1=83=D0=
=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=B2 =D0=B8 =D0=B2=D1=8B=D0=BF=D1=83=D1=81=D0=
=BA=D0=BD=D0=B8=D0=BA=D0=BE=D0=B2 =D0=A0=D0=AD=D0=A8<br>
=E2=80=A2 =D0=92 19:00, Webinar with Kearney =C2=AB<a data-cke-saved-href=
=3D""http://docs.google.com/forms/d/e/1FAIpQLSeCxuPcKyMRP0kTRFUnEulJiH-zSPqW=
WouKhbPDg-qKKvC4Ow/viewform"" href=3D""https://unimail.hse.ru/ru/mail_link_tr=
acker?hash=3D6rae85zz3z6ys6jnr33pasja89w17rz1ua7fpnpnbncejhw71pmu4hzabxgxfy=
tw8h684qnskt16rodowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cDovL2RvY=
3MuZ29vZ2xlLmNvbS9mb3Jtcy9kL2UvMUZBSXBRTFNlQ3h1UGNLeU1SUDBrVFJGVW5FdWxKaUgt=
elNQcVdXb3VLaGJQRGctcUtLdkM0T3cvdmlld2Zvcm0~&uid=3DMTMyMzY3NA=3D=3D"" style=
=3D""color: rgb(0, 127, 255);"">Telecom industry in the time of COVID and aft=
er</a>=C2=BB<br>
<br>
=D0=A7=D0=B5=D1=82=D0=B2=D0=B5=D1=80=D0=B3, 4 =D0=B8=D1=8E=D0=BD=D1=8F:<br>
=E2=80=A2 =D0=92 16:00, <a data-cke-saved-href=3D""http://anketolog.ru/be_ef=
fective"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D69g5w1b=
uoiur1ojnr33pasja89w17rz1ua7fpnpnbncejhw71pmuhe46m4conszw9hwdpro4e9b7w9cfzq=
rh53f9cfwknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cDovL2Fua2V0b2xvZy5ydS9iZV9lZ=
mZlY3RpdmU~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255);"">=D0=
=A2=D1=80=D0=B5=D0=BD=D0=B8=D0=BD=D0=B3 =D0=9B=D0=B8=D0=B3=D0=B8 =D0=9A=D0=
=9F=D0=9C=D0=93. Be effective</a>: =D0=B8=D0=BD=D1=81=D1=82=D1=80=D1=83=D0=
=BC=D0=B5=D0=BD=D1=82=D1=8B, =D0=BF=D0=BE=D0=B4=D1=85=D0=BE=D0=B4=D1=8B =D0=
=B8 =D0=BB=D0=B0=D0=B9=D1=84=D1=85=D0=B0=D0=BA=D0=B8<br>
=E2=80=A2 =D0=92 18:00, <a data-cke-saved-href=3D""http://go.mywebinar.com/p=
jcd-pzbe-epht-pktx"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?has=
h=3D6jjicr9ek15t36jnr33pasja89w17rz1ua7fpnpnbncejhw71pmuiq84xt7gh85gkk5eodd=
4dgbsytuxfk9zpdzjsnaknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cDovL2dvLm15d2ViaW=
5hci5jb20vcGpjZC1wemJlLWVwaHQtcGt0eA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""col=
or: rgb(0, 127, 255);"">=D1=82=D1=80=D0=B5=D0=BD=D0=B8=D0=BD=D0=B3 BCG, =D0=
=BF=D0=BE=D1=81=D0=B2=D1=8F=D1=89=D1=91=D0=BD=D0=BD=D1=8B=D0=B9 =D1=81=D0=
=BE=D0=B7=D0=B4=D0=B0=D0=BD=D0=B8=D1=8E =D0=B1=D0=B8=D0=B7=D0=BD=D0=B5=D1=
=81-=D0=BF=D1=80=D0=B5=D0=B7=D0=B5=D0=BD=D1=82=D0=B0=D1=86=D0=B8=D0=B9</a><=
br>
=E2=80=A2 =D0=92 19:00, <a data-cke-saved-href=3D""http://clck.ru/NegQh"" hre=
f=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6cdrwa7nmgcswojnr33=
pasja89w17rz1ua7fpnpnbncejhw71pmuuejozwxx79aj8r1rje5dgd1tjb9yzhx5t8ngiyhknt=
3a4jmx9fqz66eftcjthdgco&url=3DaHR0cDovL2NsY2sucnUvTmVnUWg~&uid=3DMTMyMzY3NA=
=3D=3D"" style=3D""color: rgb(0, 127, 255);"">=D1=82=D1=80=D0=B0=D0=BD=D1=81=
=D0=BB=D1=8F=D1=86=D0=B8=D1=8F, =D0=BA=D0=BE=D1=82=D0=BE=D1=80=D0=B0=D1=8F =
=D0=BF=D0=BE=D0=BC=D0=BE=D0=B6=D0=B5=D1=82 =D0=BE=D0=BF=D1=80=D0=B5=D0=B4=
=D0=B5=D0=BB=D0=B8=D1=82=D1=8C=D1=81=D1=8F =D1=81 =D0=BA=D0=B0=D1=80=D1=8C=
=D0=B5=D1=80=D0=BD=D1=8B=D0=BC =D1=82=D1=80=D0=B5=D0=BA=D0=BE=D0=BC</a>. =
=D0=A1=D0=BF=D0=B8=D0=BA=D0=B5=D1=80: =D0=A1=D0=B5=D1=80=D0=B3=D0=B5=D0=B9 =
=D0=A2=D0=B0=D0=BB=D0=B0=D0=BB=D0=B0=D0=B5=D0=B2, 11 =D0=BB=D0=B5=D1=82 =D1=
=80=D0=B0=D0=B1=D0=BE=D1=82=D0=B0=D0=BB =D0=B2 Citi, =D0=A0=D0=BE=D1=81=D0=
=B1=D0=B0=D0=BD=D0=BA=D0=B5, LVMH&nbsp;<br>
=E2=80=A2 =D0=92 19:00, <a data-cke-saved-href=3D""http://ru.surveymonkey.co=
m/r/GF7FG8V"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6pc=
9te6hbsn15kjnr33pasja89w17rz1ua7fpnpnbncejhw71pmuia8j974hcj3cy3mccmfopew414=
nsnyzrngg11eoknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cDovL3J1LnN1cnZleW1vbmtle=
S5jb20vci9HRjdGRzhW&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255=
);"">=D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D0=B2=D1=81=D1=82=D1=80=D0=B5=D1=
=87=D0=B0 =D1=81 =C2=AB=D0=B2=D1=8B=D0=BF=D1=83=D1=81=D0=BA=D0=BD=D0=B8=D0=
=BA=D0=BE=D0=BC=C2=BB McKinsey</a>, =D0=BE=D0=BF=D0=B5=D1=80=D0=B0=D1=86=D0=
=B8=D0=BE=D0=BD=D0=BD=D1=8B=D0=BC =D0=B4=D0=B8=D1=80=D0=B5=D0=BA=D1=82=D0=
=BE=D1=80=D0=BE=D0=BC =D0=90=D0=BB=D1=8C=D1=84=D0=B0-=D0=91=D0=B0=D0=BD=D0=
=BA=D0=B0 =D0=94=D0=BC=D0=B8=D1=82=D1=80=D0=B8=D0=B5=D0=BC =D0=92=D0=B8=D1=
=82=D0=BC=D0=B0=D0=BD=D0=BE=D0=BC<br>
<br>
=E2=80=A2 =D0=94=D0=BE 8 =D0=B8=D1=8E=D0=BD=D1=8F, <a data-cke-saved-href=
=3D""https://mck.co/3bkhQaE"" href=3D""https://unimail.hse.ru/ru/mail_link_tra=
cker?hash=3D6wpcurgcxw7ouwjnr33pasja89w17rz1ua7fpnpnbncejhw71pmuwdnqxkqz8w5=
8qt6jytx9tefmfe3twcgsaro76uhknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9tY2=
suY28vM2JraFFhRQ~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=
McKinsey Women Achievement Award</a>&nbsp;=D0=BE=D1=82 =D1=81=D1=82=D1=83=
=D0=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=BA =D0=BB=D1=8E=D0=B1=D1=8B=D1=85 =D1=81=
=D0=BF=D0=B5=D1=86=D0=B8=D0=B0=D0=BB=D1=8C=D0=BD=D0=BE=D1=81=D1=82=D0=B5=D0=
=B9 =D0=B8 =D0=B2=D1=8B=D0=BF=D1=83=D1=81=D0=BA=D0=BD=D0=B8=D1=86 =D1=81 =
=D0=BE=D0=BF=D1=8B=D1=82=D0=BE=D0=BC =D1=80=D0=B0=D0=B1=D0=BE=D1=82=D1=8B =
=D0=BC=D0=B5=D0=BD=D0=B5=D0=B5 5 =D0=BB=D0=B5=D1=82.<br>
=E2=80=A2 =D0=94=D0=BE 8 =D0=B8 =D0=B4=D0=BE 15 =D0=B8=D1=8E=D0=BD=D1=8F,&n=
bsp;<a data-cke-saved-href=3D""http://ancorstart.tilda.ws/"" href=3D""https://=
unimail.hse.ru/ru/mail_link_tracker?hash=3D6jdy9fbpm7c7gkjnr33pasja89w17rz1=
ua7fpnpnbncejhw71pmuid1spi8pr55nb5bmfnpxyz6yj9y6gt5f1gmfwtyknt3a4jmx9fqz66e=
ftcjthdgco&url=3DaHR0cDovL2FuY29yc3RhcnQudGlsZGEud3Mv&uid=3DMTMyMzY3NA=3D=
=3D"" style=3D""color: rgb(0, 127, 255);"">=D0=90=D0=9D=D0=9A=D0=9E=D0=A0 =D0=
=BF=D1=80=D0=B8=D0=B3=D0=BB=D0=B0=D1=88=D0=B0=D0=B5=D1=82 =D0=BD=D0=B0 =D0=
=9B=D0=B5=D1=82=D0=BD=D0=B8=D0=B5 digital-=D1=88=D0=BA=D0=BE=D0=BB=D1=8B</a=
>: =D0=A8=D0=BA=D0=BE=D0=BB=D0=B0 =D0=BA=D0=BE=D0=BE=D1=80=D0=B4=D0=B8=D0=
=BD=D0=B0=D1=82=D0=BE=D1=80=D0=B0 =D0=B8 =D0=A8=D0=BA=D0=BE=D0=BB=D0=B0 =D1=
=80=D0=B5=D0=BA=D1=80=D1=83=D1=82=D0=B5=D1=80=D0=B0 =D1=81=D0=BE=D0=BE=D1=
=82=D0=B2=D0=B5=D1=82=D1=81=D0=B2=D0=B5=D0=BD=D0=BD=D0=BE.<br>
=E2=80=A2 8 =D0=B8=D1=8E=D0=BD=D1=8F,&nbsp;<a data-cke-saved-href=3D""https:=
//sbergrad.timepad.ru/event/1315213/"" href=3D""https://unimail.hse.ru/ru/mai=
l_link_tracker?hash=3D6esiyfqr7mx1okjnr33pasja89w17rz1ua7fpnpnbncejhw71pmui=
8dhhb3ynz78a4br55sqf4hudicfzqrh53f9cfwknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0=
cHM6Ly9zYmVyZ3JhZC50aW1lcGFkLnJ1L2V2ZW50LzEzMTUyMTMv&uid=3DMTMyMzY3NA=3D=3D=
"" style=3D""color: rgb(0, 127, 255);"">=D0=92=D0=B5=D0=B1=D0=B8=D0=BD=D0=B0=
=D1=80 ""=D0=A0=D0=B0=D0=B1=D0=BE=D1=82=D0=B0 =D0=B2 Agile-=D0=BA=D0=BE=D0=
=BC=D0=B0=D0=BD=D0=B4=D0=B0=D1=85""</a>&nbsp;=D0=BE=D1=82 =D0=A1=D0=B1=D0=B5=
=D1=80=D0=B1=D0=B0=D0=BD=D0=BA=D0=B0<br>
=E2=80=A2 10 =D0=B8=D1=8E=D0=BD=D1=8F,&nbsp;<a data-cke-saved-href=3D""https=
://go.mywebinar.com/whbc-zxhp-zwkg-wghe"" href=3D""https://unimail.hse.ru/ru/=
mail_link_tracker?hash=3D6zok3y4zhhmsxojnr33pasja89w17rz1ua7fpnpnbncejhw71p=
mu1g4m8w1ocat4k33a7tysjx3jy43twcgsaro76uhknt3a4jmx9fqz66eftcjthdgco&url=3Da=
HR0cHM6Ly9nby5teXdlYmluYXIuY29tL3doYmMtenhocC16d2tnLXdnaGU~&uid=3DMTMyMzY3N=
A=3D=3D"" style=3D""color: rgb(0, 127, 255);"">=D0=BB=D0=B5=D0=BA=D1=86=D0=B8=
=D1=8F BCG =D0=BE =D0=BA=D0=BE=D0=BD=D1=81=D0=B0=D0=BB=D1=82=D0=B8=D0=BD=D0=
=B3=D0=B5 =D0=B2 =D0=BF=D1=80=D0=B0=D0=BA=D1=82=D0=B8=D0=BA=D0=B5 =D1=82=D0=
=B5=D0=BB=D0=B5=D0=BA=D0=BE=D0=BC=D0=BC=D1=83=D0=BD=D0=B8=D0=BA=D0=B0=D1=86=
=D0=B8=D0=B8, =D0=BC=D0=B5=D0=B4=D0=B8=D0=B0, =D1=82=D0=B5=D1=85=D0=BD=D0=
=BE=D0=BB=D0=BE=D0=B3=D0=B8=D0=B8</a><br>
=E2=80=A2 15 =D0=B8=D1=8E=D0=BD=D1=8F, <a data-cke-saved-href=3D""http://tin=
yurl.com/qw4sabv"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=
=3D61atwy1raxxxnsjnr33pasja89w17rz1ua7fpnpnbncejhw71pmuw1faosktqck6zrxmbap5=
stj7acgftcrc1foxceyknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cDovL3Rpbnl1cmwuY29=
tL3F3NHNhYnY~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255);"">=
=D0=B2=D0=B5=D0=B1=D0=B8=D0=BD=D0=B0=D1=80-""=D0=B4=D0=B5=D0=BD=D1=8C =D0=BE=
=D1=82=D0=BA=D1=80=D1=8B=D1=82=D1=8B=D1=85 =D0=B4=D0=B2=D0=B5=D1=80=D0=B5=
=D0=B9""=D0=B2 IQVIA</a><br>
=E2=80=A2 =D0=94=D0=BE 17 =D0=B8=D1=8E=D0=BD=D1=8F,&nbsp;=D0=B7=D0=B0=D0=BF=
=D0=B8=D1=81=D1=8C =D0=BD=D0=B0&nbsp;<a data-cke-saved-href=3D""https://clck=
.ru/Neh7P"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D69epx=
och3mz69sjnr33pasja89w17rz1ua7fpnpnbncejhw71pmusrmow8c6fti37i98o81a7cf8wmux=
fk9zpdzjsnaknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9jbGNrLnJ1L05laDdQ&ui=
d=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255);"">=D0=9A=D0=B0=D1=80=
=D1=8C=D0=B5=D1=80=D0=BD=D1=8B=D0=B9 =D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=
=D1=81=D0=BF=D1=80=D0=B8=D0=BD=D1=82 Total Modern Trade</a><br>
=E2=80=A2 =D0=94=D0=BE 21 =D0=B8=D1=8E=D0=BD=D1=8F, =D0=BD=D0=B0=D0=B1=D0=
=BE=D1=80 =D0=BD=D0=B0&nbsp;<a data-cke-saved-href=3D""https://pwcrussia.ru/=
surveys/index.php/164316?newtest=3DY&amp;lang=3Dru"" href=3D""https://unimail=
.hse.ru/ru/mail_link_tracker?hash=3D6dqoocafko18ssjnr33pasja89w17rz1ua7fpnp=
nbncejhw71pmu73bar4iugn1otuhitbzx999uty3twcgsaro76uhknt3a4jmx9fqz66eftcjthd=
gco&url=3DaHR0cHM6Ly9wd2NydXNzaWEucnUvc3VydmV5cy9pbmRleC5waHAvMTY0MzE2P25ld=
3Rlc3Q9WSZsYW5nPXJ1&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255=
);"">=D0=B5=D0=B6=D0=B5=D0=B3=D0=BE=D0=B4=D0=BD=D1=83=D1=8E =D0=BF=D1=80=D0=
=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC=D1=83 PwC Audit Camp</a><br>
=E2=80=A2 20-21 =D0=B8=D1=8E=D0=BD=D1=8F,&nbsp;<a data-cke-saved-href=3D""ht=
tps://vk.com/create_2day?w=3Dwall-195554451_43"" href=3D""https://unimail.hse=
.ru/ru/mail_link_tracker?hash=3D63exbbi9fojeg4jnr33pasja89w17rz1ua7fpnpnbnc=
ejhw71pmuwcok1nwjb3sd3i6zzp5oibxm6ydowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&=
url=3DaHR0cHM6Ly92ay5jb20vY3JlYXRlXzJkYXk_dz13YWxsLTE5NTU1NDQ1MV80Mw~~&uid=
=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255);"">CREATE 2DAY</a>&nbs=
p;- =D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD =D1=84=D0=BE=D1=80=D1=83=D0=BC =D0=
=BF=D1=80=D0=BE=D1=84=D0=B5=D1=81=D1=81=D0=B8=D0=B9 =D0=B1=D1=83=D0=B4=D1=
=83=D1=89=D0=B5=D0=B3=D0=BE, =D0=BA=D0=BE=D1=82=D0=BE=D1=80=D1=8B=D0=B9 =D0=
=BF=D0=BE=D0=BC=D0=BE=D0=B6=D0=B5=D1=82 =D1=82=D0=B5=D0=B1=D0=B5 =D1=80=D0=
=B0=D0=B7=D0=BE=D0=B1=D1=80=D0=B0=D1=82=D1=8C=D1=81=D1=8F =D1=81 =D0=BC=D0=
=B8=D1=80=D0=BE=D0=B2=D1=8B=D0=BC=D0=B8 =D1=82=D1=80=D0=B5=D0=BD=D0=B4=D0=
=B0=D0=BC=D0=B8 =D0=B2 =D0=BF=D1=80=D0=BE=D1=84=D0=B5=D1=81=D1=81=D0=B8=D0=
=BE=D0=BD=D0=B0=D0=BB=D1=8C=D0=BD=D0=BE=D0=B9 =D1=81=D1=84=D0=B5=D1=80=D0=
=B5 =D0=B8 =D1=83=D0=B7=D0=BD=D0=B0=D1=82=D1=8C =D0=BF=D0=BE=D0=B4=D1=80=D0=
=BE=D0=B1=D0=BD=D0=B5=D0=B5 =D0=BE=D0=B1 =D0=B0=D0=BA=D1=82=D1=83=D0=B0=D0=
=BB=D1=8C=D0=BD=D1=8B=D1=85 =D0=BF=D1=80=D0=BE=D1=84=D0=B5=D1=81=D1=81=D0=
=B8=D1=8F=D1=85 =D0=B1=D1=83=D0=B4=D1=83=D1=89=D0=B5=D0=B3=D0=BE.&nbsp;<br>
=E2=80=A2 23 =D0=B8=D1=8E=D0=BD=D1=8F,&nbsp;<a data-cke-saved-href=3D""https=
://anketolog.ru/kpmg_leaguetalks"" href=3D""https://unimail.hse.ru/ru/mail_li=
nk_tracker?hash=3D6f1pdjhzz3bfxqjnr33pasja89w17rz1ua7fpnpnbncejhw71pmu9ftyc=
apu7n78ii4gc1jin1ifkmy6gt5f1gmfwtyknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6=
Ly9hbmtldG9sb2cucnUva3BtZ19sZWFndWV0YWxrcw~~&uid=3DMTMyMzY3NA=3D=3D"" style=
=3D""color: rgb(0, 127, 255);"">KPMG League talks</a>&nbsp;=E2=80=94 5 =D0=B2=
=D1=8B=D1=81=D1=82=D1=83=D0=BF=D0=BB=D0=B5=D0=BD=D0=B8=D0=B9 =D0=BF=D0=BE =
=D1=80=D0=B0=D0=B7=D0=BD=D1=8B=D0=BC =D1=82=D0=B5=D0=BC=D0=B0=D0=BC soft sk=
ills.&nbsp;</span></span><br></p>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 10px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 10px; min-height:=
 10px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(169, 205, 242); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 26px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:center;""><span style=3D""font-size:18px;""><strong>=
=D0=9F=D0=BE=D0=BB=D0=B5=D0=B7=D0=BD=D1=8B=D0=B5 =D0=BC=D0=B0=D1=82=D0=B5=
=D1=80=D0=B8=D0=B0=D0=BB=D1=8B</strong></span></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix cke_editable cke_editable_inline cke_contents_ltr ck=
e_show_borders"" style=3D""overflow-wrap: break-word; position: relative;"" ta=
bindex=3D""0"" spellcheck=3D""false"" role=3D""textbox"" aria-label=3D""false"" ari=
a-describedby=3D""cke_129"">
<ul>
<li><a data-cke-saved-href=3D""http://vk.com/wall-278573_22173"" href=3D""http=
s://unimail.hse.ru/ru/mail_link_tracker?hash=3D65b4wim4mx6br6jnr33pasja89w1=
7rz1ua7fpnpnbncejhw71pmuwkzxs6qbiaxj5knwcmr6mbo6tr5bxs7u9pcem3wknt3a4jmx9fq=
z66eftcjthdgco&url=3DaHR0cDovL3ZrLmNvbS93YWxsLTI3ODU3M18yMjE3Mw~~&uid=3DMTM=
yMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255);""><span style=3D""line-heigh=
t:1.5""><span style=3D""font-size:16px"">=D0=9A=D0=B0=D0=BA =D1=80=D0=B0=D0=B1=
=D0=BE=D1=82=D0=B0=D1=82=D1=8C =D0=B4=D0=BE=D0=BC=D0=B0: 40 =D1=81=D0=BE=D0=
=B2=D0=B5=D1=82=D0=BE=D0=B2 =D0=B2 =D0=B8=D0=BD=D1=84=D0=BE=D0=B3=D1=80=D0=
=B0=D1=84=D0=B8=D0=BA=D0=B5</span></span></a></li>
<li><a data-cke-saved-href=3D""http://vk.com/@hse_career-sovety-vsem-kto-nas=
hel-novuu-udalennuu-rabotu"" href=3D""https://unimail.hse.ru/ru/mail_link_tra=
cker?hash=3D6uzosohjs7pjc4jnr33pasja89w17rz1ua7fpnpnbncejhw71pmuwwqq8rqut79=
hhmu89rdw18fb4g5bxs7u9pcem3wknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cDovL3ZrLm=
NvbS9AaHNlX2NhcmVlci1zb3ZldHktdnNlbS1rdG8tbmFzaGVsLW5vdnV1LXVkYWxlbm51dS1yY=
WJvdHU~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 255);""><span st=
yle=3D""line-height:1.5""><span style=3D""font-size:16px"">5 =D1=81=D0=BE=D0=B2=
=D0=B5=D1=82=D0=BE=D0=B2 =D0=B4=D0=BB=D1=8F =D1=82=D0=B5=D1=85, =D0=BA=D1=
=82=D0=BE =D0=B2=D1=8B=D1=85=D0=BE=D0=B4=D0=B8=D1=82 =D0=BD=D0=B0 =D0=BD=D0=
=BE=D0=B2=D1=83=D1=8E =D1=80=D0=B0=D0=B1=D0=BE=D1=82=D1=83 =D0=B2 =D0=BA=D0=
=B0=D1=80=D0=B0=D0=BD=D1=82=D0=B8=D0=BD</span></span></a><br></li>
<li><a data-cke-saved-href=3D""http://vk.com/@hse_career-kakie-privychki-nuz=
hno-sohranit-na-udalennoi-rabote"" href=3D""https://unimail.hse.ru/ru/mail_li=
nk_tracker?hash=3D6j5k1ypg4fuszrjnr33pasja89w17rz1ua7fpnpnbncejhw71pmuzzg9d=
ree6fzaxqwnse8nmpai3m6ardjf311q9raknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cDov=
L3ZrLmNvbS9AaHNlX2NhcmVlci1rYWtpZS1wcml2eWNoa2ktbnV6aG5vLXNvaHJhbml0LW5hLXV=
kYWxlbm5vaS1yYWJvdGU~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0, 127, 2=
55);""><span style=3D""line-height:1.5""><span style=3D""font-size:16px"">=D0=9A=
=D0=B0=D0=BA=D0=B8=D0=B5 =D0=BF=D1=80=D0=B8=D0=B2=D1=8B=D1=87=D0=BA=D0=B8 =
=D0=BD=D1=83=D0=B6=D0=BD=D0=BE =D1=81=D0=BE=D1=85=D1=80=D0=B0=D0=BD=D0=B8=
=D1=82=D1=8C =D0=BD=D0=B0 =D1=83=D0=B4=D0=B0=D0=BB=D1=91=D0=BD=D0=BA=D0=B5<=
/span></span></a></li>
</ul>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 10px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 10px; min-height:=
 10px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(20=
4, 204, 204); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix cke_editable cke_editable_inline cke_contents_ltr ck=
e_show_borders"" style=3D""overflow-wrap: break-word; position: relative;"" ta=
bindex=3D""0"" spellcheck=3D""false"" role=3D""textbox"" aria-label=3D""false"" ari=
a-describedby=3D""cke_427""><span style=3D""font-size:16px""><span style=3D""lin=
e-height:1.5""><span style=3D""line-height:1.5""><span style=3D""font-size:16px=
""><span style=3D""font-size:16px""><span style=3D""line-height:1.5"">=D0=9D=D0=
=B0=D0=BF=D0=BE=D0=BC=D0=B8=D0=BD=D0=B0=D0=B5=D0=BC, =D1=87=D1=82=D0=BE =D0=
=BC=D0=BE=D0=B6=D0=BD=D0=BE =D0=B7=D0=B0=D0=BF=D0=B8=D1=81=D0=B0=D1=82=D1=
=8C=D1=81=D1=8F =D0=BD=D0=B0&nbsp;<a data-cke-saved-href=3D""http://career.h=
se.ru/consult_center"" href=3D""https://unimail.hse.ru/ru/mail_link_tracker?h=
ash=3D66kbkpf841wwh6jnr33pasja89w17rz1ua7fpnpnbncejhw71pmuwxfa78r8wbfx5hcir=
41f5hujsogftcrc1foxceyknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cDovL2NhcmVlci5o=
c2UucnUvY29uc3VsdF9jZW50ZXI~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color: rgb(0,=
 127, 255);"">=D0=B8=D0=BD=D0=B4=D0=B8=D0=B2=D0=B8=D0=B4=D1=83=D0=B0=D0=BB=
=D1=8C=D0=BD=D1=83=D1=8E =D0=BA=D0=BE=D0=BD=D1=81=D1=83=D0=BB=D1=8C=D1=82=
=D0=B0=D1=86=D0=B8=D1=8E</a><strong>&nbsp;</strong>=E2=80=94 =D0=B5=D1=81=
=D0=BB=D0=B8 =D0=B5=D1=81=D1=82=D1=8C =D0=B2=D0=BE=D0=BF=D1=80=D0=BE=D1=81=
=D1=8B =D0=BF=D0=BE =D0=B8=D0=BD=D1=82=D0=B5=D1=80=D0=B2=D1=8C=D1=8E, cover=
 letter =D0=B8=D0=BB=D0=B8 =D0=BA=D0=B0=D1=80=D1=8C=D0=B5=D1=80=D0=BD=D0=BE=
=D0=BC=D1=83 =D1=82=D1=80=D0=B5=D0=BA=D1=83.<br></span></span></span></span=
><br>
=D0=98 =D0=BF=D0=BE=D0=B4=D0=BF=D0=B8=D1=81=D1=8B=D0=B2=D0=B0=D0=B9=D1=82=
=D0=B5=D1=81=D1=8C =D0=BD=D0=B0 =D0=BD=D0=B0=D1=88=D1=83&nbsp;<a data-cke-s=
aved-href=3D""http://vk.com/hse_career?w=3Dapp5748831_-278573"" href=3D""https=
://unimail.hse.ru/ru/mail_link_tracker?hash=3D66zedoctypho9sjnr33pasja89w17=
rz1ua7fpnpnbncejhw71pmu66cdogfmtf6xdhijrrd84qks6duxfk9zpdzjsnaknt3a4jmx9fqz=
66eftcjthdgco&url=3DaHR0cDovL3ZrLmNvbS9oc2VfY2FyZWVyP3c9YXBwNTc0ODgzMV8tMjc=
4NTcz&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=BA=D0=B0=
=D1=80=D1=8C=D0=B5=D1=80=D0=BD=D1=83=D1=8E =D1=80=D0=B0=D1=81=D1=81=D1=8B=
=D0=BB=D0=BA=D1=83 =D0=B2=D0=BA=D0=BE=D0=BD=D1=82=D0=B0=D0=BA=D1=82=D0=B5</=
a>.&nbsp;=D0=9C=D0=BE=D0=B6=D0=BD=D0=BE =D0=B1=D1=83=D0=B4=D0=B5=D1=82 =D0=
=BF=D0=BE=D0=BB=D1=83=D1=87=D0=B0=D1=82=D1=8C =D0=B2 =D0=BB=D0=B8=D1=87=D0=
=BA=D1=83 =D1=82=D0=BE, =D1=87=D1=82=D0=BE =D0=B2=D0=B0=D0=BC =D0=B8=D0=BD=
=D1=82=D0=B5=D1=80=D0=B5=D1=81=D0=BD=D0=B5=D0=B5 - =D0=B2=D0=B0=D0=BA=D0=B0=
=D0=BD=D1=81=D0=B8=D0=B8, =D1=81=D0=BE=D0=B1=D1=8B=D1=82=D0=B8=D1=8F =D0=B8=
=D0=BB=D0=B8 =D1=81=D1=82=D0=B0=D1=82=D1=8C=D0=B8.</span></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 38px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div style=3D""text-align:center;""><span style=3D""font-size:16px;"">=D0=92=D0=
=B0=D1=88 =D0=A6=D0=B5=D0=BD=D1=82=D1=80 =D1=80=D0=B0=D0=B7=D0=B2=D0=B8=D1=
=82=D0=B8=D1=8F =D0=BA=D0=B0=D1=80=D1=8C=D0=B5=D1=80=D1=8B</span></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block container-block"" width=3D""100%"" border=3D""0"" cell=
spacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; =
height: auto; border-collapse: collapse; background-color: rgb(86, 170, 255=
); background-image: none; border-spacing: 0px;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px; border: none;"" class=3D""block-wrap=
per"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width:100%;"" class=3D""content-wrapper"">
<table class=3D""container-table content-box"" border=3D""0"" cellspacing=3D""0""=
 cellpadding=3D""0"" style=3D""width: 100%; height: 100%; border-spacing: 0px;=
 border-collapse: collapse;"" id=3D""JColResizer0"">
<tbody>
<tr class=3D""container-row"">
<td class=3D""td-wrapper"" style=3D""font-size:0;vertical-align:top;"" align=3D=
""center""><!--[if (gte mso 9)|(IE)]><table width=3D""100%"" cellpadding=3D""0"" =
cellspacing=3D""0"" border=3D""0""><tr><td width=3D""319px"" valign=3D""top""><![en=
dif]-->
<table class=3D""column"" cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" wi=
dth=3D""100%"" style=3D""height: auto; vertical-align: top; display: inline-ta=
ble; max-width: 319px; border-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""sortable-container ui-sortable"" style=3D""vertical-align: top; =
width: 100%;"" align=3D""center"" valign=3D""top"">
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 73px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<p><span style=3D""line-height:1.5;""><strong>=D0=9D=D0=B0=D1=88=D0=B8 =D0=BA=
=D0=BE=D0=BD=D1=82=D0=B0=D0=BA=D1=82=D1=8B:&nbsp;</strong>career@hse.ru,&nb=
sp;+7(495) 772-95-90 =D0=B4=D0=BE=D0=B1. 27710, 27711<br>
=D0=9C=D0=BE=D1=81=D0=BA=D0=B2=D0=B0, =D0=9F=D0=BE=D0=BA=D1=80=D0=BE=D0=B2=
=D1=81=D0=BA=D0=B8=D0=B9 =D0=B1=D1=83=D0=BB=D1=8C=D0=B2=D0=B0=D1=80 11, L10=
2</span></p>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td><td width=3D""261px"" valign=3D""top""><![endif]=
-->
<table class=3D""column"" cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" wi=
dth=3D""100%"" style=3D""height: auto; vertical-align: top; display: inline-ta=
ble; max-width: 261px; border-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""sortable-container ui-sortable"" style=3D""vertical-align: top; =
width: 100%;"" align=3D""center"" valign=3D""top"">
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 73px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px 5px 20px; vertical-align: top; =
font-size: 14px; font-family: Arial, Helvetica, sans-serif; line-height: 16=
.8px; color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<div><span style=3D""line-height:1.5;""><strong>=D0=A1=D0=BE=D0=BE=D0=B1=D1=
=89=D0=B5=D1=81=D1=82=D0=B2=D0=B0 HSE Career:&nbsp;</strong></span></div>
<div><span style=3D""line-height:1.5;""><a href=3D""https://unimail.hse.ru/ru/=
mail_link_tracker?hash=3D6rr1pf7bcomt6ajnr33pasja89w17rz1ua7fpnpnbncejhw71p=
muufsxnbhddfkrjpccguefnpt4bm9yzhx5t8ngiyhknt3a4jmx9fqz66eftcjthdgco&url=3Da=
HR0cHM6Ly92ay5jb20vaHNlX2NhcmVlcg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:=
rgb(0,127,255);"">=D0=B2=D0=BA=D0=BE=D0=BD=D1=82=D0=B0=D0=BA=D1=82=D0=B5</a>=
,&nbsp;<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D65xo6i=
r3inhaqwjnr33pasja89w17rz1ua7fpnpnbncejhw71pmu51edq88ia957nfxaehrcfgzgg99yz=
hx5t8ngiyhknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuZmFjZWJvb2suY29t=
L2hzZS52YWNhbmN5Lw~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);=
"">=D1=84=D0=B5=D0=B9=D1=81=D0=B1=D1=83=D0=BA=D0=B5</a>&nbsp;=D0=B8&nbsp;<a =
href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6oubpxiw6bnb81jn=
r33pasja89w17rz1ua7fpnpnbncejhw71pmu18bxo96umy51d4m3an9iwf54eedowuby7eb1cf5=
ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cDovL3QtZG8ucnUvaHNlY2FyZWVy&uid=3DMTM=
yMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D1=82=D0=B5=D0=BB=D0=B5=D0=
=B3=D1=80=D0=B0=D0=BC=D0=B5</a>.</span></div>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</center>
<table bgcolor=3D""white"" align=3D""left"" width=3D""100%""><tr><td><span style=
=3D""font-family: arial,helvetica,sans-serif; color: black; font-size: 12px;=
""><p style=3D""text-align: center; color: #bababa;"">=D0=A7=D1=82=D0=BE=D0=B1=
=D1=8B =D0=BE=D1=82=D0=BF=D0=B8=D1=81=D0=B0=D1=82=D1=8C=D1=81=D1=8F =D0=BE=
=D1=82 =D1=8D=D1=82=D0=BE=D0=B9 =D1=80=D0=B0=D1=81=D1=81=D1=8B=D0=BB=D0=BA=
=D0=B8, =D0=BF=D0=B5=D1=80=D0=B5=D0=B9=D0=B4=D0=B8=D1=82=D0=B5 =D0=BF=D0=BE=
 <a style=3D""color: #46a8c6;"" href=3D""https://unimail.hse.ru/ru/unsubscribe=
?hash=3D6fjiuf9yn87zh7157fxz57m156w17rz1ua7fpnpnbncejhw71pmuzuu7w7dc9ra8osj=
ozspcmjaf1b85ox1u8n1qzyr#no_tracking"">=D1=81=D1=81=D1=8B=D0=BB=D0=BA=D0=B5<=
/a></p></span></td></tr></table><center><table><tr><td><img src=3D""https://=
unimail.hse.ru/ru/mail_read_tracker/1323674?hash=3D6nngqoytp3y7psiwgeupcxy5=
q8w17rz1ua7fpnpnbncejhw71pmu51ucwk9tbjy81p9bbmszibib9x9z1bcmncxy3ur"" width=
=3D""1"" height=3D""1"" alt=3D"""" title=3D"""" border=3D""0""></td></tr></table></ce=
nter></body>
</html>
";

        private const string BodyHse4 = @"<!DOCTYPE html>
<html>
<head>
<meta name=3D""viewport"" content=3D""width=3Ddevice-width, initial-scale=3D1""=
>
<title></title>

<style type=3D""text/css"">
/* ///////// CLIENT-SPECIFIC STYLES ///////// */
#outlook a{padding:0;} /* Force Outlook to provide a 'view in browser' mess=
age */
.ReadMsgBody{width:100%;} .ExternalClass{width:100%;} /* Force Hotmail to d=
isplay emails at full width */
.ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font,=
 .ExternalClass td, .ExternalClass div {line-height: 100%;} /* Force Hotmai=
l to display normal line spacing */
body, table, td, p, a, li, blockquote{-webkit-text-size-adjust:100%; -ms-te=
xt-size-adjust:100%;} /* Prevent WebKit and Windows mobile changing default=
 text sizes */
table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;} /* Remove spacing be=
tween tables in Outlook 2007 and up */
img{-ms-interpolation-mode:bicubic;} /* Allow smoother rendering of resized=
 image in Internet Explorer */
/* ///////// RESET STYLES ///////// */
body{margin:0; padding:0;}
img{border:0; height:auto; line-height:100%; outline:none; text-decoration:=
none;}
table{border-collapse:collapse !important;}
table td { border-collapse: collapse !important;}
body, #bodyTable, #bodyCell{height:100% !important; margin:0; padding:0; wi=
dth:100% !important;}
#mailBody.mailBody .uni-block.button-block { display:block; } /* Specific u=
kr.net style*/
body {
margin: 0;
text-align: left;
}
pre {
white-space: pre;
white-space: pre-wrap;
word-wrap: break-word;
}
table.mhLetterPreview { width:100%; }
table {
mso-table-lspace:0pt;
mso-table-rspace:0pt;
}
img {
-ms-interpolation-mode:bicubic;
}
</style>

<style id=3D""media_css"" type=3D""text/css"">
@media all and (max-width: 480px), only screen and (max-device-width : 480p=
x) {
    body{width:100% !important; min-width:100% !important;} /* Prevent iOS =
Mail from adding padding to the body */
    table[class=3D'container-table'] {
       width:100% !important;
    }
    td.image-wrapper {
       padding: 0 !important;
    }
    td.image-wrapper, td.text-wrapper {
       display:block !important;
       width:100% !important;
       max-width:initial !important;
    }
    table[class=3D'wrapper1'] {
       table-layout: fixed !important;
       padding: 0 !important;
       max-width: 600px !important;
    }
    td[class=3D'wrapper-row'] {
       table-layout: fixed !important;
       box-sizing: border-box !important;
       width:100% !important;
       min-width:100% !important;
    }
    table[class=3D'wrapper2'] {
       table-layout: fixed !important;
       border: none !important;
       width: 100% !important;
       max-width: 600px !important;
       min-height: 520px !important;
    }
    div[class=3D'column-wrapper']{
       max-width:300px !important;
    }
    table.uni-block {
       max-width:100% !important;
       height:auto !important;
       min-height: auto !important;
    }
    table[class=3D'block-wrapper-inner-table'] {
       height:auto !important;
       min-height: auto !important;
    }
    td[class=3D'block-wrapper'] {
       height:auto !important;
       min-height: auto !important;
    }
    .submit-button-block .button-wrapper=20
{       height: auto !important;
       width: auto !important;
       min-height: initial !important;
       max-height: initial !important;
       min-width: initial !important;
       max-width: initial !important;
    }
    img[class=3D'image-element'] {
       height:auto !important;
       box-sizing: border-box !important;
    }
}
@media all and (max-width: 615px), only screen and (max-device-width : 615p=
x) {
    td[class=3D'wrapper-row'] {
       padding: 0 !important;
       margin: 0 !important;
    }
    .column {
       width:100% !important;
       max-width:100% !important;
    }
}
</style>
<meta http-equiv=3D""Content-Type"" content=3D""text/html;charset=3DUTF-8"">
</head>
<body width=3D""100%"" align=3D""center"">
<!--[if gte mso 9]>       <style type=3D""text/css"">           .uni-block im=
g {               display:block !important;           }       </style><![en=
dif]-->
<center>
<table cellpadding=3D""0"" cellspacing=3D""0"" align=3D""center"" width=3D""100%"" =
class=3D""container responsive"">
<tbody>
<tr>
<td>
<table cellpadding=3D""0"" cellspacing=3D""0"" align=3D""center"" class=3D""wrappe=
r1"" style=3D""width: 100%; box-sizing: border-box; background-color: rgb(206=
, 230, 255); float: left;"">
<tbody>
<tr>
<td class=3D""wrapper-row"" style=3D""padding: 25px;""><!--[if (gte mso 9)|(IE)=
]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" ali=
gn=3D""center""><tr><td><![endif]-->
<table cellpadding=3D""0"" cellspacing=3D""0"" class=3D""wrapper2"" align=3D""cent=
er"" style=3D""background-color: rgb(255, 255, 255); border-radius: 3px; max-=
width: 600px; width: 100%; border: none; margin: 0px auto; border-spacing: =
0px; border-collapse: collapse;"">
<tbody>
<tr>
<td width=3D""100%"" class=3D""wrapper-row"" style=3D""vertical-align: top; max-=
width: 600px; font-size: 0px; padding: 0px;""><!--[if (gte mso 9)|(IE)]><tab=
le cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600"" align=3D""=
center""><tr><td><![endif]-->
<table class=3D""uni-block social-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: right; height: 0px;"" class=3D""block-w=
rapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 0px;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 10px; background-image: none; text-a=
lign: right;"" class=3D""content-wrapper""><span class=3D""networks-wrapper""><s=
pan class=3D""scl-button scl-inst""><a href=3D""https://unimail.hse.ru/ru/mail=
_link_tracker?hash=3D6comrzhui37ywrjnr33pasja89w17rz1ua7fpnpfm43usgmoqamb45=
8497jua5xz1czt46zm7w7yeodowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0c=
HM6Ly93d3cuaW5zdGFncmFtLmNvbS9zdHVkbGlmZWhzZS8_dXRtX21lZGl1bT1lbWFpbCZ1dG1f=
c291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjMyODc4MjMy&uid=3DMTMyMzY3NA=3D=3D=
"" target=3D""_blank""><img style=3D""max-height:64px;max-width:64px;"" src=3D""h=
ttp://unimail.hse.ru/v5/img/ico/scl/inst.png"" alt=3D""Instagram""></a></span>=
 <span class=3D""scl-button scl-vk""><a href=3D""https://unimail.hse.ru/ru/mai=
l_link_tracker?hash=3D6dgnmwqwzdeu1cjnr33pasja89w17rz1ua7fpnpfm43usgmoqamb1=
hh3xujjyfay7umaafh1wr8fseeyztq9354zmy19xro34zj6hpe5r5gw4qgwa48ay&url=3DaHR0=
cHM6Ly92ay5jb20vc3R1ZGxpZmVfaHNlP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1Vbml=
TZW5kZXImdXRtX2NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" target=3D=
""_blank""><img style=3D""max-height:64px;max-width:64px;"" src=3D""http://unima=
il.hse.ru/v5/img/ico/scl/vk.png"" alt=3D""=D0=92=D0=BA=D0=BE=D0=BD=D1=82=D0=
=B0=D0=BA=D1=82=D0=B5""></a></span> <span class=3D""scl-button scl-custom""><a=
 href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6cg3zurhctuy8aj=
nr33pasja89w17rz1ua7fpnpfm43usgmoqamb4nm8oixetr5gu3b9d6qqgu13aceyztq9354zmy=
19xro34zj6hpe5r5gw4qgwa48ay&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci8_dXRtX21lZ=
Gl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjMyODc4MjMy&uid=
=3DMTMyMzY3NA=3D=3D"" target=3D""_blank""><img style=3D""max-height:64px;max-wi=
dth:64px;"" src=3D""http://unimail.hse.ru/v5/img/ico/scl/custom.png"" alt=3D""=
=D0=9C=D0=BE=D0=B9 =D1=81=D0=B0=D0=B9=D1=82""></a></span></span></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block image-block"" width=3D""100%"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heig=
ht: auto; border-collapse: collapse; border-spacing: 0px; display: inline-t=
able; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; padding: 5px 10px 20px 20=
px; height: 100%;"" class=3D""block-wrapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 116.875px; width: 100%; table-layout: fix=
ed; text-align: center; border-spacing: 0px; border-collapse: collapse; fon=
t-size: 0px;"">
<tbody>
<tr>
<td style=3D""width: auto; height: 100%; display: inline-table;"" class=3D""co=
ntent-wrapper"">
<table class=3D""content-box"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""=
0"" style=3D""display: inline-table; vertical-align: top; width: auto; height=
: 100%; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""vertical-align: middle;"">
<div class=3D""image-wrapper image-drop""><a class=3D""image-link"" href=3D""htt=
ps://unimail.hse.ru/ru/mail_link_tracker?hash=3D6j7gxejecdwirejnr33pasja89w=
17rz1ua7fpnpfm43usgmoqambumdt3rpge1fk43b9d6qqgu13aceyztq9354zmy19xro34zj6hp=
e5r5gw4qgwa48ay&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci8_dXRtX21lZGl1bT1lbWFpb=
CZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjMyODc4MjMy&uid=3DMTMyMzY3N=
A=3D=3D"" target=3D""_blank""><img class=3D""image-element"" src=3D""http://unima=
il.hse.ru/ru/user_file?resource=3Dhimg&user_id=3D1323674&name=3D6jwsf8rjn9j=
aq3piu3azzngxco47cfqz5xza57gibw4u5ycqngamjhppq9816zw4zzhmwomy54f9so"" alt=3D=
""Some Image"" id=3D""gridster_block_386_main_img"" style=3D""font-size: small; =
border: none; width: auto; max-width: 136px; height: auto; max-height: 117p=
x; outline: none; text-decoration: none;"" width=3D""136""></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block image-block"" width=3D""100%"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heig=
ht: auto; border-collapse: collapse; border-spacing: 0px; display: inline-t=
able; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; padding: 0px 0px 15px; he=
ight: 100%;"" class=3D""block-wrapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 205px; width: 100%; table-layout: fixed; =
text-align: center; border-spacing: 0px; border-collapse: collapse; font-si=
ze: 0px;"">
<tbody>
<tr>
<td style=3D""width: auto; height: 100%; display: inline-table;"" class=3D""co=
ntent-wrapper"">
<table class=3D""content-box"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""=
0"" style=3D""display: inline-table; vertical-align: top; width: auto; height=
: 100%; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""vertical-align: middle;"">
<div class=3D""image-wrapper image-drop""><a class=3D""image-link"" href=3D""jav=
ascript:;"" target=3D""_self""><img class=3D""image-element"" src=3D""http://unim=
ail.hse.ru/ru/user_file?resource=3Dhimg&user_id=3D1323674&name=3D67rztaxrgy=
zpfzpiu3azzngxco47cfqz5xza57gtg4u3hzh3n68mjhppq9816zw4zff4ri7fzsig74"" alt=
=3D""Some Image"" id=3D""gridster_block_440_main_img"" style=3D""font-size: smal=
l; border: none; width: 100%; max-width: 600px; height: auto; max-height: 2=
00px; outline: none; text-decoration: none;"" width=3D""600""></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 1140px; width: 100%; table-layout: fixed;=
 border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px; vertical-align: top; font-siz=
e: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; colo=
r: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<p><span style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D=
""line-height:1.5;""><span style=3D""font-size:16px;""><span style=3D""font-fami=
ly:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><span st=
yle=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-s=
erif;""><span style=3D""line-height:1.5;""><span style=3D""font-size:16px;""><st=
rong>=D0=A1 =D0=BD=D0=BE=D0=B2=D0=BE=D0=B3=D0=BE =D1=83=D1=87=D0=B5=D0=B1=
=D0=BD=D0=BE=D0=B3=D0=BE =D0=B3=D0=BE=D0=B4=D0=B0 =D0=B2 =D0=92=D1=8B=D1=88=
=D0=BA=D0=B5 =D0=BD=D0=B0=D1=87=D0=BD=D0=B5=D1=82 =D1=80=D0=B0=D0=B1=D0=BE=
=D1=82=D1=83 =D0=92=D1=8B=D1=81=D1=88=D0=B0=D1=8F =D1=88=D0=BA=D0=BE=D0=BB=
=D0=B0 =D0=B1=D0=B8=D0=B7=D0=BD=D0=B5=D1=81=D0=B0</strong><br>
=D0=9D=D0=B0 =D1=81=D1=82=D0=B0=D1=80=D1=82=D0=B5 =D0=92=D0=A8=D0=91 =D0=BE=
=D0=B1=D1=8A=D0=B5=D0=B4=D0=B8=D0=BD=D0=B8=D1=82 22 =D0=BF=D1=80=D0=BE=D0=
=B3=D1=80=D0=B0=D0=BC=D0=BC=D1=8B =D0=B1=D0=B0=D0=BA=D0=B0=D0=BB=D0=B0=D0=
=B2=D1=80=D0=B8=D0=B0=D1=82=D0=B0 =D0=B8 =D0=BC=D0=B0=D0=B3=D0=B8=D1=81=D1=
=82=D1=80=D0=B0=D1=82=D1=83=D1=80=D1=8B (3 =D0=B8=D0=B7 =D0=BD=D0=B8=D1=85 =
=E2=80=93 =D0=BD=D0=B0 =D0=B0=D0=BD=D0=B3=D0=BB=D0=B8=D0=B9=D1=81=D0=BA=D0=
=BE=D0=BC =D1=8F=D0=B7=D1=8B=D0=BA=D0=B5) =D0=B8 =D0=B1=D0=BE=D0=BB=D0=B5=
=D0=B5 200 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC =D0=BF=D0=B5=D1=
=80=D0=B5=D0=BF=D0=BE=D0=B4=D0=B3=D0=BE=D1=82=D0=BE=D0=B2=D0=BA=D0=B8 =D0=
=B8 =D0=BF=D0=BE=D0=B2=D1=8B=D1=88=D0=B5=D0=BD=D0=B8=D1=8F =D0=BA=D0=B2=D0=
=B0=D0=BB=D0=B8=D1=84=D0=B8=D0=BA=D0=B0=D1=86=D0=B8=D0=B8. =D0=9F=D0=BE=D0=
=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5 =D1=87=D0=B8=D1=82=D0=B0=D0=B9=D1=
=82=D0=B5 <a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6a9=
15qj5gzdwyrjnr33pasja89w17rz1ua7fpnpfm43usgmoqamboh6t9463npa8zh5e3nqto541ms=
dowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L25ld=
3MvZWR1LzM2MTIyNTIyMy5odG1sP3V0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9tZWRpdW09ZW1h=
aWwmdXRtX2NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color=
:rgb(0,127,255);"">=D0=B2 =D0=BC=D0=B0=D1=82=D0=B5=D1=80=D0=B8=D0=B0=D0=BB=
=D0=B5</a> =D0=BD=D0=BE=D0=B2=D0=BE=D1=81=D1=82=D0=BD=D0=BE=D0=B9 =D1=81=D0=
=BB=D1=83=D0=B6=D0=B1=D1=8B =D0=BF=D0=BE=D1=80=D1=82=D0=B0=D0=BB=D0=B0.<br>
<br>
<strong>=D0=9F=D0=BE=D0=BA=D1=80=D0=BE=D0=B2=D0=BA=D0=B0 =D1=82=D0=B5=D0=BF=
=D0=B5=D1=80=D1=8C =D0=B8 =D0=B2 Minecraft</strong><br>
3 =D0=BC=D0=B0=D1=8F =D0=BF=D1=80=D0=BE=D1=88=D0=BB=D0=BE =D1=82=D0=BE=D1=
=80=D0=B6=D0=B5=D1=81=D1=82=D0=B2=D0=B5=D0=BD=D0=BD=D0=BE=D0=B5 =D0=BE=D1=
=82=D0=BA=D1=80=D1=8B=D1=82=D0=B8=D0=B5 =D0=B2=D0=B8=D1=80=D1=82=D1=83=D0=
=B0=D0=BB=D1=8C=D0=BD=D0=BE=D0=B3=D0=BE =D0=BA=D0=BE=D1=80=D0=BF=D1=83=D1=
=81=D0=B0. =D0=92 <a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?ha=
sh=3D6q7jp3edhwywykjnr33pasja89w17rz1ua7fpnpfm43usgmoqamb1yjjrbdgyf36kbasxy=
qoj1k67weyztq9354zmy19xro34zj6hpe5r5gw4qgwa48ay&url=3DaHR0cHM6Ly93d3cuaHNlL=
nJ1L291ci9uZXdzLzM2MjM3MjA0OC5odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1V=
bmlTZW5kZXImdXRtX2NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=
=3D""color:rgb(0,127,255);"">=D1=81=D0=BA=D1=80=D0=B8=D0=BD=D1=88=D0=BE=D1=82=
-=D1=80=D0=B5=D0=BF=D0=BE=D1=80=D1=82=D0=B0=D0=B6=D0=B5</a> =D0=BF=D0=BE=D0=
=BA=D0=B0=D0=B7=D1=8B=D0=B2=D0=B0=D0=B5=D0=BC, =D0=BA=D0=B0=D0=BA =D0=BE=D0=
=BD =D0=B2=D1=8B=D0=B3=D0=BB=D1=8F=D0=B4=D0=B8=D1=82 =D0=B8=D0=B7=D0=BD=D1=
=83=D1=82=D1=80=D0=B8. =D0=A2=D0=B5=D0=BF=D0=B5=D1=80=D1=8C =D0=B4=D0=B8=D1=
=81=D1=82=D0=B0=D0=BD=D1=86=D0=B8=D0=BE=D0=BD=D0=BD=D0=BE =D0=BC=D0=BE=D0=
=B6=D0=BD=D0=BE =D0=BD=D0=B5 =D1=82=D0=BE=D0=BB=D1=8C=D0=BA=D0=BE =D1=83=D1=
=87=D0=B8=D1=82=D1=8C=D1=81=D1=8F =D0=B8 =D1=80=D0=B0=D0=B1=D0=BE=D1=82=D0=
=B0=D1=82=D1=8C, =D0=BD=D0=BE =D0=B8 =D0=B3=D1=83=D0=BB=D1=8F=D1=82=D1=8C =
=D0=BF=D0=BE =D0=B0=D1=82=D1=80=D0=B8=D1=83=D0=BC=D1=83, =D1=81=D1=82=D0=BE=
=D0=BB=D0=BE=D0=B2=D0=BE=D0=B9 =D0=B8 =D0=B1=D0=B8=D0=B1=D0=BB=D0=B8=D0=BE=
=D1=82=D0=B5=D0=BA=D0=B5! =D0=A3=D0=B7=D0=BD=D0=B0=D1=82=D1=8C =D0=B1=D0=BE=
=D0=BB=D1=8C=D1=88=D0=B5 =D0=BE =D1=82=D0=BE=D0=BC, =D0=BA=D0=B0=D0=BA =D0=
=BD=D0=B0=D1=87=D0=B8=D0=BD=D0=B0=D0=BB=D0=BE=D1=81=D1=8C =D1=81=D1=82=D1=
=80=D0=BE=D0=B8=D1=82=D0=B5=D0=BB=D1=8C=D1=81=D1=82=D0=B2=D0=BE =D0=B8 =D0=
=BA=D0=B0=D0=BA=D0=B8=D0=B5 =D0=B2=D0=BE=D0=B7=D0=BC=D0=BE=D0=B6=D0=BD=D0=
=BE=D1=81=D1=82=D0=B8&nbsp;=D0=BE=D1=82=D0=BA=D1=80=D1=8B=D1=82=D1=8B =D1=
=81=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D0=B0=D0=BC =D0=BE=D0=BD=D0=BB=D0=
=B0=D0=B9=D0=BD, =D0=BC=D0=BE=D0=B6=D0=BD=D0=BE =D0=B8=D0=B7 <a href=3D""htt=
ps://unimail.hse.ru/ru/mail_link_tracker?hash=3D6x6mj15r3f455qjnr33pasja89w=
17rz1ua7fpnpfm43usgmoqambuoqia59u91pjzhcgob8358eee6dowuby7eb1cf5ge75tbx3h5j=
71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLzM2MTQ0MTk4My5od=
G1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIz=
Mjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=B8=
=D0=BD=D1=82=D0=B5=D1=80=D0=B2=D1=8C=D1=8E</a> =D1=81 =D0=BE=D1=80=D0=B3=D0=
=B0=D0=BD=D0=B8=D0=B7=D0=B0=D1=82=D0=BE=D1=80=D0=BE=D0=BC =D0=BF=D1=80=D0=
=BE=D0=B5=D0=BA=D1=82=D0=B0 =D0=9E=D0=BB=D0=B5=D1=81=D0=B5=D0=B9 =D0=AF=D0=
=BA=D0=BE=D0=B2=D0=BB=D0=B5=D0=B2=D0=BE=D0=B9.<br>
<br>
<strong>=D0=9E =D1=80=D0=B5=D0=B6=D0=B8=D0=BC=D0=B5 =D0=B8=D0=B7=D0=BE=D0=
=BB=D1=8F=D1=86=D0=B8=D0=B8 =D0=B2 =D0=BE=D0=B1=D1=89=D0=B5=D0=B6=D0=B8=D1=
=82=D0=B8=D1=8F=D1=85</strong><br>
=D0=A1 29 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F =D0=B2 =D0=BE=D0=B1=D1=89=D0=
=B5=D0=B6=D0=B8=D1=82=D0=B8=D1=8F=D1=85 =D0=92=D1=8B=D1=88=D0=BA=D0=B8 <a h=
ref=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D64dou39dk9otbejnr=
33pasja89w17rz1ua7fpnpfm43usgmoqamb959dmuiy4odmnc53yay7hop47hdowuby7eb1cf5g=
e75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLzM2MDk=
xMjI5OC5odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbX=
BhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255=
);"">=D0=B2=D0=B2=D0=B5=D0=B4=D0=B5=D0=BD=D0=B0</a> =D0=BE=D0=B1=D1=8F=D0=B7=
=D0=B0=D1=82=D0=B5=D0=BB=D1=8C=D0=BD=D0=B0=D1=8F =D1=81=D0=B0=D0=BC=D0=BE=
=D0=B8=D0=B7=D0=BE=D0=BB=D1=8F=D1=86=D0=B8=D1=8F =D0=B4=D0=BB=D1=8F =D1=81=
=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=B2, =D0=BA=D0=BE=D1=82=D0=BE=
=D1=80=D1=8B=D0=B5 =D0=B2=D0=BE=D0=B7=D0=B2=D1=80=D0=B0=D1=89=D0=B0=D1=8E=
=D1=82=D1=81=D1=8F =D0=B8=D0=B7 =D0=B4=D1=80=D1=83=D0=B3=D0=B8=D1=85 =D1=80=
=D0=B5=D0=B3=D0=B8=D0=BE=D0=BD=D0=BE=D0=B2. =D0=9E=D1=82=D0=B2=D0=B5=D1=82=
=D1=8B =D0=BD=D0=B0 =D0=B3=D0=BB=D0=B0=D0=B2=D0=BD=D1=8B=D0=B5 =D0=B2=D0=BE=
=D0=BF=D1=80=D0=BE=D1=81=D1=8B, =D1=81=D0=B2=D1=8F=D0=B7=D0=B0=D0=BD=D0=BD=
=D1=8B=D0=B5 =D1=81 =D0=BD=D0=BE=D0=B2=D0=BE=D0=B9 =D0=BC=D0=B5=D1=80=D0=BE=
=D0=B9, =D0=BC=D0=BE=D0=B6=D0=BD=D0=BE =D0=BD=D0=B0=D0=B9=D1=82=D0=B8 =D0=
=B2 <a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6nihxx484=
xirk6jnr33pasja89w17rz1ua7fpnpfm43usgmoqambs3f9st4iojxrxfihegrsuijomweyztq9=
354zmy19xro34zj6hpe5r5gw4qgwa48ay&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXd=
zLzM2MTYwMTgzMy5odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdX=
RtX2NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0=
,127,255);"">=D0=BD=D0=B0=D1=88=D0=B5=D0=BC =D0=BC=D0=B0=D1=82=D0=B5=D1=80=
=D0=B8=D0=B0=D0=BB=D0=B5</a> =D0=B8 =D0=B2 <a href=3D""https://unimail.hse.r=
u/ru/mail_link_tracker?hash=3D6fy7dnpxhp9z3kjnr33pasja89w17rz1ua7fpnpfm43us=
gmoqambzayifqpgethbt1p7zw4ttefr7odowuby7eb1cf5ge75tbx3h5j71sywopdfiu6auo&ur=
l=3DaHR0cHM6Ly92ay5jb20vd2FsbC02NDk1MjM2Nl81NTYyP3V0bV9tZWRpdW09ZW1haWwmdXR=
tX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=
=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=BF=D0=BE=D1=81=D1=82=D0=B5</a> =
=D0=A1=D1=82=D1=83=D0=B4=D1=81=D0=BE=D0=B2=D0=B5=D1=82=D0=B0.&nbsp;<br>
<br>
<strong>=D0=A6=D0=B5=D0=BD=D1=82=D1=80 =D1=83=D0=BD=D0=B8=D0=B2=D0=B5=D1=80=
=D1=81=D0=B8=D1=82=D0=B5=D1=82=D1=81=D0=BA=D0=BE=D0=B9 =D0=B0=D0=BD=D1=82=
=D1=80=D0=BE=D0=BF=D0=BE=D0=BB=D0=BE=D0=B3=D0=B8=D0=B8 =D0=B8 =D0=BA=D1=83=
=D0=BB=D1=8C=D1=82=D1=83=D1=80=D1=8B =D0=B7=D0=B0=D0=BF=D1=83=D1=81=D1=82=
=D0=B8=D0=BB =D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82 =D0=B2 =D0=B6=D0=B0=D0=BD=
=D1=80=D0=B5 =D0=BA=D0=BE=D0=BB=D0=BB=D0=B5=D0=BA=D1=82=D0=B8=D0=B2=D0=BD=
=D0=BE=D0=B3=D0=BE =D0=B4=D0=BD=D0=B5=D0=B2=D0=BD=D0=B8=D0=BA=D0=B0</strong=
><br>
=D0=98=D0=B4=D0=B5=D1=8F =D0=B2 =D1=82=D0=BE=D0=BC, =D1=87=D1=82=D0=BE=D0=
=B1=D1=8B =D0=B7=D0=B0=D1=84=D0=B8=D0=BA=D1=81=D0=B8=D1=80=D0=BE=D0=B2=D0=
=B0=D1=82=D1=8C =D0=BD=D0=BE=D0=B2=D1=83=D1=8E =D0=B3=D0=BB=D0=B0=D0=B2=D1=
=83 =D0=B2 =D0=B6=D0=B8=D0=B7=D0=BD=D0=B8 =D1=83=D0=BD=D0=B8=D0=B2=D0=B5=D1=
=80=D1=81=D0=B8=D1=82=D0=B5=D1=82=D0=B0, =D1=81=D1=82=D1=83=D0=B4=D0=B5=D0=
=BD=D1=82=D0=BE=D0=B2 =D0=B8 =D1=81=D0=BE=D1=82=D1=80=D1=83=D0=B4=D0=BD=D0=
=B8=D0=BA=D0=BE=D0=B2 =D0=B2 =D0=B6=D0=B0=D0=BD=D1=80=D0=B5 =C2=AB=D0=BD=D0=
=B0=D1=80=D0=BE=D0=B4=D0=BD=D0=BE=D0=B9 =D0=B8=D1=81=D1=82=D0=BE=D1=80=D0=
=B8=D0=B8=C2=BB. <a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?has=
h=3D67f5a96s658jycjnr33pasja89w17rz1ua7fpnpfm43usgmoqamb9t8yyjzdspq691ghrre=
kb3nuad6ardjf311q9raknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLn=
J1L291ci9uZXdzLzM1OTgwNzY4NC5odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1Vb=
mlTZW5kZXImdXRtX2NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=
=3D""color:rgb(0,127,255);"">=D0=A0=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B6=D0=B8=
=D1=82=D0=B5</a>, =D0=BA=D0=B0=D0=BA =D0=BF=D1=80=D0=BE=D1=85=D0=BE=D0=B4=
=D0=B8=D1=82 =D0=B2=D0=B0=D1=88 =D0=B4=D0=B5=D0=BD=D1=8C, =D1=87=D1=82=D0=
=BE =D0=B2=D1=8B =D1=87=D1=83=D0=B2=D1=81=D1=82=D0=B2=D1=83=D0=B5=D1=82=D0=
=B5, =D0=BA=D0=B0=D0=BA =D0=BF=D0=B0=D0=BD=D0=B4=D0=B5=D0=BC=D0=B8=D1=8F =
=D0=B8=D0=B7=D0=BC=D0=B5=D0=BD=D0=B8=D0=BB=D0=B0 =D0=B2=D0=B0=D1=88=D1=83 =
=D0=B6=D0=B8=D0=B7=D0=BD=D1=8C =D0=B8 =D0=BA=D0=B0=D0=BA =D0=BE=D0=BD=D0=B0=
 =D0=BC=D0=BE=D0=B6=D0=B5=D1=82 =D0=B8=D0=B7=D0=BC=D0=B5=D0=BD=D0=B8=D1=82=
=D1=8C =D0=BC=D0=B8=D1=80. =D0=9F=D0=BE=D0=B4=D0=B5=D0=BB=D0=B8=D1=82=D1=8C=
=D1=81=D1=8F =D0=B8=D1=81=D1=82=D0=BE=D1=80=D0=B8=D0=B5=D0=B9 =D0=BC=D0=BE=
=D0=B6=D0=BD=D0=BE, =D0=B7=D0=B0=D0=BF=D0=BE=D0=BB=D0=BD=D0=B8=D0=B2 <a hre=
f=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6m8kqif8euuipejnr33=
pasja89w17rz1ua7fpnpfm43usgmoqamb1mhd8n4rh3bwtezehus6widjnw3twcgsaro76uhknt=
3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9zZG8uaHNlLnJ1L3VhYy9wb2xscy8zNTk4M=
DQ4NTUuaHRtbD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1w=
YWlnbj0yMzI4NzgyMzI~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);=
"">=D1=84=D0=BE=D1=80=D0=BC=D1=83</a> =D0=B8=D0=BB=D0=B8 =D0=BE=D0=BF=D1=83=
=D0=B1=D0=BB=D0=B8=D0=BA=D0=BE=D0=B2=D0=B0=D0=B2 =D1=80=D0=B0=D1=81=D1=81=
=D0=BA=D0=B0=D0=B7 =D0=B2 =D1=81=D0=BE=D1=86=D1=81=D0=B5=D1=82=D1=8F=D1=85 =
=D1=81 =D1=85=D0=B5=D1=88=D1=82=D0=B5=D0=B3=D0=BE=D0=BC #=D0=92=D0=A8=D0=AD=
=D0=BD=D0=B0=D0=BA=D0=B0=D1=80=D0=B0=D0=BD=D1=82=D0=B8=D0=BD=D0=B5.<br>
<br>
<strong>=D0=92 =D0=92=D1=8B=D1=88=D0=BA=D0=B5 =D1=81=D1=82=D0=B0=D1=80=D1=
=82=D1=83=D0=B5=D1=82 =D0=BE=D0=B1=D1=89=D0=B5=D1=83=D0=BD=D0=B8=D0=B2=D0=
=B5=D1=80=D1=81=D0=B8=D1=82=D0=B5=D1=82=D1=81=D0=BA=D0=B8=D0=B9 =D0=BA=D0=
=BE=D0=BD=D0=BA=D1=83=D1=80=D1=81 =D0=BD=D0=B0 =D1=81=D0=BE=D0=B7=D0=B4=D0=
=B0=D0=BD=D0=B8=D0=B5 =D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D0=BD=D1=8B=D1=
=85 =D0=B3=D1=80=D1=83=D0=BF=D0=BF</strong><br>
=D0=9F=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D1=8B =D0=BC=D0=BE=D0=B3=D1=83=D1=82 =
=D0=B1=D1=8B=D1=82=D1=8C =D1=81=D0=B0=D0=BC=D1=8B=D0=BC=D0=B8 =D1=80=D0=B0=
=D0=B7=D0=BD=D1=8B=D0=BC=D0=B8, =D0=BD=D0=B0=D1=87=D0=B8=D0=BD=D0=B0=D1=8F =
=D0=BE=D1=82 =D1=84=D1=83=D0=BD=D0=B4=D0=B0=D0=BC=D0=B5=D0=BD=D1=82=D0=B0=
=D0=BB=D1=8C=D0=BD=D1=8B=D1=85 =D0=B8=D1=81=D1=81=D0=BB=D0=B5=D0=B4=D0=BE=
=D0=B2=D0=B0=D0=BD=D0=B8=D0=B9 =D0=B8 =D0=B7=D0=B0=D0=BA=D0=B0=D0=BD=D1=87=
=D0=B8=D0=B2=D0=B0=D1=8F =D0=BF=D1=80=D0=BE=D1=81=D0=B2=D0=B5=D1=82=D0=B8=
=D1=82=D0=B5=D0=BB=D1=8C=D1=81=D0=BA=D0=B8=D0=BC=D0=B8 =D0=B8=D0=BB=D0=B8 =
=D0=B0=D1=80=D1=82-=D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D0=B0=D0=BC=D0=B8. =
=D0=97=D0=B0=D1=8F=D0=B2=D0=BA=D0=B8 =D0=B1=D1=83=D0=B4=D1=83=D1=82 =D0=BF=
=D1=80=D0=B8=D0=BD=D0=B8=D0=BC=D0=B0=D1=82=D1=8C=D1=81=D1=8F =D0=B4=D0=BE 1=
 =D0=B8=D1=8E=D0=BD=D1=8F. =D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=
=D0=B5 =D0=BE =D0=BA=D0=BE=D0=BD=D0=BA=D1=83=D1=80=D1=81=D0=B5 <a href=3D""h=
ttps://unimail.hse.ru/ru/mail_link_tracker?hash=3D6g3y9b1oau3y71jnr33pasja8=
9w17rz1ua7fpnpfm43usgmoqambzkxd8ygf13q6eyfsbn1n6ytryuiymni3oot4gwcknt3a4jmx=
9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLzM2MDkwMTYwNC5=
odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPT=
IzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D1=
=80=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B7=D0=B0=D0=BB</a> =D0=BF=D0=B5=D1=80=
=D0=B2=D1=8B=D0=B9 =D0=BF=D1=80=D0=BE=D1=80=D0=B5=D0=BA=D1=82=D0=BE=D1=80 =
=D0=92=D0=B0=D0=B4=D0=B8=D0=BC =D0=A0=D0=B0=D0=B4=D0=B0=D0=B5=D0=B2.<br>
<br>
<strong>=D0=9D=D0=B0=D1=87=D0=B0=D0=BB=D1=81=D1=8F =D0=BF=D1=80=D0=B8=D0=B5=
=D0=BC =D0=B7=D0=B0=D1=8F=D0=B2=D0=BE=D0=BA =D0=BD=D0=B0 =D0=B2=D0=B5=D1=81=
=D0=B5=D0=BD=D0=BD=D0=B8=D0=B9 =D0=BA=D0=BE=D0=BD=D0=BA=D1=83=D1=80=D1=81 =
=D0=BF=D0=BE=D0=B4=D0=B4=D0=B5=D1=80=D0=B6=D0=BA=D0=B8 =D1=81=D1=82=D1=83=
=D0=B4=D0=B5=D0=BD=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D1=85 =D0=B8=D0=BD=D0=B8=
=D1=86=D0=B8=D0=B0=D1=82=D0=B8=D0=B2</strong><br>
=D0=97=D0=B0=D1=8F=D0=B2=D0=BA=D1=83 =D0=BD=D0=B0 =D1=83=D1=87=D0=B0=D1=81=
=D1=82=D0=B8=D0=B5 =D0=BC=D0=BE=D0=B6=D0=BD=D0=BE =D0=BF=D0=BE=D0=B4=D0=B0=
=D1=82=D1=8C =D0=B4=D0=BE 19 =D0=BC=D0=B0=D1=8F =D0=B2 =D0=BE=D0=B4=D0=BD=
=D0=BE=D0=B9 6 =D0=BA=D0=B0=D1=82=D0=B5=D0=B3=D0=BE=D1=80=D0=B8=D0=B9: =D0=
=BD=D0=B0=D1=83=D0=BA=D0=B0 =D0=B8 =D0=BE=D0=B1=D1=80=D0=B0=D0=B7=D0=BE=D0=
=B2=D0=B0=D0=BD=D0=B8=D0=B5; =D1=81=D0=BF=D0=BE=D1=80=D1=82 =D0=B8 =D0=B7=
=D0=B4=D0=BE=D1=80=D0=BE=D0=B2=D1=8C=D0=B5; =D0=BC=D0=B5=D0=B4=D0=B8=D0=B0=
=D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D1=8B; =D0=B4=D0=BE=D0=B1=D1=80=D1=8B=
=D0=B5 =D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D1=8B; =D0=BA=D1=83=D0=BB=D1=8C=
=D1=82=D1=83=D1=80=D0=BD=D0=BE-=D0=BC=D0=B0=D1=81=D1=81=D0=BE=D0=B2=D1=8B=
=D0=B5 =D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D1=8B; =D0=B3=D0=BE=D1=80=D0=BE=
=D0=B4=D1=81=D0=BA=D0=B8=D0=B5 =D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D1=8B. =
=D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=BE=D1=81=D1=82=D0=B8 =D0=BE =
=D0=BF=D1=80=D0=B0=D0=B2=D0=B8=D0=BB=D0=B0=D1=85 =D0=B8 =D0=BE=D0=B1=D1=8A=
=D0=B5=D0=BC=D0=B5 =D1=84=D0=B8=D0=BD=D0=B0=D0=BD=D1=81=D0=B8=D1=80=D0=BE=
=D0=B2=D0=B0=D0=BD=D0=B8=D1=8F =D0=B8=D1=89=D0=B8=D1=82=D0=B5 =D0=BD=D0=B0 =
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D689ps3jo3y3ob=
wjnr33pasja89w17rz1ua7fpnpfm43usgmoqambzjhpij4zc3p3hbxfd9mbjywhg89yzhx5t8ng=
iyhknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9zdHVkc3VwcG9ydC5oc2UucnUvbmV=
3cy8zNjI3MDQ2MzcuaHRtbD91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJn=
V0bV9jYW1wYWlnbj0yMzI4NzgyMzI~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0=
,127,255);"">=D1=81=D1=82=D1=80=D0=B0=D0=BD=D0=B8=D1=86=D0=B5 =D0=A6=D0=9F=
=D0=A1=D0=98</a>.<br>
<br>
<strong>=D0=9D=D0=B0=D1=83=D1=87=D0=BD=D1=8B=D0=B5 =D0=B1=D0=BE=D0=B8 =D0=
=BF=D1=80=D0=BE=D0=B9=D0=B4=D1=83=D1=82 16 =D0=BC=D0=B0=D1=8F</strong><br>
=D0=9D=D0=BE=D0=B2=D1=8B=D0=B5 =D1=81=D0=BF=D0=B8=D0=BA=D0=B5=D1=80=D1=8B, =
=D1=83=D0=B2=D0=BB=D0=B5=D0=BA=D0=B0=D1=82=D0=B5=D0=BB=D1=8C=D0=BD=D1=8B=D0=
=B5 =D0=B8=D1=81=D1=81=D0=BB=D0=B5=D0=B4=D0=BE=D0=B2=D0=B0=D0=BD=D0=B8=D1=
=8F =D0=B8 =D0=BA=D1=83=D1=87=D0=B0 =D1=8D=D0=BC=D0=BE=D1=86=D0=B8=D0=B9 =
=E2=80=93 =D0=B8 =D0=B2=D1=81=D0=B5 =D1=8D=D1=82=D0=BE =D0=BD=D0=B5 =D0=B2=
=D1=8B=D1=85=D0=BE=D0=B4=D1=8F =D0=B8=D0=B7 =D0=B4=D0=BE=D0=BC=D0=B0, =D0=
=B0 =D0=BF=D1=80=D0=B8 =D0=B6=D0=B5=D0=BB=D0=B0=D0=BD=D0=B8=D0=B8 =D0=B4=D0=
=B0=D0=B6=D0=B5 =D0=BD=D0=B5 =D0=B2=D1=81=D1=82=D0=B0=D0=B2=D0=B0=D1=8F =D1=
=81 =D0=B4=D0=B8=D0=B2=D0=B0=D0=BD=D0=B0! =D0=92=D1=81=D1=8F =D0=B8=D0=BD=
=D1=84=D0=BE=D1=80=D0=BC=D0=B0=D1=86=D0=B8=D1=8F <span style=3D""font-family=
:Arial, Helvetica, sans-serif;""><span style=3D""line-height:1.5;""><span styl=
e=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica, sans-ser=
if;""><span style=3D""line-height:1.5;""><span style=3D""font-size:16px;""><span=
 style=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""line-he=
ight:1.5;""><span style=3D""font-size:16px;"">=E2=80=93&nbsp;</span></span></s=
pan></span></span></span></span></span></span>=D0=B2 <a href=3D""https://uni=
mail.hse.ru/ru/mail_link_tracker?hash=3D6d93iimpzynf71jnr33pasja89w17rz1ua7=
fpnpfm43usgmoqamb53mmerhc859yejxmccp38c9e3sdowuby7eb1cf5ge75tbx3h5j71sywopd=
fiu6auo&url=3DaHR0cHM6Ly92ay5jb20vbmJoc2U_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291c=
mNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjMyODc4MjMy&uid=3DMTMyMzY3NA=3D=3D"" sty=
le=3D""color:rgb(0,127,255);"">=D0=B3=D1=80=D1=83=D0=BF=D0=BF=D0=B5</a>, =D0=
=B0 =D1=80=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F <a h=
ref=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6p9eq4pqrrfs8ejnr=
33pasja89w17rz1ua7fpnpfm43usgmoqambhoqzx838ejq13maw6wh8tfgqt4nsnyzrngg11eok=
nt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9zY2llbmNlYmF0dGxlcy50aW1lcGFkLnJ=
1L2V2ZW50LzEzMDkzMDAvP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdX=
RtX2NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0=
,127,255);"">=D1=82=D1=83=D1=82</a>.</span></span></span></span></span></spa=
n></span></span></span></p>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block button-block"" width=3D""100%"" border=3D""0"" cellspa=
cing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; hei=
ght: auto; border-collapse: collapse; border-spacing: 0px; display: inline-=
table; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; min-height: 86.9445px; he=
ight: 86.9445px;"" class=3D""block-wrapper"" valign=3D""middle"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 100%; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 86.9445px;"">
<tbody>
<tr>
<td style=3D""width: 100%; text-align: center;"" class=3D""content-wrapper"">
<table class=3D""valign-wrapper"" border=3D""0"" cellspacing=3D""0"" cellpadding=
=3D""0"" style=3D""display: inline-table; width: auto; border-spacing: 0px; bo=
rder-collapse: collapse;"">
<tbody>
<tr>
<td class=3D""button-wrapper"" align=3D""center"" valign=3D""middle"" style=3D""bo=
rder: none; border-radius: 10px; padding: 0px 30px; background-color: rgb(0=
, 95, 129); height: 50px; min-height: 50px;""><a class=3D""mailbtn"" href=3D""h=
ttps://unimail.hse.ru/ru/mail_link_tracker?hash=3D6sx5m74uas91kajnr33pasja8=
9w17rz1ua7fpnpfm43usgmoqambunwca6h7ryqoabnoq5b8jd153cdowuby7eb1cf5ge75tbx3h=
5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci8_dXRtX21lZGl1bT1lbWF=
pbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjMyODc4MjMy&uid=3DMTMyMzY=
3NA=3D=3D"" target=3D""_blank"" style=3D""width:100%;display:inline-block;text-=
decoration:none;""><span class=3D""btn-inner"" style=3D""display: inline; font-=
size: 16px; font-family: Arial, Helvetica, sans-serif; line-height: 19.2px;=
 color: rgb(255, 255, 255); background-color: rgb(0, 95, 129); border: 0px;=
 word-break: break-all;"">=D0=92=D1=81=D0=B5 =D0=BD=D0=BE=D0=B2=D0=BE=D1=81=
=D1=82=D0=B8</span></a></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(252, 252, 207); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 460px; width: 100%; table-layout: fixed; =
border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px; vertical-align: top; font-siz=
e: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; colo=
r: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:18px;""><span style=3D""font-family:Arial, Helvetica, sans-serif;""=
><span style=3D""line-height:1.5;"">=D0=98=D0=BD=D1=82=D0=B5=D1=80=D0=B5=D1=
=81=D0=BD=D0=BE=D0=B5</span></span></span><br>
<br>
<span style=3D""font-size:16px;""><span style=3D""font-family:Arial, Helvetica=
, sans-serif;""><span style=3D""line-height:1.5;""><strong>=D0=A1=D0=BE=D1=86=
=D0=B8=D0=BE=D0=BB=D0=BE=D0=B3=D0=B8 =D0=BF=D0=B8=D1=82=D0=B5=D1=80=D1=81=
=D0=BA=D0=BE=D0=B9 =D0=92=D1=8B=D1=88=D0=BA=D0=B8 =D0=B7=D0=B0=D0=BF=D1=83=
=D1=81=D1=82=D0=B8=D0=BB=D0=B8 =D0=B1=D0=BB=D0=BE=D0=B3 =C2=ABPandemic Scie=
nce Maps=C2=BB</strong><br>
=D0=9D=D0=B0 <a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D=
6uhj6heb7id88qjnr33pasja89w17rz1ua7fpnpfm43usgmoqamb7oiiods7ppwb9d5uwq1s6wd=
mb43twcgsaro76uhknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cDovL3BhbmRlbWljc2NpZW=
5jZW1hcHMub3JnL3J1Lz91dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0b=
V9jYW1wYWlnbj0yMzI4NzgyMzI~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,12=
7,255);"">=D1=81=D0=B0=D0=B9=D1=82=D0=B5</a> =D0=BF=D1=83=D0=B1=D0=BB=D0=B8=
=D0=BA=D1=83=D1=8E=D1=82 =D0=BE=D0=B1=D0=B7=D0=BE=D1=80=D1=8B =D0=BD=D0=B0 =
=D1=81=D1=82=D0=B0=D1=82=D1=8C=D0=B8 =D0=BE =D0=BC=D0=B8=D1=80=D0=BE=D0=B2=
=D1=8B=D1=85 =D1=8D=D0=BF=D0=B8=D0=B4=D0=B5=D0=BC=D0=B8=D1=8F=D1=85 =D0=B8 =
=D0=BF=D1=80=D0=B5=D0=BF=D1=80=D0=B8=D0=BD=D1=82=D1=8B =D0=B8=D1=81=D1=81=
=D0=BB=D0=B5=D0=B4=D0=BE=D0=B2=D0=B0=D0=BD=D0=B8=D0=B9 =D0=BA=D0=BE=D1=80=
=D0=BE=D0=BD=D0=B0=D0=B2=D0=B8=D1=80=D1=83=D1=81=D0=B0. =D0=A0=D0=B5=D0=B4=
=D0=B0=D0=BA=D1=86=D0=B8=D1=8F =D1=81=D0=BE=D0=B1=D0=B8=D1=80=D0=B0=D0=B5=
=D1=82 =D0=B0=D0=BA=D1=82=D1=83=D0=B0=D0=BB=D1=8C=D0=BD=D1=8B=D0=B5 =D0=B8=
=D1=81=D1=81=D0=BB=D0=B5=D0=B4=D0=BE=D0=B2=D0=B0=D0=BD=D0=B8=D1=8F, =D0=B0 =
=D0=B7=D0=B0=D1=82=D0=B5=D0=BC =D0=B2=D0=B8=D0=B7=D1=83=D0=B0=D0=BB=D0=B8=
=D0=B7=D0=B8=D1=80=D1=83=D0=B5=D1=82 =D0=B8=D0=BD=D1=84=D0=BE=D1=80=D0=BC=
=D0=B0=D1=86=D0=B8=D1=8E =D0=B2 =D0=B2=D0=B8=D0=B4=D0=B5 =D0=BA=D0=B0=D1=80=
=D1=82 =D0=BD=D0=B0=D1=83=D0=BA=D0=B8.<br>
<br>
<strong>=D0=A2=D1=80=D0=B5=D0=BD=D0=B8=D1=80=D0=BE=D0=B2=D0=BA=D0=B8 =D0=B4=
=D0=BE=D0=BC=D0=B0? =D0=9B=D0=B5=D0=B3=D0=BA=D0=BE!</strong><br>
PR-=D0=BC=D0=B5=D0=BD=D0=B5=D0=B4=D0=B6=D0=B5=D1=80 =D0=A1=D1=82=D1=83=D0=
=B4=D0=B5=D0=BD=D1=87=D0=B5=D1=81=D0=BA=D0=BE=D0=B3=D0=BE =D1=81=D0=BF=D0=
=BE=D1=80=D1=82=D0=B8=D0=B2=D0=BD=D0=BE=D0=B3=D0=BE =D0=BA=D0=BB=D1=83=D0=
=B1=D0=B0 =D0=B8 =D0=9A=D0=9C=D0=A1 =D0=BF=D0=BE =D1=84=D0=B5=D1=85=D1=82=
=D0=BE=D0=B2=D0=B0=D0=BD=D0=B8=D1=8E =D0=A1=D0=B2=D0=B5=D1=82=D0=BB=D0=B0=
=D0=BD=D0=B0 =D0=A5=D0=B0=D1=80=D0=B8=D1=82=D0=BE=D0=BD=D0=BE=D0=B2=D0=B0 <=
a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6476c4dsx1pfi4=
jnr33pasja89w17rz1ua7fpnpfm43usgmoqamb78zp6jafec3wdzyr96zi9ccbf6nsnyzrngg11=
eoknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLzM2=
MDkxMTI1NS5odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2N=
hbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,=
255);"">=D0=B4=D0=B5=D0=BB=D0=B8=D1=82=D1=81=D1=8F</a> =D1=81=D0=BE=D0=B2=D0=
=B5=D1=82=D0=B0=D0=BC=D0=B8 =D0=BE =D1=82=D0=BE=D0=BC, =D0=BA=D0=B0=D0=BA =
=D0=BC=D0=BE=D0=B6=D0=BD=D0=BE =D0=BE=D1=81=D0=BE=D0=B7=D0=BD=D0=B0=D0=BD=
=D0=BD=D0=BE =D0=BF=D0=BE=D0=B4=D0=BE=D0=B9=D1=82=D0=B8 =D0=BA =D0=B7=D0=B0=
=D0=BD=D1=8F=D1=82=D0=B8=D1=8F=D0=BC =D1=81=D0=BF=D0=BE=D1=80=D1=82=D0=BE=
=D0=BC. =D0=92 =D0=BC=D0=B0=D1=82=D0=B5=D1=80=D0=B8=D0=B0=D0=BB=D0=B5: =D0=
=BF=D0=BE=D0=BB=D0=B5=D0=B7=D0=BD=D1=8B=D0=B5 =D0=BF=D1=80=D0=B8=D0=BB=D0=
=BE=D0=B6=D0=B5=D0=BD=D0=B8=D1=8F, =D0=BB=D0=B0=D0=B9=D1=84=D1=85=D0=B0=D0=
=BA=D0=B8 =D0=B8 =D0=B2=D0=B8=D0=B4=D0=B5=D0=BE.<br>
<strong>&nbsp;<br>
=D0=A2=D0=B5=D1=81=D1=82: =D0=BA=D1=82=D0=BE =D0=B2=D1=8B =D0=BD=D0=B0 =D0=
=BC=D0=B0=D0=B9=D1=81=D0=BA=D0=B8=D1=85?</strong><br>
STUDLIFE HSE =D1=81=D0=B4=D0=B5=D0=BB=D0=B0=D0=BB =D0=B7=D0=B0=D0=B1=D0=B0=
=D0=B2=D0=BD=D1=8B=D0=B9 <a href=3D""https://unimail.hse.ru/ru/mail_link_tra=
cker?hash=3D6oc94s4kgbbrohjnr33pasja89w17rz1ua7fpnpfm43usgmoqambo6gdf8pj47m=
7ber51grfmbkxbxuxfk9zpdzjsnaknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly92ay=
5jb20vc3R1ZGxpZmVfaHNlP3c9d2FsbC01OTIwODQ4OF85MzYyJnV0bV9tZWRpdW09ZW1haWwmd=
XRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3N=
A=3D=3D"" style=3D""color:rgb(0,127,255);"">=D1=82=D0=B5=D1=81=D1=82</a> =D1=
=81 =D1=82=D0=BE=D1=82=D0=B5=D0=BC=D0=BD=D1=8B=D0=BC=D0=B8 =D0=B6=D0=B8=D0=
=B2=D0=BE=D1=82=D0=BD=D1=8B=D0=BC=D0=B8. =D0=A3=D0=B7=D0=BD=D0=B0=D0=B9=D1=
=82=D0=B5, =D0=BA=D1=82=D0=BE =D0=B2=D1=8B: =D0=B7=D0=B0=D0=BD=D1=8F=D1=82=
=D0=B0=D1=8F =D0=BF=D1=87=D0=B5=D0=BB=D0=BA=D0=B0. =D1=81=D0=BE=D1=86=D0=B8=
=D0=B0=D0=BB=D1=8C=D0=BD=D1=8B=D0=B9 =D0=BF=D0=BE=D0=BF=D1=83=D0=B3 =D0=B8=
=D0=BB=D0=B8 =D1=81=D0=B5=D1=80=D0=B8=D0=B0=D0=BB=D1=8C=D0=BD=D1=8B=D0=B9 =
=D0=BF=D0=B0=D1=83=D0=BA.</span></span></span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block line-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; height: 100%; vertical-al=
ign: middle; min-height: auto; font-size: medium;"" class=3D""block-wrapper"" =
valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 10px; width: 100%; table-layout: fixed; b=
order-spacing: 0px; border-collapse: collapse; min-height: 10px;"">
<tbody>
<tr>
<td style=3D""width: 100%; vertical-align: middle; height: 10px; min-height:=
 10px;"" class=3D""content-wrapper"">
<table border=3D""0"" cellspacing=3D""0"" cellpadding=3D""0"" style=3D""width: 100=
%; table-layout: fixed; border-spacing: 0; border-collapse: collapse; font-=
size: 0;"">
<tbody>
<tr>
<td class=3D""separator-line"" style=3D""width: 100%; background-color: rgb(25=
5, 255, 255); height: 1px; min-height: 1px; max-height: 1px; line-height: 1=
px;"">&nbsp;</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block image-block"" width=3D""100%"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heig=
ht: auto; border-collapse: collapse; border-spacing: 0px; display: inline-t=
able; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; padding: 0px; height: 100=
%;"" class=3D""block-wrapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 200px; width: 100%; table-layout: fixed; =
text-align: center; border-spacing: 0px; border-collapse: collapse; font-si=
ze: 0px;"">
<tbody>
<tr>
<td style=3D""width: auto; height: 100%; display: inline-table;"" class=3D""co=
ntent-wrapper"">
<table class=3D""content-box"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""=
0"" style=3D""display: inline-table; vertical-align: top; width: auto; height=
: 100%; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""vertical-align: middle;"">
<div class=3D""image-wrapper image-drop""><a class=3D""image-link"" href=3D""jav=
ascript:;"" target=3D""_self""><img class=3D""image-element"" src=3D""http://unim=
ail.hse.ru/ru/user_file?resource=3Dhimg&user_id=3D1323674&name=3D6ff4b61a3t=
nr79piu3azzngxco47cfqz5xza57gs5u5ro3d9bmjkxt7ktpsx7aui4zuku35zh4nnbc"" alt=
=3D""Some Image"" id=3D""gridster_block_449_main_img"" style=3D""font-size: smal=
l; border: none; width: 100%; max-width: 600px; height: auto; max-height: 2=
00px; outline: none; text-decoration: none;"" width=3D""600""></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 1506.67px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px; vertical-align: top; font-siz=
e: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; colo=
r: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;"">
<p><span style=3D""line-height:1.5;""><span style=3D""font-size:16px;""><span s=
tyle=3D""font-family:Arial, Helvetica, sans-serif;""><span style=3D""backgroun=
d-color:#ffffcc;"">6-8 =D0=BC=D0=B0=D1=8F</span><br>
<strong>Mental Health Spring</strong><br>
=D0=9D=D0=B0 =D1=8D=D1=82=D0=BE=D0=B9 =D0=BA=D0=BE=D1=80=D0=BE=D1=82=D0=BA=
=D0=BE=D0=B9 =D0=BF=D1=80=D0=B0=D0=B7=D0=B4=D0=BD=D0=B8=D1=87=D0=BD=D0=BE=
=D0=B9 =D0=BD=D0=B5=D0=B4=D0=B5=D0=BB=D0=B5 =D0=A6=D0=B5=D0=BD=D1=82=D1=80 =
=D0=BF=D1=81=D0=B8=D1=85=D0=BE=D0=BB=D0=BE=D0=B3=D0=B8=D1=87=D0=B5=D1=81=D0=
=BA=D0=BE=D0=B3=D0=BE =D0=BA=D0=BE=D0=BD=D1=81=D1=83=D0=BB=D1=8C=D1=82=D0=
=B8=D1=80=D0=BE=D0=B2=D0=B0=D0=BD=D0=B8=D1=8F =D0=BF=D1=80=D0=BE=D0=B2=D0=
=B5=D0=B4=D0=B5=D1=82 =D1=82=D1=80=D0=B0=D0=B4=D0=B8=D1=86=D0=B8=D0=BE=D0=
=BD=D0=BD=D1=83=D1=8E =D0=BC=D0=B5=D0=B4=D0=B8=D1=82=D0=B0=D1=86=D0=B8=D1=
=8E =D0=BE=D1=81=D0=BE=D0=B7=D0=BD=D0=B0=D0=BD=D0=BD=D0=BE=D1=81=D1=82=D0=
=B8, =D0=BB=D0=B5=D0=BA=D1=86=D0=B8=D0=B8 =D0=BE =D1=82=D0=BE=D0=BC, =D0=BA=
=D0=B0=D0=BA =D0=B8=D1=81=D0=BF=D0=BE=D0=BB=D1=8C=D0=B7=D0=BE=D0=B2=D0=B0=
=D1=82=D1=8C =D0=BD=D0=B0=D1=88=D1=83 =D0=BF=D0=B0=D0=BC=D1=8F=D1=82=D1=8C =
=D0=B2 =D0=BA=D0=B0=D1=87=D0=B5=D1=81=D1=82=D0=B2=D0=B5 =D1=80=D0=B5=D1=81=
=D1=83=D1=80=D1=81=D0=B0 =D0=B8 =D0=BA=D0=B0=D0=BA=D0=B8=D0=B5 =D1=81=D0=BE=
=D1=86=D0=B8=D0=B0=D0=BB=D1=8C=D0=BD=D1=8B=D0=B5 =D0=BA=D0=BE=D0=BD=D1=82=
=D0=B0=D0=BA=D1=82=D1=8B =D0=BD=D0=B0=D0=BC =D0=BD=D1=83=D0=B6=D0=BD=D1=8B,=
 =D1=87=D1=82=D0=BE=D0=B1=D1=8B =D0=B1=D1=8B=D1=82=D1=8C =D1=81=D1=87=D0=B0=
=D1=81=D1=82=D0=BB=D0=B8=D0=B2=D1=8B=D0=BC=D0=B8, =D0=BE=D0=BD=D0=BB=D0=B0=
=D0=B9=D0=BD-=D0=B2=D1=81=D1=82=D1=80=D0=B5=D1=87=D1=83 =D1=81 =D1=85=D1=83=
=D0=B4=D0=BE=D0=B6=D0=BD=D0=B8=D1=86=D0=B5=D0=B9 =D0=B8 =D0=B0=D0=B2=D1=82=
=D0=BE=D1=80=D0=BE=D0=BC =C2=AB=D0=9A=D0=BD=D0=B8=D0=B3=D0=B8 =D0=BE =D0=94=
=D0=B5=D0=BF=D1=80=D0=B5=D1=81=D1=81=D0=B8=D0=B8=C2=BB =D0=A1=D0=B0=D1=88=
=D0=B5=D0=B9 =D0=A1=D0=BA=D0=BE=D1=87=D0=B8=D0=BB=D0=B5=D0=BD=D0=BA=D0=BE, =
=D0=B0 =D1=82=D0=B0=D0=BA=D0=B6=D0=B5 =C2=AB=D0=B6=D0=B8=D0=B2=D1=83=D1=8E =
=D0=B1=D0=B5=D1=81=D0=B5=D0=B4=D1=83=C2=BB =D1=81=D0=BE =D1=81=D1=82=D1=83=
=D0=B4=D0=B5=D0=BD=D1=82=D0=B0=D0=BC=D0=B8 =D0=BE =D0=B2=D0=BB=D0=B8=D1=8F=
=D0=BD=D0=B8=D0=B8 =D1=80=D0=BE=D0=B4=D0=B8=D1=82=D0=B5=D0=BB=D0=B5=D0=B9 =
=D0=BD=D0=B0 =D0=B8=D1=85 =D0=B6=D0=B8=D0=B7=D0=BD=D1=8C.<br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6w9k6jmsaas8j=
4jnr33pasja89w17rz1ua7fpnpfm43usgmoqamb1w3r6yd6jspuo3u3j4zn8zeo8o5bxs7u9pce=
m3wknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci9uZXdzLzM=
2MjcwMzgxNi5odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2=
NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127=
,255);"">=D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5</a><br>
<br>
<span style=3D""background-color:#ffffcc;"">6-8 =D0=BC=D0=B0=D1=8F</span><br>
<strong>Home Show =D0=BE=D1=82 XCE Factor</strong><br>
=D0=92=D1=81=D1=82=D1=80=D0=B5=D1=87=D0=B0=D0=B9=D1=82=D0=B5 =D0=BF=D0=B5=
=D1=80=D0=B2=D0=BE=D0=B5 digital-=D1=88=D0=BE=D1=83 =D1=82=D0=B0=D0=BB=D0=
=B0=D0=BD=D1=82=D0=BE=D0=B2! =D0=9A=D0=B0=D0=B6=D0=B4=D1=8B=D0=B9 =D0=B2=D0=
=B5=D1=87=D0=B5=D1=80 =D0=B2 20:00 =D0=B2=D0=B0=D1=81 =D0=B6=D0=B4=D1=83=D1=
=82 live-=D0=BA=D0=BE=D0=BD=D1=86=D0=B5=D1=80=D1=82=D1=8B =D1=81 =D0=B0=D1=
=80=D1=82=D0=B8=D1=81=D1=82=D0=B0=D0=BC=D0=B8 =D0=B8=D0=B7 =D0=92=D1=8B=D1=
=88=D0=BA=D0=B8. =D0=92 =D0=BF=D1=80=D1=8F=D0=BC=D0=BE=D0=BC =D1=8D=D1=84=
=D0=B8=D1=80=D0=B5 =D0=B2=D1=8B =D1=83=D1=81=D0=BB=D1=8B=D1=88=D0=B8=D1=82=
=D0=B5 =D0=BA=D0=B0=D0=B2=D0=B5=D1=80=D1=8B =D0=BD=D0=B0 =D0=B8=D0=B7=D0=B2=
=D0=B5=D1=81=D1=82=D0=BD=D1=8B=D0=B5 =D0=BA=D0=BE=D0=BC=D0=BF=D0=BE=D0=B7=
=D0=B8=D1=86=D0=B8=D0=B8, =D0=B0 =D1=82=D0=B0=D0=BA=D0=B6=D0=B5 =D0=B0=D0=
=B2=D1=82=D0=BE=D1=80=D1=81=D0=BA=D0=B8=D0=B5 =D1=82=D1=80=D0=B5=D0=BA=D0=
=B8. =D0=95=D1=81=D0=BB=D0=B8 =D0=B2=D1=8B =D1=82=D0=BE=D0=B6=D0=B5 =D1=85=
=D0=BE=D1=82=D0=B8=D1=82=D0=B5 =D0=B2=D1=8B=D1=81=D1=82=D1=83=D0=BF=D0=B8=
=D1=82=D1=8C =D0=BD=D0=B0 =D0=B0=D1=83=D0=B4=D0=B8=D1=82=D0=BE=D1=80=D0=B8=
=D1=8E =D0=B4=D0=BE 100 000 =D0=B7=D1=80=D0=B8=D1=82=D0=B5=D0=BB=D0=B5=D0=
=B9, =D0=BF=D0=B8=D1=88=D0=B8=D1=82=D0=B5 =D0=B2 =D0=BB=D0=B8=D1=87=D0=BD=
=D1=8B=D0=B5 =D1=81=D0=BE=D0=BE=D0=B1=D1=89=D0=B5=D0=BD=D0=B8=D1=8F =D0=B3=
=D1=80=D1=83=D0=BF=D0=BF=D1=8B, =D0=B8 =D1=80=D0=B5=D0=B1=D1=8F=D1=82=D0=B0=
 =D0=B2=D1=81=D0=B5 =D0=BE=D1=80=D0=B3=D0=B0=D0=BD=D0=B8=D0=B7=D1=83=D1=8E=
=D1=82. =D0=90=D0=BA=D1=82=D1=83=D0=B0=D0=BB=D1=8C=D0=BD=D0=BE=D0=B5 =D1=80=
=D0=B0=D1=81=D0=BF=D0=B8=D1=81=D0=B0=D0=BD=D0=B8=D0=B5 =D0=BA=D0=BE=D0=BD=
=D1=86=D0=B5=D1=80=D1=82=D0=BE=D0=B2 =D0=B2 VK.<br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6rm5kfnuhaney=
ojnr33pasja89w17rz1ua7fpnpfm43usgmoqambhasdc8f6nhga1py65etkc347m8iymni3oot4=
gwcknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly92ay5jb20veGNlZmFjdG9yMjAyMD9=
1dG1fbWVkaXVtPWVtYWlsJnV0bV9zb3VyY2U9VW5pU2VuZGVyJnV0bV9jYW1wYWlnbj0yMzI4Nz=
gyMzI~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=9F=D0=BE=
=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5</a><br>
<br>
<span style=3D""background-color:#ffffcc;"">7 =D0=BC=D0=B0=D1=8F, 19:00</span=
><br>
<strong>=D0=92=D0=B5=D0=B1=D0=B8=D0=BD=D0=B0=D1=80 =C2=AB=D0=A7=D1=82=D0=BE=
 =D0=BE=D1=86=D0=B5=D0=BD=D0=B8=D0=B2=D0=B0=D1=8E=D1=82 HR? =D0=A1=D0=B5=D0=
=BA=D1=80=D0=B5=D1=82=D1=8B =D0=B8=D0=BD=D1=82=D0=B5=D1=80=D0=B2=D1=8C=D1=
=8E=C2=BB</strong><br>
=D0=92=D0=BC=D0=B5=D1=81=D1=82=D0=B5 =D1=81 =D0=9A=D1=81=D0=B5=D0=BD=D0=B8=
=D0=B5=D0=B9 =D0=9F=D0=B5=D1=82=D1=80=D1=83=D1=85=D0=B8=D0=BD=D0=BE=D0=B9, =
=D0=B2=D1=8B=D0=BF=D1=83=D1=81=D0=BA=D0=BD=D0=B8=D1=86=D0=B5=D0=B9 =D1=84=
=D0=B0=D0=BA=D1=83=D0=BB=D1=8C=D1=82=D0=B5=D1=82=D0=B0 =D1=8D=D0=BA=D0=BE=
=D0=BD=D0=BE=D0=BC=D0=B8=D0=BA=D0=B8 =D0=92=D1=8B=D1=88=D0=BA=D0=B8 =D0=B8 =
HR-=D1=81=D0=BF=D0=B5=D1=86=D0=B8=D0=B0=D0=BB=D0=B8=D1=81=D1=82=D0=BE=D0=BC=
 =D0=BA=D1=80=D1=83=D0=BF=D0=BD=D0=BE=D0=B3=D0=BE =D0=BC=D0=B5=D0=B6=D0=B4=
=D1=83=D0=BD=D0=B0=D1=80=D0=BE=D0=B4=D0=BD=D0=BE=D0=B3=D0=BE =D1=85=D0=BE=
=D0=BB=D0=B4=D0=B8=D0=BD=D0=B3=D0=B0, =D0=BE=D0=B1=D1=81=D1=83=D0=B4=D0=B8=
=D0=BC, =D0=BA=D0=B0=D0=BA=D0=B8=D0=B5 =D0=BA=D1=80=D0=B8=D1=82=D0=B5=D1=80=
=D0=B8=D0=B8 =D0=B2=D0=B0=D0=B6=D0=BD=D1=8B =D0=B8=D0=BC=D0=B5=D0=BD=D0=BD=
=D0=BE =D0=B4=D0=BB=D1=8F =D0=B2=D0=B0=D1=81 =D0=B2 =D0=BF=D0=BE=D0=B8=D1=
=81=D0=BA=D0=B5 =D1=80=D0=B0=D0=B1=D0=BE=D1=82=D1=8B, =D0=BA=D0=B0=D0=BA =
=D0=BF=D0=BE=D0=BD=D1=8F=D1=82=D1=8C, =D0=BA=D0=B0=D0=BA=D0=B8=D0=B5 =D0=B2=
=D0=B0=D1=88=D0=B8 =D0=BB=D0=B8=D1=87=D0=BD=D0=BE=D1=81=D1=82=D0=BD=D1=8B=
=D0=B5 =D0=BA=D0=B0=D1=87=D0=B5=D1=81=D1=82=D0=B2=D0=B0 =D0=B8 =D1=81=D0=B8=
=D0=BB=D1=8C=D0=BD=D1=8B=D0=B5 =D1=81=D1=82=D0=BE=D1=80=D0=BE=D0=BD=D1=8B =
=D0=B1=D1=83=D0=B4=D1=83=D1=82 =D0=BF=D0=BE=D0=BB=D0=B5=D0=B7=D0=BD=D1=8B =
=D0=B8 =D1=86=D0=B5=D0=BD=D0=BD=D1=8B =D0=BA=D0=BE=D0=BC=D0=BF=D0=B0=D0=BD=
=D0=B8=D0=B8 =D0=B8 =D0=BF=D0=BE=D1=87=D0=B5=D0=BC=D1=83 =D0=BD=D0=B5=D1=82=
 =D1=81=D0=BC=D1=8B=D1=81=D0=BB=D0=B0 =D0=BF=D1=80=D0=B8=D1=82=D0=B2=D0=BE=
=D1=80=D1=8F=D1=82=D1=8C=D1=81=D1=8F =D0=B8 =D0=B3=D0=BE=D1=82=D0=BE=D0=B2=
=D0=B8=D1=82=D1=8C=D1=81=D1=8F =D0=BA =D0=B8=D0=BD=D1=82=D0=B5=D1=80=D0=B2=
=D1=8C=D1=8E =D0=B7=D0=B0=D1=80=D0=B0=D0=BD=D0=B5=D0=B5.<br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6qmuun11wxp76=
ejnr33pasja89w17rz1ua7fpnpfm43usgmoqambat7aj8j9srp1wt7zp4c9i9zp4sdowuby7eb1=
cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly92ay5jb20vYXBwNTg5ODE4Ml8tMTg=
1NTUwNzQ2P3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaW=
duPTIzMjg3ODIzMiNzPTc4NTg4OA~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0=
,127,255);"">=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=
=8F</a><br>
<br>
<span style=3D""background-color:#ffffcc;"">8 =D0=BC=D0=B0=D1=8F, 19:00</span=
><br>
<strong>=D0=9F=D1=80=D0=B0=D0=BA=D1=82=D0=B8=D0=BA=D0=B0 =D0=B8=D1=82=D0=B0=
=D0=BB=D1=8C=D1=8F=D0=BD=D1=81=D0=BA=D0=BE=D0=B3=D0=BE =D1=81 =D0=BA=D0=BB=
=D1=83=D0=B1=D0=BE=D0=BC Poboltaem?</strong><br>
=D0=9F=D0=BE=D0=BA=D0=B0 =D0=BE =D0=BF=D0=BE=D0=B5=D0=B7=D0=B4=D0=BA=D0=B0=
=D1=85 =D0=B2 =D1=81=D0=BE=D0=BB=D0=BD=D0=B5=D1=87=D0=BD=D1=83=D1=8E =D0=98=
=D1=82=D0=B0=D0=BB=D0=B8=D1=8E =D0=BC=D0=BE=D0=B6=D0=BD=D0=BE =D1=82=D0=BE=
=D0=BB=D1=8C=D0=BA=D0=BE =D0=BC=D0=B5=D1=87=D1=82=D0=B0=D1=82=D1=8C, =D0=BF=
=D0=BE=D0=B3=D0=BE=D0=B2=D0=BE=D1=80=D0=B8=D0=BC =D0=BE =D0=BB=D1=8E=D0=B1=
=D0=B8=D0=BC=D1=8B=D1=85 =D0=BC=D0=B5=D1=81=D1=82=D0=B0=D1=85 =D0=B8 =D1=80=
=D0=B5=D0=B3=D0=B8=D0=BE=D0=BD=D0=B0=D1=85 =D1=8D=D1=82=D0=BE=D0=B9 =D0=BD=
=D0=B5=D0=B2=D0=B5=D1=80=D0=BE=D1=8F=D1=82=D0=BD=D0=BE =D0=BA=D1=80=D0=B0=
=D1=81=D0=B8=D0=B2=D0=BE=D0=B9 =D1=81=D1=82=D1=80=D0=B0=D0=BD=D1=8B. =D0=A1=
=D1=81=D1=8B=D0=BB=D0=BA=D0=B0 =D0=BD=D0=B0 =D0=BA=D0=BE=D0=BD=D1=84=D0=B5=
=D1=80=D0=B5=D0=BD=D1=86=D0=B8=D1=8E =D0=B2 Zoom =D0=BF=D0=BE=D1=8F=D0=B2=
=D0=B8=D1=82=D1=81=D1=8F =D0=B2 =D0=B3=D1=80=D1=83=D0=BF=D0=BF=D0=B5 =D0=BF=
=D0=B5=D1=80=D0=B5=D0=B4 =D0=BD=D0=B0=D1=87=D0=B0=D0=BB=D0=BE=D0=BC =D0=B2=
=D1=81=D1=82=D1=80=D0=B5=D1=87=D0=B8.&nbsp;<br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6m4ecixotsmtu=
hjnr33pasja89w17rz1ua7fpnpfm43usgmoqamb6igru7733gguyhwxtjzgxutjr8cfzqrh53f9=
cfwknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly92ay5jb20vcG9ib2x0YWVtaHNlP3V=
0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzMjg3OD=
IzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=9F=D0=BE=
=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5</a><br>
<br>
<span style=3D""background-color:#ffffcc;"">8 =D0=BC=D0=B0=D1=8F, 19:00</span=
><br>
<strong>=D0=9B=D0=B5=D0=BA=D1=86=D0=B8=D1=8F =C2=AB=D0=9E =D0=B7=D0=B0=D0=
=B3=D0=B0=D0=B4=D0=BA=D0=B5 =D0=BE=D0=B4=D0=BD=D0=BE=D0=B3=D0=BE =D1=81=D1=
=82=D0=B8=D1=85=D0=BE=D1=82=D0=B2=D0=BE=D1=80=D0=B5=D0=BD=D0=B8=D1=8F =D0=
=9F=D0=B0=D1=81=D1=82=D0=B5=D1=80=D0=BD=D0=B0=D0=BA=D0=B0=C2=BB</strong><br=
>
=D0=A1=D1=82=D0=B8=D1=85=D0=BE=D1=82=D0=B2=D0=BE=D1=80=D0=B5=D0=BD=D0=B8=D0=
=B5 =C2=AB=D0=A1=D0=BA=D0=B0=D0=B7=D0=BA=D0=B0=C2=BB =D0=B1=D1=8B=D0=BB=D0=
=BE =D0=BD=D0=B0=D0=BF=D0=B8=D1=81=D0=B0=D0=BD=D0=BE =D0=B2 1953 =D0=B3=D0=
=BE=D0=B4=D1=83 =D0=B8 =D0=B2=D0=BE=D1=88=D0=BB=D0=BE =D0=B2 =D1=81=D0=BE=
=D1=81=D1=82=D0=B0=D0=B2 =C2=AB=D0=A1=D1=82=D0=B8=D1=85=D0=BE=D1=82=D0=B2=
=D0=BE=D1=80=D0=B5=D0=BD=D0=B8=D0=B9 =D0=AE=D1=80=D0=B8=D1=8F =D0=96=D0=B8=
=D0=B2=D0=B0=D0=B3=D0=BE=C2=BB. =D0=92 =D1=80=D0=BE=D0=BC=D0=B0=D0=BD=D0=B5=
 =D0=9F=D0=B0=D1=81=D1=82=D0=B5=D1=80=D0=BD=D0=B0=D0=BA =D0=BF=D0=BE=D0=B4=
=D1=80=D0=BE=D0=B1=D0=BD=D0=BE =D0=BE=D0=BF=D0=B8=D1=81=D1=8B=D0=B2=D0=B0=
=D0=B5=D1=82 =D0=B8=D1=81=D1=82=D0=BE=D1=80=D0=B8=D1=8E =D1=81=D0=BE=D0=B7=
=D0=B4=D0=B0=D0=BD=D0=B8=D1=8F =D1=8D=D1=82=D0=BE=D0=B3=D0=BE =D1=81=D1=82=
=D0=B8=D1=85=D0=BE=D1=82=D0=B2=D0=BE=D1=80=D0=B5=D0=BD=D0=B8=D1=8F, =D0=BD=
=D0=BE =D0=BE=D0=BD=D0=B0 =D0=BD=D0=B5 =D1=81=D0=BE=D0=B2=D0=BF=D0=B0=D0=B4=
=D0=B0=D0=B5=D1=82 =D1=81 =D1=80=D0=B5=D0=B0=D0=BB=D1=8C=D0=BD=D0=BE=D0=B9.=
 =D0=97=D0=B0=D1=87=D0=B5=D0=BC =D0=9F=D0=B0=D1=81=D1=82=D0=B5=D1=80=D0=BD=
=D0=B0=D0=BA=D1=83 =D0=BF=D0=BE=D0=BD=D0=B0=D0=B4=D0=BE=D0=B1=D0=B8=D0=BB=
=D0=B0=D1=81=D1=8C =D0=B2=D1=8B=D0=BC=D1=8B=D1=88=D0=BB=D0=B5=D0=BD=D0=BD=
=D0=B0=D1=8F =D0=B8=D1=81=D1=82=D0=BE=D1=80=D0=B8=D1=8F =D1=81=D0=BE=D0=B7=
=D0=B4=D0=B0=D0=BD=D0=B8=D1=8F =D1=81=D1=82=D0=B8=D1=85=D0=BE=D1=82=D0=B2=
=D0=BE=D1=80=D0=B5=D0=BD=D0=B8=D1=8F? =D0=9A=D0=B0=D0=BA =D0=BE=D0=BD=D0=B0=
 =D0=BF=D1=80=D0=BE=D1=8F=D1=81=D0=BD=D1=8F=D0=B5=D1=82 =D1=81=D0=BC=D1=8B=
=D1=81=D0=BB =D0=BF=D0=BE=D1=8D=D1=82=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=BE=
=D0=B3=D0=BE =D1=82=D0=B5=D0=BA=D1=81=D1=82=D0=B0 =D0=B8 =D1=80=D0=BE=D0=BC=
=D0=B0=D0=BD=D0=B0 =D0=B2 =D1=86=D0=B5=D0=BB=D0=BE=D0=BC? =D0=A3=D0=B7=D0=
=BD=D0=B0=D0=B5=D0=BC =D0=B8=D0=B7 =D0=BB=D0=B5=D0=BA=D1=86=D0=B8=D0=B8 =D0=
=9C=D0=B0=D1=80=D0=B8=D0=B8 =D0=93=D0=B5=D0=BB=D1=8C=D1=84=D0=BE=D0=BD=D0=
=B4.<br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6mx8ze86hhhgh=
yjnr33pasja89w17rz1ua7fpnpfm43usgmoqamb3gunxdyizkpjxea8ajku5nujcb6ardjf311q=
9raknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L2xlY3Rvcmlhbi9=
jYW1wdXNvbmxpbmU_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2=
FtcGFpZ249MjMyODc4MjMy&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255=
);"">=D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5</a><br>
<br>
<span style=3D""background-color:#ffffcc;"">10 =D0=BC=D0=B0=D1=8F, 16:00</spa=
n><br>
<strong>=D0=9E=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D0=BD=D0=B0=D1=81=D1=82=D0=BE=
=D0=BB=D0=BA=D0=B8</strong><br>
=D0=9A=D0=BB=D1=83=D0=B1 =D0=BD=D0=B0=D1=81=D1=82=D0=BE=D0=BB=D1=8C=D0=BD=
=D1=8B=D1=85 =D0=B8=D0=B3=D1=80 =C2=AB=D0=9A=D0=BE=D0=BD=D1=82=D0=B0=D0=BA=
=D1=82=C2=BB =D1=81=D0=BE=D0=B2=D0=BC=D0=B5=D1=81=D1=82=D0=BD=D0=BE =D1=81=
=D0=BE =D1=81=D1=82=D1=83=D0=B4=D0=B0=D0=BA=D1=82=D0=B8=D0=B2=D0=BE=D0=BC =
=D1=84=D0=B0=D0=BA=D1=83=D0=BB=D1=8C=D1=82=D0=B5=D1=82=D0=B0 =D0=BF=D1=80=
=D0=B0=D0=B2=D0=B0 =D0=BF=D1=80=D0=BE=D0=B2=D0=B5=D0=B4=D0=B5=D1=82 =D0=BE=
=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D0=B2=D0=B5=D1=87=D0=B5=D1=80 =D0=B8=D0=B3=
=D1=80. =D0=92 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC=D0=B5: Code=
names, Spyfall, Jackbox, =D0=A1=D0=B2=D0=BE=D1=8F =D0=98=D0=B3=D1=80=D0=B0 =
=D0=B8 =D0=BC=D0=BD=D0=BE=D0=B3=D0=BE=D0=B5 =D0=B4=D1=80=D1=83=D0=B3=D0=BE=
=D0=B5.<br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6tpa96m1s1g19=
ojnr33pasja89w17rz1ua7fpnpfm43usgmoqambwgu65ninajangfnffc3y5xzxa8uxfk9zpdzj=
snaknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly92ay5jb20vaHNlZ2c_dXRtX21lZGl=
1bT1lbWFpbCZ1dG1fc291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjMyODc4MjMy&uid=
=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=9F=D0=BE=D0=B4=D1=
=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5</a><br>
<br>
<span style=3D""background-color:#ffffcc;"">11 =D0=BC=D0=B0=D1=8F, 15:00</spa=
n><br>
<strong>=D0=9E=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D0=BC=D0=B0=D1=80=D0=B0=D1=84=
=D0=BE=D0=BD =C2=AB=D0=A1=D0=B5=D0=BC=D1=8C=D1=8F =D0=BD=D0=B0 =D1=81=D0=B0=
=D0=BC=D0=BE=D0=B8=D0=B7=D0=BE=D0=BB=D1=8F=D1=86=D0=B8=D0=B8=C2=BB</strong>=
<br>
=D0=9A=D0=B0=D0=BA =D0=B2 =D1=83=D1=81=D0=BB=D0=BE=D0=B2=D0=B8=D1=8F=D1=85 =
=D1=81=D0=B0=D0=BC=D0=BE=D0=B8=D0=B7=D0=BE=D0=BB=D1=8F=D1=86=D0=B8=D0=B8 =
=D1=81=D0=BE=D1=85=D1=80=D0=B0=D0=BD=D0=B8=D1=82=D1=8C =D0=B7=D0=B4=D0=BE=
=D1=80=D0=BE=D0=B2=D1=8B=D0=B5 =D0=BE=D1=82=D0=BD=D0=BE=D1=88=D0=B5=D0=BD=
=D0=B8=D1=8F =D0=B2 =D1=81=D0=B5=D0=BC=D1=8C=D0=B5? =D0=9F=D1=80=D0=B5=D0=
=BF=D0=BE=D0=B4=D0=B0=D0=B2=D0=B0=D1=82=D0=B5=D0=BB=D0=B8 =D0=BC=D0=B0=D0=
=B3=D0=B8=D1=81=D1=82=D0=B5=D1=80=D1=81=D0=BA=D0=BE=D0=B9 =D0=BF=D1=80=D0=
=BE=D0=B3=D1=80=D0=B0=D0=BC=D0=BC=D1=8B =C2=AB=D0=A1=D0=B8=D1=81=D1=82=D0=
=B5=D0=BC=D0=BD=D0=B0=D1=8F =D1=81=D0=B5=D0=BC=D0=B5=D0=B9=D0=BD=D0=B0=D1=
=8F =D0=BF=D1=81=D0=B8=D1=85=D0=BE=D1=82=D0=B5=D1=80=D0=B0=D0=BF=D0=B8=D1=
=8F=C2=BB =D0=BF=D1=80=D0=BE=D0=B2=D0=B5=D0=B4=D1=83=D1=82 =D0=BB=D0=B5=D0=
=BA=D1=86=D0=B8=D0=B8 =D0=BE =D0=B4=D0=B5=D1=82=D1=8F=D1=85, =D0=B4=D0=BE=
=D0=BC=D0=B0=D1=88=D0=BD=D0=B5=D0=BC =D0=BD=D0=B0=D1=81=D0=B8=D0=BB=D0=B8=
=D0=B8, =D0=BF=D0=B8=D1=82=D0=BE=D0=BC=D1=86=D0=B0=D1=85 =D0=B8 =D0=BE=D1=
=82=D0=B2=D0=B5=D1=82=D1=8F=D1=82 =D0=BD=D0=B0 =D0=B2=D0=BE=D0=BF=D1=80=D0=
=BE=D1=81=D1=8B =D1=81=D0=BB=D1=83=D1=88=D0=B0=D1=82=D0=B5=D0=BB=D0=B5=D0=
=B9 =D0=B2 =D0=BF=D1=80=D1=8F=D0=BC=D0=BE=D0=BC =D1=8D=D1=84=D0=B8=D1=80=D0=
=B5.<br>
<a href=3D""https://unimail.hse.ru/ru/mail_link_tracker?hash=3D6ywi3fww4neym=
yjnr33pasja89w17rz1ua7fpnpfm43usgmoqamb1d69h89y4ur7jt384bykygg1x1dowuby7eb1=
cf5ge75tbx3h5j71sywopdfiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L2xlY3Rvcmlhbi9=
hbm5vdW5jZW1lbnRzLzM2MjU0MTIxNS5odG1sP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT=
1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" styl=
e=3D""color:rgb(0,127,255);"">=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=
=B0=D1=86=D0=B8=D1=8F</a></span></span></span><br>
&nbsp;</p>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block image-block"" width=3D""100%"" border=3D""0"" cellspac=
ing=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heig=
ht: auto; border-collapse: collapse; border-spacing: 0px; display: inline-t=
able; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-image: none; padding: 5px 10px 20px 20=
px; height: 100%;"" class=3D""block-wrapper"" valign=3D""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 215.99px; width: 100%; table-layout: fixe=
d; text-align: center; border-spacing: 0px; border-collapse: collapse; font=
-size: 0px;"">
<tbody>
<tr>
<td style=3D""width: auto; height: 100%; display: inline-table;"" class=3D""co=
ntent-wrapper"">
<table class=3D""content-box"" border=3D""0"" cellspacing=3D""0"" cellpadding=3D""=
0"" style=3D""display: inline-table; vertical-align: top; width: auto; height=
: 100%; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""vertical-align: middle;"">
<div class=3D""image-wrapper image-drop""><a class=3D""image-link"" href=3D""jav=
ascript:;"" target=3D""_self""><img class=3D""image-element"" src=3D""http://unim=
ail.hse.ru/ru/user_file?resource=3Dhimg&user_id=3D1323674&name=3D6o1rs6w691=
pmsbpiu3azzngxco47cfqz5xza57gwjjoyscp431kw7fed9u1jd8dyaa86w81czkc88o"" alt=
=3D""Some Image"" id=3D""gridster_block_454_main_img"" style=3D""font-size: smal=
l; border: none; width: 100%; max-width: 570px; height: auto; max-height: 1=
90px; outline: none; text-decoration: none;"" width=3D""570""></a></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 0px; width: 100%; table-layout: fixed; bo=
rder-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px; vertical-align: top; font-siz=
e: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; colo=
r: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix cke_editable cke_editable_inline cke_contents_ltr ck=
e_show_borders"" style=3D""overflow-wrap: break-word; position: relative;"" ta=
bindex=3D""0"" spellcheck=3D""false"" role=3D""textbox"" aria-label=3D""false"" ari=
a-describedby=3D""cke_45"">
<p><span style=3D""font-size:16px""><span style=3D""font-family:Arial,Helvetic=
a,sans-serif""><span style=3D""line-height:1.5""><span style=3D""background-col=
or:#ffffcc"">7 =D0=BC=D0=B0=D1=8F, 17:00</span><br>
<strong>=D0=9B=D0=B5=D0=BA=D1=82=D0=BE=D1=80=D0=B8=D0=B9 =D0=A0=D0=AD=D0=A8=
 =C2=AB=D0=A1=D1=82=D0=B8=D0=BC=D1=83=D0=BB=D1=8B: =D0=BF=D0=B0=D0=BD=D0=B8=
=D0=BA=D0=B0, =D0=B3=D1=80=D0=B5=D1=87=D0=BA=D0=B0 =D0=B8 =D1=8D=D1=84=D1=
=84=D0=B5=D0=BA=D1=82 =D0=BA=D0=BE=D0=B1=D1=80=D1=8B=C2=BB</strong><br>
=D0=AD=D0=BA=D0=BE=D0=BD=D0=BE=D0=BC=D0=B8=D1=81=D1=82=D1=8B =D1=81=D1=87=
=D0=B8=D1=82=D0=B0=D1=8E=D1=82, =D1=87=D1=82=D0=BE =D1=80=D0=B5=D1=88=D0=B5=
=D0=BD=D0=B8=D1=8F, =D0=BA=D0=BE=D1=82=D0=BE=D1=80=D1=8B=D0=B5 =D0=BC=D1=8B=
 =D0=BF=D1=80=D0=B8=D0=BD=D0=B8=D0=BC=D0=B0=D0=B5=D0=BC, =D0=B2=D1=81=D0=B5=
=D0=B3=D0=B4=D0=B0 =D1=81=D0=B2=D1=8F=D0=B7=D0=B0=D0=BD=D1=8B =D1=81=D0=BE =
=D1=81=D1=82=D0=B8=D0=BC=D1=83=D0=BB=D0=B0=D0=BC=D0=B8 =E2=80=93 =D0=B8 =D0=
=BF=D0=BE=D0=BD=D0=B8=D0=BC=D0=B0=D1=8F, =D0=BA=D0=B0=D0=BA =D0=BE=D0=BD=D0=
=B8 =D1=83=D1=81=D1=82=D1=80=D0=BE=D0=B5=D0=BD=D1=8B, =D0=BC=D1=8B =D0=BC=
=D0=BE=D0=B6=D0=B5=D0=BC =D1=81=D0=B4=D0=B5=D0=BB=D0=B0=D1=82=D1=8C =D0=B1=
=D0=BE=D0=BB=D0=B5=D0=B5 =D1=82=D0=BE=D1=87=D0=BD=D1=8B=D0=B9 =D0=B8 =D0=B0=
=D0=B4=D0=B5=D0=BA=D0=B2=D0=B0=D1=82=D0=BD=D1=8B=D0=B9 =D0=B2=D1=8B=D0=B1=
=D0=BE=D1=80. =D0=9F=D0=B0=D0=BD=D0=B8=D0=BA=D0=B0 =E2=80=93 =D1=8D=D1=82=
=D0=BE =D1=82=D0=BE=D0=B6=D0=B5 =D1=81=D1=82=D0=B8=D0=BC=D1=83=D0=BB: =D0=
=BA=D0=B0=D0=BA =D0=B2=D0=B5=D0=B4=D0=B5=D1=82 =D1=81=D0=B5=D0=B1=D1=8F =D1=
=8D=D0=BA=D0=BE=D0=BD=D0=BE=D0=BC=D0=B8=D0=BA=D0=B0, =D0=BA=D0=BE=D0=B3=D0=
=B4=D0=B0 =D0=BB=D1=8E=D0=B4=D0=B8 =D1=81=D0=BB=D0=B5=D0=B4=D1=83=D1=8E=D1=
=82 =C2=AB=D0=BF=D0=BB=D0=BE=D1=85=D0=B8=D0=BC=C2=BB =D1=81=D1=82=D0=B8=D0=
=BC=D1=83=D0=BB=D0=B0=D0=BC? =D0=A0=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B6=D0=
=B5=D1=82 =D0=90=D0=BD=D0=B4=D1=80=D0=B5=D0=B9 =D0=91=D1=80=D0=B5=D0=BC=D0=
=B7=D0=B5=D0=BD, =D0=BF=D1=80=D0=BE=D1=84=D0=B5=D1=81=D1=81=D0=BE=D1=80 =D1=
=8D=D0=BA=D0=BE=D0=BD=D0=BE=D0=BC=D0=B8=D0=BA=D0=B8 =D0=A0=D0=AD=D0=A8.<br>
<a data-cke-saved-href=3D""https://www.inliberty.ru/public/nes/"" href=3D""htt=
ps://unimail.hse.ru/ru/mail_link_tracker?hash=3D635z5n6joj7gqqjnr33pasja89w=
17rz1ua7fpnpfm43usgmoqamb1nfzzz1ux9n1pys5akutu4soepy6gt5f1gmfwtyknt3a4jmx9f=
qz66eftcjthdgco&url=3DaHR0cHM6Ly93d3cuaW5saWJlcnR5LnJ1L3B1YmxpYy9uZXMvP3V0b=
V9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2NhbXBhaWduPTIzMjg3ODIz=
Mg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127,255);"">=D0=A0=D0=B5=
=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F</a><br>
<br>
<span style=3D""background-color:#ffffcc"">=D0=94=D0=B5=D0=B4=D0=BB=D0=B0=D0=
=B9=D0=BD =E2=80=93 9 =D0=BC=D0=B0=D1=8F</span><br>
<strong>SBER#UP for STUDENTS<br>
=D0=9F=D1=80=D0=BE=D0=BA=D0=B0=D1=87=D0=B0=D0=B9 =D0=B8=D0=B4=D0=B5=D1=8E =
=D1=81=D1=82=D0=B0=D1=80=D1=82=D0=B0=D0=BF-=D0=BF=D1=80=D0=BE=D0=B5=D0=BA=
=D1=82=D0=B0</strong><br>
=D0=A1=D0=BE=D1=82=D1=80=D1=83=D0=B4=D0=BD=D0=B8=D0=BA=D0=B8 =D0=91=D0=B0=
=D0=BD=D0=BA=D0=B0 =D1=83=D1=87=D0=B0=D1=81=D1=82=D0=B2=D1=83=D1=8E=D1=82 =
=D0=B2 =D0=90=D0=BA=D1=81=D0=B5=D0=BB=D0=B5=D1=80=D0=B0=D1=82=D0=BE=D1=80=
=D0=B5 Sber#Up, =D1=87=D1=82=D0=BE=D0=B1=D1=8B =D1=80=D0=B0=D0=B7=D0=B2=D0=
=B8=D1=82=D1=8C =D1=81=D0=B2=D0=BE=D0=B8 =D0=B8=D0=B4=D0=B5=D0=B8 =D0=B4=D0=
=BE =D1=81=D1=82=D0=B0=D1=80=D1=82=D0=B0=D0=BF=D0=BE=D0=B2. =D0=98 =D0=B2=
=D1=8B =D0=BC=D0=BE=D0=B6=D0=B5=D1=82=D0=B5 =D1=81=D1=82=D0=B0=D1=82=D1=8C =
=D1=87=D0=B0=D1=81=D1=82=D1=8C=D1=8E =D1=8D=D1=82=D0=BE=D0=B9 =D0=B8=D1=81=
=D1=82=D0=BE=D1=80=D0=B8=D0=B8. =D0=95=D1=81=D0=BB=D0=B8 =D0=B2=D0=B0=D1=88=
=D0=B0 =D0=B8=D0=B4=D0=B5=D1=8F/=D1=80=D0=B5=D1=88=D0=B5=D0=BD=D0=B8=D0=B5 =
=D0=B1=D1=83=D0=B4=D0=B5=D1=82 =D0=B2=D1=8B=D0=B1=D1=80=D0=B0=D0=BD=D0=BE =
=D0=B4=D0=BB=D1=8F =D0=BF=D1=80=D0=BE=D0=BA=D0=B0=D1=87=D0=BA=D0=B8 =D0=BF=
=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D0=B0, =D1=83 =D0=B2=D0=B0=D1=81 =D0=BF=D0=
=BE=D1=8F=D0=B2=D0=B8=D1=82=D1=81=D1=8F =D1=88=D0=B0=D0=BD=D1=81 =D0=BF=D0=
=BE=D0=BF=D0=B0=D1=81=D1=82=D1=8C =D0=B2 =D0=BA=D0=BE=D0=BC=D0=B0=D0=BD=D0=
=B4=D1=83 =D1=81=D1=82=D0=B0=D1=80=D1=82=D0=B0=D0=BF=D0=B0 =D0=B8 =D1=81=D1=
=82=D0=B0=D1=82=D1=8C =D0=B5=D0=B3=D0=BE =D0=BF=D0=BE=D0=BB=D0=BD=D0=BE=D1=
=86=D0=B5=D0=BD=D0=BD=D1=8B=D0=BC =D1=83=D1=87=D0=B0=D1=81=D1=82=D0=BD=D0=
=B8=D0=BA=D0=BE=D0=BC. =D0=9A=D1=80=D0=BE=D0=BC=D0=B5 =D1=82=D0=BE=D0=B3=D0=
=BE, =D0=B2 =D1=81=D0=BB=D1=83=D1=87=D0=B0=D0=B5 =D0=BF=D0=BE=D0=B1=D0=B5=
=D0=B4=D1=8B =D0=B2=D0=B0=D1=88=D0=B5=D0=B9 =D0=B8=D0=B4=D0=B5=D0=B8 =D0=B2=
=D1=8B =D1=82=D0=B0=D0=BA=D0=B6=D0=B5 =D0=BF=D0=BE=D0=BB=D1=83=D1=87=D0=B8=
=D1=82=D0=B5 =D1=81=D0=B5=D1=80=D1=82=D0=B8=D1=84=D0=B8=D0=BA=D0=B0=D1=82 =
=D0=BD=D0=B0 =D0=BF=D0=BE=D0=BA=D1=83=D0=BF=D0=BA=D1=83 =D1=82=D0=B5=D1=85=
=D0=BD=D0=B8=D0=BA=D0=B8 =D0=BD=D0=B0 =D1=81=D1=83=D0=BC=D0=BC=D1=83 150 00=
0 =D1=80=D1=83=D0=B1.<br>
<a data-cke-saved-href=3D""http://sber-up.ru/"" href=3D""https://unimail.hse.r=
u/ru/mail_link_tracker?hash=3D6wwes585toy5dcjnr33pasja89w17rz1ua7fpnpfm43us=
gmoqamb6jg7jth3hjcoz8k794uid57iiogftcrc1foxceyknt3a4jmx9fqz66eftcjthdgco&ur=
l=3DaHR0cDovL3NiZXItdXAucnUvP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5=
kZXImdXRtX2NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""colo=
r:rgb(0,127,255);"">=D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5</=
a></span></span></span></p>
</div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 67.1875px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 10px 30px; vertical-align: top; font-siz=
e: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; colo=
r: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""line-height:1.5;""><span style=3D""font-size:16px;""><span style=3D""font-fami=
ly:Arial, Helvetica, sans-serif;"">=D0=A1=D0=BB=D0=B5=D0=B4=D0=B8=D1=82=D0=
=B5 =D0=B7=D0=B0 =D0=B2=D1=81=D0=B5=D0=BC=D0=B8 =D0=B8=D0=B7=D0=BC=D0=B5=D0=
=BD=D0=B5=D0=BD=D0=B8=D1=8F=D0=BC=D0=B8 =D0=BD=D0=B0 <a href=3D""https://uni=
mail.hse.ru/ru/mail_link_tracker?hash=3D67z5mdh5x17446jnr33pasja89w17rz1ua7=
fpnpfm43usgmoqambzcirfkt5893g1bnoq5b8jd153cdowuby7eb1cf5ge75tbx3h5j71sywopd=
fiu6auo&url=3DaHR0cHM6Ly93d3cuaHNlLnJ1L291ci8_dXRtX21lZGl1bT1lbWFpbCZ1dG1fc=
291cmNlPVVuaVNlbmRlciZ1dG1fY2FtcGFpZ249MjMyODc4MjMy&uid=3DMTMyMzY3NA=3D=3D""=
 style=3D""color:rgb(0,127,255);"">=C2=AB=D0=92=D1=8B=D1=88=D0=BA=D0=B5 =D0=
=B4=D0=BB=D1=8F =D1=81=D0=B2=D0=BE=D0=B8=D1=85=C2=BB</a> =D0=B8 =D0=B2 =D0=
=BD=D0=B0=D1=88=D0=B5=D0=BC <a href=3D""https://unimail.hse.ru/ru/mail_link_=
tracker?hash=3D669k1p9qtqkf4ejnr33pasja89w17rz1ua7fpnpfm43usgmoqamb3t3aw3qr=
7xj6qwqmoe56xgf69q3twcgsaro76uhknt3a4jmx9fqz66eftcjthdgco&url=3DaHR0cHM6Ly9=
0Lm1lL2hzZV9saXZlP3V0bV9tZWRpdW09ZW1haWwmdXRtX3NvdXJjZT1VbmlTZW5kZXImdXRtX2=
NhbXBhaWduPTIzMjg3ODIzMg~~&uid=3DMTMyMzY3NA=3D=3D"" style=3D""color:rgb(0,127=
,255);"">Telegram-=D0=BA=D0=B0=D0=BD=D0=B0=D0=BB=D0=B5</a>!</span></span></s=
pan></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--><!--[if (gte mso 9)=
|(IE)]><table cellpadding=3D""0"" cellspacing=3D""0"" border=3D""0"" width=3D""600=
"" align=3D""center""><tr><td><![endif]-->
<table class=3D""uni-block text-block"" width=3D""100%"" border=3D""0"" cellspaci=
ng=3D""0"" cellpadding=3D""0"" style=3D""width: 100%; table-layout: fixed; heigh=
t: auto; border-collapse: collapse; border-spacing: 0px; display: inline-ta=
ble; vertical-align: top; font-size: medium;"">
<tbody>
<tr>
<td style=3D""width: 100%; background-color: rgb(255, 255, 255); background-=
image: none; border: none; height: 100%;"" class=3D""block-wrapper"" valign=3D=
""top"">
<table class=3D""block-wrapper-inner-table"" border=3D""0"" cellspacing=3D""0"" c=
ellpadding=3D""0"" style=3D""height: 45.9549px; width: 100%; table-layout: fix=
ed; border-spacing: 0px; border-collapse: collapse;"">
<tbody>
<tr>
<td style=3D""width: 100%; padding: 5px 30px 20px; vertical-align: top; font=
-size: 14px; font-family: Tahoma, Geneva, sans-serif; line-height: 16.8px; =
color: rgb(51, 51, 51);"" class=3D""content-wrapper"">
<div class=3D""clearfix"" style=3D""overflow-wrap: break-word;""><span style=3D=
""font-size:14px;""><em><span style=3D""font-family:Arial, Helvetica, sans-ser=
if;""><span style=3D""line-height:1.5;"">=D0=9F=D0=BE=D1=8F=D0=B2=D0=B8=D0=BB=
=D0=B8=D1=81=D1=8C =D0=B2=D0=BE=D0=BF=D1=80=D0=BE=D1=81=D1=8B =D0=B8=D0=BB=
=D0=B8 =D0=BF=D1=80=D0=B5=D0=B4=D0=BB=D0=BE=D0=B6=D0=B5=D0=BD=D0=B8=D1=8F? =
=D0=9F=D0=B8=D1=88=D0=B8=D1=82=D0=B5: community@hse.ru.</span></span></em><=
/span></div>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td>
</tr>
</tbody>
</table>
<!--[if (gte mso 9)|(IE)]></td></tr></table><![endif]--></td>
</tr>
</tbody>
</table>
</td>
</tr>
</tbody>
</table>
</center>
<table bgcolor=3D""white"" align=3D""left"" width=3D""100%""><tr><td><span style=
=3D""font-family: arial,helvetica,sans-serif; color: black; font-size: 12px;=
""><p style=3D""text-align: center; color: #bababa;"">=D0=A7=D1=82=D0=BE=D0=B1=
=D1=8B =D0=BE=D1=82=D0=BF=D0=B8=D1=81=D0=B0=D1=82=D1=8C=D1=81=D1=8F =D0=BE=
=D1=82 =D1=8D=D1=82=D0=BE=D0=B9 =D1=80=D0=B0=D1=81=D1=81=D1=8B=D0=BB=D0=BA=
=D0=B8, =D0=BF=D0=B5=D1=80=D0=B5=D0=B9=D0=B4=D0=B8=D1=82=D0=B5 =D0=BF=D0=BE=
 <a style=3D""color: #46a8c6;"" href=3D""https://unimail.hse.ru/ru/unsubscribe=
?hash=3D6knt4e3zwqyttj157fxz57m156w17rz1ua7fpnpfm43usgmoqambawhmfw7kc1jd5sj=
ozspcmjaf1b85ox1u8n1qzyr#no_tracking"">=D1=81=D1=81=D1=8B=D0=BB=D0=BA=D0=B5<=
/a></p></span></td></tr></table><center><table><tr><td><img src=3D""https://=
unimail.hse.ru/ru/mail_read_tracker/1323674?hash=3D67yb35bmoneikriwgeupcxy5=
q8w17rz1ua7fpnpfm43usgmoqambzzhixoab47cz1p9bbmszibib9x9z1bcmncxy3ur"" width=
=3D""1"" height=3D""1"" alt=3D"""" title=3D"""" border=3D""0""></td></tr></table></ce=
nter></body>
</html>";


        private const string HseBody0Plain = @"=D0=A1=D1=82=D1=83=D0=B4=D1=81=D0=BE=D0=B2=D0=B5=D1=82 =D0=BE=D0=BF=D1=
=83=D0=B1=D0=BB=D0=B8=D0=BA=D0=BE=D0=B2=D0=B0=D0=BB =D0=BF=D1=80=D0=BE=
=D0=B5=D0=BA=D1=82 =D0=9A=D0=BE=D0=B4=D0=B5=D0=BA=D1=81=D0=B0 =D1=8D=D1=
=82=D0=B8=D0=BA=D0=B8 =D1=81=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D0=BE=
=D0=B2
=D0=92=D1=8B=D1=88=D0=BA=D0=B0 =D0=BF=D1=80=D0=BE=D0=B4=D0=BE=D0=BB=D0=
=B6=D0=B0=D0=B5=D1=82 =D1=80=D0=B0=D0=B7=D1=80=D0=B0=D0=B1=D0=BE=D1=82=
=D0=BA=D1=83 =D1=8D=D1=82=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D1=85 =D0=
=BA=D0=BE=D0=B4=D0=B5=D0=BA=D1=81=D0=BE=D0=B2 =D0=B4=D0=BB=D1=8F =D1=81=
=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=B2, =D1=81=D0=BE=D1=82=D1=
=80=D1=83=D0=B4=D0=BD=D0=B8=D0=BA=D0=BE=D0=B2 =D0=B8 =D0=BF=D1=80=D0=B5=
=D0=BF=D0=BE=D0=B4=D0=B0=D0=B2=D0=B0=D1=82=D0=B5=D0=BB=D0=B5=D0=B9. =D0=
=9F=D0=BE=D1=81=D0=BB=D0=B5 =D0=BD=D0=B5=D1=81=D0=BA=D0=BE=D0=BB=D1=8C=
=D0=BA=D0=B8=D1=85 =D0=BD=D0=B5=D0=B4=D0=B5=D0=BB=D1=8C =D0=BE=D0=B1=D1=
=81=D1=83=D0=B6=D0=B4=D0=B5=D0=BD=D0=B8=D0=B9 =D1=80=D0=B0=D0=B1=D0=BE=
=D1=87=D0=B0=D1=8F =D0=B3=D1=80=D1=83=D0=BF=D0=BF=D0=B0 =D1=81=D1=82=D1=
=83=D0=B4=D0=B5=D0=BD=D1=87=D0=B5=D1=81=D0=BA=D0=BE=D0=B3=D0=BE =D1=81=
=D0=BE=D0=B2=D0=B5=D1=82=D0=B0 =D0=BF=D0=BE=D0=B4=D0=B3=D0=BE=D1=82=D0=
=BE=D0=B2=D0=B8=D0=BB=D0=B0 =D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82 (https://d=
ocs.google.com/document/d/1OyY1aHQ3ZmxgEKevJycP2iQJ0zN5UpfaL7phNBT4cwk/edit=
) =D0=B4=D0=BE=D0=BA=D1=83=D0=BC=D0=B5=D0=BD=D1=82=D0=B0 =D0=B4=D0=BB=D1=
=8F =D1=81=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=B2. =D0=A1=D1=82=
=D1=83=D0=B4=D1=81=D0=BE=D0=B2=D0=B5=D1=82 =D0=BF=D1=80=D0=B8=D0=B3=D0=
=BB=D0=B0=D1=88=D0=B0=D0=B5=D1=82 =D0=B2=D1=81=D0=B5=D1=85 =D1=81=D1=82=
=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=B2 =D0=BE=D0=B7=D0=BD=D0=B0=D0=
=BA=D0=BE=D0=BC=D0=B8=D1=82=D1=8C=D1=81=D1=8F =D1=81 =D0=BF=D1=80=D0=BE=
=D0=B5=D0=BA=D1=82=D0=BE=D0=BC =D1=8D=D1=82=D0=B8=D1=87=D0=B5=D1=81=D0=
=BA=D0=BE=D0=B3=D0=BE =D0=BA=D0=BE=D0=B4=D0=B5=D0=BA=D1=81=D0=B0 =D0=B8 =
=D0=B2=D1=8B=D1=81=D0=BA=D0=B0=D0=B7=D0=B0=D1=82=D1=8C (https://login.micro=
softonline.com/common/oauth2/authorize?response_mode=3Dform_post&response_t=
ype=3Did_token+code&scope=3Dopenid&msafed=3D0&nonce=3D37beb43b-2a7a-40c3-ac=
15-029d9f1a5246.637225416820943320&state=3Dhttps://forms.office.com/Pages/R=
esponsePage.aspx?id=3DJGzyIZMHB0unPVY80uwjX9P4tlQ4C19AmAkxIMbZ1yJUODRQMkVVN=
U5XUExZUlMzTTFMR0VKMEUxMy4u&client_id=3Dc9a559d2-7aab-4f13-a6ed-e7e9c52aec8=
7&redirect_uri=3Dhttps://forms.office.com/auth/signin) =D1=81=D0=B2=D0=
=BE=D0=B5 =D0=BC=D0=BD=D0=B5=D0=BD=D0=B8=D0=B5.
=D0=A0=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B7=D0=B0=D0=BB=D0=B8 =D0=BF=D0=
=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5 =D0=BE =D0=9A=D0=BE=D0=B4=
=D0=B5=D0=BA=D1=81=D0=B5 =D0=B8 =D1=80=D0=B0=D0=B1=D0=BE=D1=87=D0=B5=D0=
=B9 =D0=B3=D1=80=D1=83=D0=BF=D0=BF=D0=B5 =D0=B2 =D1=81=D1=82=D0=B0=D1=82=
=D1=8C=D0=B5 (https://www.hse.ru/our/news/360750505.html).

=D0=92=D1=81=D1=82=D1=80=D0=B5=D1=87=D0=B0 =D0=AF=D1=80=D0=BE=D1=81=D0=
=BB=D0=B0=D0=B2=D0=B0 =D0=9A=D1=83=D0=B7=D1=8C=D0=BC=D0=B8=D0=BD=D0=BE=
=D0=B2=D0=B0 =D1=81 =D0=BF=D1=80=D0=B5=D0=B4=D1=81=D1=82=D0=B0=D0=B2=D0=
=B8=D1=82=D0=B5=D0=BB=D1=8F=D0=BC=D0=B8 =D1=81=D1=82=D1=83=D0=B4=D1=81=
=D0=BE=D0=B2=D0=B5=D1=82=D0=BE=D0=B2
=D0=9E =D1=80=D0=B0=D1=81=D1=81=D0=B5=D0=BB=D0=B5=D0=BD=D0=B8=D0=B8 =D1=
=81=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=B2 =D0=B2 =D0=BE=D0=B1=
=D1=89=D0=B5=D0=B6=D0=B8=D1=82=D0=B8=D1=8F=D1=85, =D0=BD=D0=BE=D0=B2=D1=
=8B=D1=85 =D1=84=D0=BE=D1=80=D0=BC=D0=B0=D1=82=D0=B0=D1=85 =D0=BF=D1=80=
=D0=BE=D0=B2=D0=B5=D0=B4=D0=B5=D0=BD=D0=B8=D1=8F =D1=8D=D0=BA=D0=B7=D0=
=B0=D0=BC=D0=B5=D0=BD=D0=BE=D0=B2, =D1=81=D0=B4=D0=B0=D1=87=D0=B5 =D0=BA=
=D1=83=D1=80=D1=81=D0=BE=D0=B2=D1=8B=D1=85 =D1=80=D0=B0=D0=B1=D0=BE=D1=
=82 =D0=B8 =D0=B4=D1=80=D1=83=D0=B3=D0=B8=D1=85 =D1=82=D0=B5=D0=B7=D0=B8=
=D1=81=D0=B0=D1=85 =D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D0=B2=D1=81=D1=
=82=D1=80=D0=B5=D1=87=D0=B8 =E2=80=94 =D0=B2 =D0=BD=D0=B0=D1=88=D0=B5=D0=
=B9 =D1=81=D1=82=D0=B0=D1=82=D1=8C=D0=B5 (https://www.hse.ru/our/news/35930=
0179.html).

=D0=92=D1=8B=D1=88=D0=BA=D0=B0 =D0=B7=D0=B0=D0=BF=D1=83=D1=81=D1=82=D0=
=B8=D0=BB=D0=B0 =D1=81=D0=B0=D0=B9=D1=82 =D1=81 =D0=B8=D1=81=D1=82=D0=BE=
=D1=80=D0=B8=D1=8F=D0=BC=D0=B8 =D0=BE =D0=B2=D0=BE=D0=B9=D0=BD=D0=B5
=D0=94=D0=B8=D1=80=D0=B5=D0=BA=D1=86=D0=B8=D1=8F =D0=BF=D0=BE =D1=80=D0=
=B0=D0=B7=D0=B2=D0=B8=D1=82=D0=B8=D1=8E =D1=81=D1=82=D1=83=D0=B4=D0=B5=
=D0=BD=D1=87=D0=B5=D1=81=D0=BA=D0=BE=D0=B3=D0=BE =D0=BF=D0=BE=D1=82=D0=
=B5=D0=BD=D1=86=D0=B8=D0=B0=D0=BB=D0=B0 =D0=BA 75-=D0=BB=D0=B5=D1=82=D0=
=B8=D1=8E =D0=9F=D0=BE=D0=B1=D0=B5=D0=B4=D1=8B =D0=B2 =D0=92=D0=B5=D0=BB=
=D0=B8=D0=BA=D0=BE=D0=B9 =D0=9E=D1=82=D0=B5=D1=87=D0=B5=D1=81=D1=82=D0=
=B2=D0=B5=D0=BD=D0=BD=D0=BE=D0=B9 =D0=B2=D0=BE=D0=B9=D0=BD=D0=B5 =D0=BF=
=D1=80=D0=B5=D0=B4=D0=BB=D0=B0=D0=B3=D0=B0=D0=B5=D1=82 (https://www.hse.ru/=
our/news/359611214.html) =D0=BF=D0=BE=D0=B4=D0=B5=D0=BB=D0=B8=D1=82=D1=
=8C=D1=81=D1=8F =D0=B2=D0=BE=D1=81=D0=BF=D0=BE=D0=BC=D0=B8=D0=BD=D0=B0=
=D0=BD=D0=B8=D1=8F=D0=BC=D0=B8 =D0=BE =D1=81=D0=B2=D0=BE=D0=B8=D1=85 =D1=
=80=D0=BE=D0=B4=D0=BD=D1=8B=D1=85, =D0=B2=D0=BE=D0=B5=D0=B2=D0=B0=D0=B2=
=D1=88=D0=B8=D1=85 =D0=BD=D0=B0 =D1=84=D1=80=D0=BE=D0=BD=D1=82=D0=B0=D1=
=85. =D0=92=D1=8B =D0=BC=D0=BE=D0=B6=D0=B5=D1=82=D0=B5 =D1=80=D0=B0=D1=
=81=D1=81=D0=BA=D0=B0=D0=B7=D0=B0=D1=82=D1=8C =D1=81=D0=B2=D0=BE=D1=8E =
=D1=81=D0=B5=D0=BC=D0=B5=D0=B9=D0=BD=D1=83=D1=8E =D0=B8=D1=81=D1=82=D0=
=BE=D1=80=D0=B8=D1=8E, =D0=B7=D0=B0=D0=BF=D0=BE=D0=BB=D0=BD=D0=B8=D0=B2 =
=D1=84=D0=BE=D1=80=D0=BC=D1=83 =D0=BF=D0=BE =D1=81=D1=81=D1=8B=D0=BB=D0=
=BA=D0=B5 (https://www.hse.ru/9may/polls/357829371.html). =D0=92=D0=B0=
=D1=88=D0=B8 =D1=80=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B7=D1=8B =D0=B8 =D1=
=84=D0=BE=D1=82=D0=BE=D0=B3=D1=80=D0=B0=D1=84=D0=B8=D0=B8 =D0=B1=D1=83=
=D0=B4=D1=83=D1=82 =D1=81=D0=BE=D0=B1=D1=80=D0=B0=D0=BD=D1=8B =D0=B8 =D0=
=BE=D0=BF=D1=83=D0=B1=D0=BB=D0=B8=D0=BA=D0=BE=D0=B2=D0=B0=D0=BD=D1=8B =
=D0=BD=D0=B0 =D1=81=D0=B0=D0=B9=D1=82=D0=B5 (https://www.hse.ru/9may) =
=D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D0=B0.

=D0=9A=D0=BE=D0=BC=D0=BF=D0=B5=D0=BD=D1=81=D0=B0=D1=86=D0=B8=D0=B8 =D0=
=BD=D0=B0 =D0=BF=D0=B5=D1=80=D0=B5=D0=B5=D0=B7=D0=B4 =D0=B8=D0=B7 =D0=BE=
=D0=B1=D1=89=D0=B5=D0=B6=D0=B8=D1=82=D0=B8=D1=8F
=D0=92=D1=8B=D1=88=D0=BA=D0=B0 =D0=BA=D0=B0=D0=B6=D0=B4=D1=8B=D0=B9 =D0=
=B3=D0=BE=D0=B4 =D0=BF=D0=BE=D0=BC=D0=BE=D0=B3=D0=B0=D0=B5=D1=82 =D0=BF=
=D0=B5=D1=80=D0=B5=D0=B5=D1=85=D0=B0=D1=82=D1=8C =D0=B8=D0=B7 =D0=BE=D0=
=B1=D1=89=D0=B5=D0=B6=D0=B8=D1=82=D0=B8=D1=8F =D0=B2 =D0=BA=D0=B2=D0=B0=
=D1=80=D1=82=D0=B8=D1=80=D1=83 =D0=B8 =D0=BA=D0=BE=D0=BC=D0=BF=D0=B5=D0=
=BD=D1=81=D0=B8=D1=80=D1=83=D0=B5=D1=82 =D1=87=D0=B0=D1=81=D1=82=D1=8C =
=D1=80=D0=B0=D1=81=D1=85=D0=BE=D0=B4=D0=BE=D0=B2 =D0=BD=D0=B0 =D0=B0=D1=
=80=D0=B5=D0=BD=D0=B4=D1=83 =D0=B6=D0=B8=D0=BB=D1=8C=D1=8F. =D0=92 =D1=
=8D=D1=82=D0=BE=D0=BC =D0=B3=D0=BE=D0=B4=D1=83 =D0=B2=D1=8B=D0=B4=D0=B5=
=D0=BB=D0=B5=D0=BD=D0=BE 2000 =D0=BA=D0=BE=D0=BC=D0=BF=D0=B5=D0=BD=D1=81=
=D0=B0=D1=86=D0=B8=D0=B9. =D0=A0=D0=B0=D0=B7=D0=BC=D0=B5=D1=80, =D0=BA=
=D0=B0=D0=BA =D0=B8 =D0=B2 =D0=BF=D1=80=D0=BE=D1=88=D0=BB=D0=BE=D0=BC =
=D0=B3=D0=BE=D0=B4=D1=83, =D1=81=D0=BE=D1=81=D1=82=D0=B0=D0=B2=D0=B8=D1=
=82 10 =D1=82=D1=8B=D1=81. =D1=80=D1=83=D0=B1=D0=BB=D0=B5=D0=B9 =D0=B5=
=D0=B6=D0=B5=D0=BC=D0=B5=D1=81=D1=8F=D1=87=D0=BD=D0=BE. =D0=97=D0=B0=D1=
=8F=D0=B2=D0=BA=D0=B8 =D0=BF=D1=80=D0=B8=D0=BD=D0=B8=D0=BC=D0=B0=D1=8E=
=D1=82=D1=81=D1=8F =D1=81 1 =D0=BC=D0=B0=D1=8F =D0=BF=D0=BE 15 =D0=B8=D1=
=8E=D0=BD=D1=8F. =D0=9E =D1=82=D0=BE=D0=BC, =D0=BA=D1=82=D0=BE =D0=BC=D0=
=BE=D0=B6=D0=B5=D1=82 =D0=BF=D1=80=D0=B5=D1=82=D0=B5=D0=BD=D0=B4=D0=BE=
=D0=B2=D0=B0=D1=82=D1=8C =D0=BD=D0=B0 =D0=BA=D0=BE=D0=BC=D0=BF=D0=B5=D0=
=BD=D1=81=D0=B0=D1=86=D0=B8=D0=B8 =D0=B8 =D0=BA=D0=B0=D0=BA=D0=B8=D0=B5 =
=D0=B4=D0=BE=D0=BA=D1=83=D0=BC=D0=B5=D0=BD=D1=82=D1=8B =D0=BD=D1=83=D0=
=B6=D0=BD=D1=8B =D0=B4=D0=BB=D1=8F =D0=BF=D0=BE=D0=B4=D0=B0=D1=87=D0=B8 =
=D0=B7=D0=B0=D1=8F=D0=B2=D0=BE=D0=BA =E2=80=94 =D0=B2 =D0=BD=D0=B0=D1=88=
=D0=B5=D0=B9 =D1=81=D1=82=D0=B0=D1=82=D1=8C=D0=B5 (https://www.hse.ru/our/n=
ews/359452149.html).

6-8 =D0=BC=D0=B0=D1=8F =E2=80=94 =D1=83=D1=87=D0=B5=D0=B1=D0=BD=D1=8B=D0=
=B5 =D0=B4=D0=BD=D0=B8
=D0=9F=D0=BE=D0=B4=D0=BF=D0=B8=D1=81=D0=B0=D0=BD =D0=BF=D1=80=D0=B8=D0=
=BA=D0=B0=D0=B7 (https://www.hse.ru/our/news/359606367.html), =D0=B2=D0=
=BD=D0=BE=D1=81=D1=8F=D1=89=D0=B8=D0=B9 =D0=B8=D0=B7=D0=BC=D0=B5=D0=BD=
=D0=B5=D0=BD=D0=B8=D1=8F =D0=B2 =D0=B3=D1=80=D0=B0=D1=84=D0=B8=D0=BA =D1=
=83=D1=87=D0=B5=D0=B1=D0=BD=D0=BE=D0=B3=D0=BE =D0=BF=D1=80=D0=BE=D1=86=
=D0=B5=D1=81=D1=81=D0=B0. =D0=9F=D0=BE =D0=BD=D0=BE=D0=B2=D0=BE=D0=BC=D1=
=83 =D0=B3=D1=80=D0=B0=D1=84=D0=B8=D0=BA=D1=83 =D0=BD=D0=B5=D1=83=D1=87=
=D0=B5=D0=B1=D0=BD=D1=8B=D0=BC=D0=B8 =D0=B1=D1=83=D0=B4=D1=83=D1=82 =D1=
=82=D0=BE=D0=BB=D1=8C=D0=BA=D0=BE =D0=BF=D1=80=D0=B0=D0=B7=D0=B4=D0=BD=
=D0=B8=D1=87=D0=BD=D1=8B=D0=B5 =D0=B4=D0=BD=D0=B8 =D1=81 1 =D0=BF=D0=BE 5 =
=D0=BC=D0=B0=D1=8F =D0=B8 =D1=81 9 =D0=BF=D0=BE 11 =D0=BC=D0=B0=D1=8F.

=D0=9A=D0=BE=D0=BD=D0=BA=D1=83=D1=80=D1=81 =D1=81=D1=82=D0=B8=D0=BF=D0=
=B5=D0=BD=D0=B4=D0=B8=D0=B9 =D0=9F=D1=80=D0=B5=D0=B7=D0=B8=D0=B4=D0=B5=
=D0=BD=D1=82=D0=B0 =D0=B8 =D0=9F=D1=80=D0=B0=D0=B2=D0=B8=D1=82=D0=B5=D0=
=BB=D1=8C=D1=81=D1=82=D0=B2=D0=B0 =D0=A0=D0=A4
=D0=94=D0=BE 5 =D0=BC=D0=B0=D1=8F =D0=BF=D1=80=D0=B8=D0=BD=D0=B8=D0=BC=
=D0=B0=D1=8E=D1=82=D1=81=D1=8F =D0=B7=D0=B0=D1=8F=D0=B2=D0=BA=D0=B8 =D0=
=BD=D0=B0 =D0=BA=D0=BE=D0=BD=D0=BA=D1=83=D1=80=D1=81 (https://www.hse.ru/sc=
holarships/announcements/359295889.html) =D1=81=D1=82=D0=B8=D0=BF=D0=B5=
=D0=BD=D0=B4=D0=B8=D0=B9 =D0=9F=D1=80=D0=B5=D0=B7=D0=B8=D0=B4=D0=B5=D0=
=BD=D1=82=D0=B0 =D0=B8 =D0=9F=D1=80=D0=B0=D0=B2=D0=B8=D1=82=D0=B5=D0=BB=
=D1=8C=D1=81=D1=82=D0=B2=D0=B0 =D0=A0=D0=A4. =D0=92 =D0=BA=D0=BE=D0=BD=
=D0=BA=D1=83=D1=80=D1=81=D0=B5 =D0=BC=D0=BE=D0=B3=D1=83=D1=82 =D1=83=D1=
=87=D0=B0=D1=81=D1=82=D0=B2=D0=BE=D0=B2=D0=B0=D1=82=D1=8C =D1=81=D1=82=
=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D1=8B =D0=B8 =D0=B0=D1=81=D0=BF=D0=B8=D1=
=80=D0=B0=D0=BD=D1=82=D1=8B, =D0=BE=D0=B1=D1=83=D1=87=D0=B0=D1=8E=D1=89=
=D0=B8=D0=B5=D1=81=D1=8F =D0=B7=D0=B0 =D1=81=D1=87=D0=B5=D1=82 =D0=B1=D1=
=8E=D0=B4=D0=B6=D0=B5=D1=82=D0=B0, =D1=81=D1=82=D1=83=D0=B4=D0=B5=D0=BD=
=D1=82=D1=8B, =D0=BD=D0=B0 1 =D1=81=D0=B5=D0=BD=D1=82=D1=8F=D0=B1=D1=80=
=D1=8F 2020 =D0=B3=D0=BE=D0=B4=D0=B0 =D0=BE=D0=B1=D1=83=D1=87=D0=B0=D1=
=8E=D1=89=D0=B8=D0=B5=D1=81=D1=8F =D0=BD=D0=B0 3-4 =D0=BA=D1=83=D1=80=D1=
=81=D0=B0=D1=85 =D0=B1=D0=B0=D0=BA=D0=B0=D0=BB=D0=B0=D0=B2=D1=80=D0=B8=
=D0=B0=D1=82=D0=B0, 3-5 =D0=BA=D1=83=D1=80=D1=81=D0=B0=D1=85 =D1=81=D0=
=BF=D0=B5=D1=86=D0=B8=D0=B0=D0=BB=D0=B8=D1=82=D0=B5=D1=82=D0=B0, 2 =D0=
=BA=D1=83=D1=80=D1=81=D0=B5 =D0=BC=D0=B0=D0=B3=D0=B8=D1=81=D1=82=D1=80=
=D0=B0=D1=82=D1=83=D1=80=D1=8B.

WEB=D0=B8=D0=BA=D0=B5=D1=82 =D0=B4=D0=BB=D1=8F =D1=81=D1=82=D1=83=D0=B4=
=D0=B5=D0=BD=D1=82=D0=BE=D0=B2 =D0=B8 =D0=BF=D1=80=D0=B5=D0=BF=D0=BE=D0=
=B4=D0=B0=D0=B2=D0=B0=D1=82=D0=B5=D0=BB=D0=B5=D0=B9

=D0=9F=D0=B5=D1=80=D0=B5=D1=85=D0=BE=D0=B4 =D0=92=D1=8B=D1=88=D0=BA=D0=
=B8 =D0=B2 =D0=B4=D0=B8=D1=81=D1=82=D0=B0=D0=BD=D1=82 =D0=B4=D0=B8=D0=BA=
=D1=82=D1=83=D0=B5=D1=82 =D0=BD=D0=BE=D0=B2=D1=8B=D0=B5 =D0=BF=D1=80=D0=
=B0=D0=B2=D0=B8=D0=BB=D0=B0: =D1=83=D0=BD=D0=B8=D0=B2=D0=B5=D1=80=D1=81=
=D0=B8=D1=82=D0=B5=D1=82=D1=81=D0=BA=D0=BE=D0=BC=D1=83 =D1=81=D0=BE=D0=
=BE=D0=B1=D1=89=D0=B5=D1=81=D1=82=D0=B2=D1=83 =D0=BD=D1=83=D0=B6=D0=BD=
=D0=BE =D0=BD=D0=B5 =D1=82=D0=BE=D0=BB=D1=8C=D0=BA=D0=BE =D0=BE=D0=BF=D0=
=B5=D1=80=D0=B0=D1=82=D0=B8=D0=B2=D0=BD=D0=BE =D0=BE=D1=81=D0=B2=D0=BE=
=D0=B8=D1=82=D1=8C =D0=BD=D0=BE=D0=B2=D1=8B=D0=B5 =D0=B8=D0=BD=D1=81=D1=
=82=D1=80=D1=83=D0=BC=D0=B5=D0=BD=D1=82=D1=8B =D1=83=D0=B4=D0=B0=D0=BB=
=D0=B5=D0=BD=D0=BD=D0=BE=D0=B3=D0=BE =D0=BE=D0=B1=D1=83=D1=87=D0=B5=D0=
=BD=D0=B8=D1=8F, =D0=BD=D0=BE =D0=B8 =D0=BD=D0=BE=D0=B2=D1=8B=D0=B5 =D0=
=BC=D0=BE=D0=B4=D0=B5=D0=BB=D0=B8 =D0=BF=D0=BE=D0=B2=D0=B5=D0=B4=D0=B5=
=D0=BD=D0=B8=D1=8F =D0=B2 =D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD=D0=B5. =
=D0=94=D0=BB=D1=8F =D1=8D=D1=82=D0=BE=D0=B3=D0=BE =D0=92=D1=8B=D1=88=D0=
=BA=D0=B0 =D1=80=D0=B0=D0=B7=D1=80=D0=B0=D0=B1=D0=BE=D1=82=D0=B0=D0=BB=
=D0=B0 =D0=BF=D1=80=D0=B0=D0=B2=D0=B8=D0=BB=D0=B0 =D0=B8=D0=BD=D1=82=D0=
=B5=D1=80=D0=B0=D0=BA=D1=82=D0=B8=D0=B2=D0=BD=D0=BE=D0=B3=D0=BE =D0=BE=
=D0=B1=D1=89=D0=B5=D0=BD=D0=B8=D1=8F =D0=B2=D0=BE =D0=B2=D1=80=D0=B5=D0=
=BC=D1=8F =D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D0=B7=D0=B0=D0=BD=D1=8F=
=D1=82=D0=B8=D0=B9 =E2=80=94 =C2=ABWEB=D0=B8=D0=BA=D0=B5=D1=82=C2=BB. =
=D0=92=D1=81=D0=B5 =D0=B8=D0=BD=D1=81=D1=82=D1=80=D1=83=D0=BA=D1=86=D0=
=B8=D0=B8 =D1=80=D0=B0=D0=B7=D0=BC=D0=B5=D1=89=D0=B5=D0=BD=D1=8B =D0=B2 =
=C2=AB=D0=94=D0=B8=D1=81=D1=82=D0=B0=D0=BD=D1=86=D0=B8=D0=BE=D0=BD=D0=BD=
=D0=BE=D0=BC =D1=81=D0=BC=D0=BE=D1=82=D1=80=D0=B8=D1=82=D0=B5=D0=BB=D0=
=B5=C2=BB.

=D0=A6=D0=B8=D1=84=D1=80=D0=BE=D0=B2=D0=BE=D0=B9 =D0=B1=D0=BB=D0=BE=D0=
=BA =D0=BF=D1=80=D0=B8=D0=B3=D0=BB=D0=B0=D1=88=D0=B0=D0=B5=D1=82 =D0=BD=
=D0=B0 =D0=B2=D0=B5=D0=B1=D0=B8=D0=BD=D0=B0=D1=80=D1=8B =C2=AB=D0=A3=D0=
=BF=D1=80=D0=B0=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=D0=B5 =D0=B4=D0=B0=D0=BD=
=D0=BD=D1=8B=D0=BC=D0=B8=C2=BB

=D0=92=D0=B5=D0=B1=D0=B8=D0=BD=D0=B0=D1=80=D1=8B =D0=B1=D1=83=D0=B4=D1=
=83=D1=82 =D0=BF=D0=BE=D0=BB=D0=B5=D0=B7=D0=BD=D1=8B =D1=82=D0=B5=D0=BC, =
=D0=BA=D1=82=D0=BE =D0=B8=D0=BD=D1=82=D0=B5=D1=80=D0=B5=D1=81=D1=83=D0=
=B5=D1=82=D1=81=D1=8F =D1=80=D0=B5=D1=88=D0=B5=D0=BD=D0=B8=D1=8F=D0=BC=
=D0=B8 =D0=B2 =D0=BE=D0=B1=D0=BB=D0=B0=D1=81=D1=82=D0=B8 =D1=83=D0=BF=D1=
=80=D0=B0=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=D1=8F =D0=B4=D0=B0=D0=BD=D0=BD=
=D1=8B=D0=BC=D0=B8. =D0=A1=D0=BF=D0=B5=D1=86=D0=B8=D0=B0=D0=BB=D0=B8=D1=
=81=D1=82=D1=8B =C2=AB=D0=AE=D0=BD=D0=B8=D0=B4=D0=B0=D1=82=D0=B0=C2=BB =
=D1=80=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B6=D1=83=D1=82 =D0=BE =D1=81=D0=
=BF=D0=B5=D1=86=D0=B8=D1=84=D0=B8=D0=BA=D0=B5 =D1=80=D0=B0=D0=B1=D0=BE=
=D1=82=D1=8B =D1=81 =D0=BF=D0=BB=D0=B0=D1=82=D1=84=D0=BE=D1=80=D0=BC=D0=
=B0=D0=BC=D0=B8 =D1=83=D0=BF=D1=80=D0=B0=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=
=D1=8F =D0=B4=D0=B0=D0=BD=D0=BD=D1=8B=D0=BC=D0=B8, =D0=B0 =D1=82=D0=B0=
=D0=BA=D0=B6=D0=B5 =D0=BE =D1=82=D0=B0=D0=BA=D0=B8=D1=85 =D0=BE=D0=B1=D0=
=BB=D0=B0=D1=81=D1=82=D1=8F=D1=85, =D0=BA=D0=B0=D0=BA Data Governance =
=D0=B8 Data Quality. =D0=9E=D1=82=D0=BF=D1=80=D0=B0=D0=B2=D0=BB=D1=8F=D0=
=B9=D1=82=D0=B5 =D1=81=D0=B2=D0=BE=D0=B8 =D0=B4=D0=B0=D0=BD=D0=BD=D1=8B=
=D0=B5 (=D0=A4=D0=98=D0=9E, =D0=BE=D0=B1=D1=80=D0=B0=D0=B7=D0=BE=D0=B2=
=D0=B0=D1=82=D0=B5=D0=BB=D1=8C=D0=BD=D1=83=D1=8E =D0=BF=D1=80=D0=BE=D0=
=B3=D1=80=D0=B0=D0=BC=D0=BC=D1=83, =D1=82=D0=B5=D0=BB=D0=B5=D1=84=D0=BE=
=D0=BD, e-mail) =D0=BD=D0=B0 =D0=BF=D0=BE=D1=87=D1=82=D1=83 hse@unidata-pla=
tform.ru. =D0=A6=D0=B8=D1=84=D1=80=D0=BE=D0=B2=D0=BE=D0=B9 =D0=B1=D0=BB=
=D0=BE=D0=BA =D1=80=D0=B0=D1=81=D1=81=D0=BC=D0=BE=D1=82=D1=80=D0=B8=D1=
=82 =D0=B7=D0=B0=D1=8F=D0=B2=D0=BA=D1=83 =D0=BD=D0=B0 =D1=83=D1=87=D0=B0=
=D1=81=D1=82=D0=B8=D0=B5 =D0=B8 =D0=BD=D0=B0=D0=BF=D1=80=D0=B0=D0=B2=D0=
=B8=D1=82 =D0=BF=D1=80=D0=B8=D0=B3=D0=BB=D0=B0=D1=88=D0=B5=D0=BD=D0=B8=
=D0=B5.

=D0=92=D1=81=D0=B5 =D0=BD=D0=BE=D0=B2=D0=BE=D1=81=D1=82=D0=B8

=D0=98=D0=BD=D1=82=D0=B5=D1=80=D0=B5=D1=81=D0=BD=D0=BE=D0=B5

=D0=A4=D0=B8=D0=B7=D0=B8=D0=BE=D0=BB=D0=BE=D0=B3=D0=B8=D1=8F =D1=87=D1=
=83=D0=B2=D1=81=D1=82=D0=B2: =D1=8D=D0=B2=D0=BE=D0=BB=D1=8E=D1=86=D0=B8=
=D1=8F =D0=B8=D0=B7=D1=83=D1=87=D0=B5=D0=BD=D0=B8=D1=8F =D1=8D=D0=BC=D0=
=BE=D1=86=D0=B8=D0=B9 =D0=BE=D1=82 =D0=94=D0=B0=D1=80=D0=B2=D0=B8=D0=BD=
=D0=B0 =D0=B4=D0=BE =D0=B4=D0=B5=D1=82=D0=B5=D0=BA=D1=82=D0=BE=D1=80=D0=
=B0 =D0=BB=D0=B6=D0=B8
=D0=9D=D0=B0=D1=83=D1=87=D0=BD=D1=8B=D0=B9 =D1=81=D0=BE=D1=82=D1=80=D1=
=83=D0=B4=D0=BD=D0=B8=D0=BA =D0=98=D0=BD=D1=81=D1=82=D0=B8=D1=82=D1=83=
=D1=82=D0=B0 =D0=BA=D0=BE=D0=B3=D0=BD=D0=B8=D1=82=D0=B8=D0=B2=D0=BD=D1=
=8B=D1=85 =D0=BD=D0=B5=D0=B9=D1=80=D0=BE=D0=BD=D0=B0=D1=83=D0=BA =D0=92=
=D1=8B=D1=88=D0=BA=D0=B8 =D0=92=D0=BB=D0=B0=D0=B4=D0=B8=D0=BC=D0=B8=D1=
=80 =D0=9A=D0=BE=D1=81=D0=BE=D0=BD=D0=BE=D0=B3=D0=BE=D0=B2 =D1=80=D0=B0=
=D1=81=D1=81=D0=BA=D0=B0=D0=B7=D0=B0=D0=BB (https://iq.hse.ru/news/35960630=
1.html) IQ.HSE, =D0=BA=D0=B0=D0=BA =D1=80=D0=B0=D1=81=D0=BF=D0=BE=D0=B7=
=D0=BD=D0=B0=D0=B2=D0=B0=D1=82=D1=8C =D0=B8 =D0=B8=D0=B7=D1=83=D1=87=D0=
=B0=D1=82=D1=8C =D1=8D=D0=BC=D0=BE=D1=86=D0=B8=D0=B8, =D0=B8 =D1=87=D1=
=82=D0=BE =D0=BC=D0=BE=D0=B3=D1=83=D1=82 =D1=81=D0=BE=D0=BE=D0=B1=D1=89=
=D0=B8=D1=82=D1=8C =D1=84=D0=B8=D0=B7=D0=B8=D0=BE=D0=BB=D0=BE=D0=B3=D0=
=B8=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D0=B5 =D0=BF=D0=BE=D0=BA=D0=B0=D0=B7=
=D0=B0=D1=82=D0=B5=D0=BB=D0=B8 =D0=BE=D0=B1 =D1=8D=D0=BC=D0=BE=D1=86=D0=
=B8=D0=BE=D0=BD=D0=B0=D0=BB=D1=8C=D0=BD=D0=BE=D0=BC =D1=81=D0=BE=D1=81=
=D1=82=D0=BE=D1=8F=D0=BD=D0=B8=D0=B8 =D1=87=D0=B5=D0=BB=D0=BE=D0=B2=D0=
=B5=D0=BA=D0=B0.

=D0=A2=D0=B5=D0=B0=D1=82=D1=80 =D0=9D=D0=98=D0=A3 =D0=92=D0=A8=D0=AD =D0=
=BF=D1=80=D0=BE=D0=B2=D0=B5=D0=B4=D0=B5=D1=82 =D0=BE=D0=BD=D0=BB=D0=B0=
=D0=B9=D0=BD-=D1=81=D0=BF=D0=B5=D0=BA=D1=82=D0=B0=D0=BA=D0=BB=D0=B8
=D0=A2=D0=B5=D0=B0=D1=82=D1=80 =D0=BF=D1=80=D0=B8=D0=B3=D0=BB=D0=B0=D1=
=88=D0=B0=D0=B5=D1=82 =D0=B2=D0=B0=D1=81 =D0=BD=D0=B5 =D1=82=D0=BE=D0=BB=
=D1=8C=D0=BA=D0=BE =D1=81=D1=82=D0=B0=D1=82=D1=8C =D0=B7=D1=80=D0=B8=D1=
=82=D0=B5=D0=BB=D1=8F=D0=BC=D0=B8, =D0=BD=D0=BE =D0=B8 =D1=83=D1=87=D0=
=B0=D1=81=D1=82=D0=BD=D0=B8=D0=BA=D0=B0=D0=BC=D0=B8 =D0=BC=D0=B5=D1=80=
=D0=BE=D0=BF=D1=80=D0=B8=D1=8F=D1=82=D0=B8=D0=B9. =D0=9A=D0=B0=D0=B6=D0=
=B4=D1=83=D1=8E =D1=81=D1=83=D0=B1=D0=B1=D0=BE=D1=82=D1=83 =D0=B2=D1=8B =
=D0=BC=D0=BE=D0=B6=D0=B5=D1=82=D0=B5 =D1=81=D0=BC=D0=BE=D1=82=D1=80=D0=
=B5=D1=82=D1=8C =D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D1=81=D0=BF=D0=B5=
=D0=BA=D1=82=D0=B0=D0=BA=D0=BB=D0=B8 =D0=B8=D0=B7 =D1=80=D0=B5=D0=BF=D0=
=B5=D1=80=D1=82=D1=83=D0=B0=D1=80=D0=B0 =D1=82=D0=B5=D0=B0=D1=82=D1=80=
=D0=B0. =D0=A3 =D0=B2=D1=8B=D1=88=D0=BA=D0=B8=D0=BD=D1=86=D0=B5=D0=B2 =
=D0=B1=D1=83=D0=B4=D0=B5=D1=82 =D0=B2=D0=BE=D0=B7=D0=BC=D0=BE=D0=B6=D0=
=BD=D0=BE=D1=81=D1=82=D1=8C =D0=BF=D0=BE=D0=BF=D0=B0=D1=81=D1=82=D1=8C =
=D0=BD=D0=B0 =D1=87=D0=B8=D1=82=D0=BA=D0=B8 =D1=81=D0=BF=D0=B5=D0=BA=D1=
=82=D0=B0=D0=BA=D0=BB=D0=B5=D0=B9, =D0=BF=D0=BE=D0=BD=D0=B0=D0=B1=D0=BB=
=D1=8E=D0=B4=D0=B0=D1=82=D1=8C =D0=B7=D0=B0 =D0=B0=D0=BA=D1=82=D0=B5=D1=
=80=D0=B0=D0=BC=D0=B8 =D0=B8 =D0=B4=D0=B0=D0=B6=D0=B5 =D1=81=D1=82=D0=B0=
=D1=82=D1=8C =D0=BF=D0=BE=D0=BB=D0=BD=D0=BE=D1=86=D0=B5=D0=BD=D0=BD=D1=
=8B=D0=BC=D0=B8 =D1=83=D1=87=D0=B0=D1=81=D1=82=D0=BD=D0=B8=D0=BA=D0=B0=
=D0=BC=D0=B8 =D0=BF=D1=80=D0=BE=D0=B8=D1=81=D1=85=D0=BE=D0=B4=D1=8F=D1=
=89=D0=B5=D0=B3=D0=BE, =D0=BE=D0=B7=D0=B2=D1=83=D1=87=D0=B8=D0=B2 =D1=82=
=D0=B5=D0=BA=D1=81=D1=82 =D0=BB=D1=8E=D0=B1=D0=B8=D0=BC=D0=BE=D0=B3=D0=
=BE =D0=BA=D0=BD=D0=B8=D0=B6=D0=BD=D0=BE=D0=B3=D0=BE =D0=BF=D0=B5=D1=80=
=D1=81=D0=BE=D0=BD=D0=B0=D0=B6=D0=B0. =D0=90 =D0=B5=D1=89=D0=B5 =D0=B0=
=D0=BA=D1=82=D0=B5=D1=80=D1=8B =D0=BF=D1=80=D0=BE=D0=B2=D0=B5=D0=B4=D1=
=83=D1=82 =D0=B7=D0=B0=D0=BD=D1=8F=D1=82=D0=B8=D1=8F =D0=BF=D0=BE =D1=81=
=D1=86=D0=B5=D0=BD=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=BE=D0=B9 =D1=80=D0=
=B5=D1=87=D0=B8. =D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5 =
=D0=BE =D1=80=D0=B5=D0=BF=D0=B5=D1=80=D1=82=D1=83=D0=B0=D1=80=D0=B5 =D0=
=B8 =D1=80=D0=B0=D1=81=D0=BF=D0=B8=D1=81=D0=B0=D0=BD=D0=B8=D0=B8 =D1=81=
=D0=BF=D0=B5=D0=BA=D1=82=D0=B0=D0=BA=D0=BB=D0=B5=D0=B9 =E2=80=94 =D0=B2 =
=D0=BD=D0=B0=D1=88=D0=B5=D0=B9 =D1=81=D1=82=D0=B0=D1=82=D1=8C=D0=B5 (https:=
//www.hse.ru/our/news/360751310.html).

=D0=9E=D0=BF=D1=80=D0=BE=D1=81=D1=8B
=E2=80=93 =D0=98 (https://www.1ka.si/a/257272)=D1=81=D1=81=D0=BB=D0=B5=
=D0=B4=D0=BE=D0=B2=D0=B0=D0=BD=D0=B8=D0=B5 =D0=B2=D0=BE=D1=81=D0=BF=D1=
=80=D0=B8=D1=8F=D1=82=D0=B8=D1=8F =D1=82=D0=B5=D0=BA=D1=81=D1=82=D0=B0 (htt=
p://www.1ka.si/a/257272)
=E2=80=93 =D0=A4=D0=B0=D0=BA=D1=82=D0=BE=D1=80=D1=8B, =D0=BE=D0=BF=D1=80=
=D0=B5=D0=B4=D0=B5=D0=BB=D1=8F=D1=8E=D1=89=D0=B8=D0=B5 =D1=82=D0=B8=D0=
=BF =D0=BF=D0=B8=D1=82=D0=B0=D0=BD=D0=B8=D1=8F =D1=81=D1=82=D1=83=D0=B4=
=D0=B5=D0=BD=D1=82=D0=BE=D0=B2 (https://www.1ka.si/a/276050)
=E2=80=93 =D0=A1=D0=BC=D1=8B=D1=81=D0=BB=D0=BE=D0=B2=D0=B0=D1=8F =D0=B8 =
=D1=8D=D0=BC=D0=BE=D1=86=D0=B8=D0=BE=D0=BD=D0=B0=D0=BB=D1=8C=D0=BD=D0=B0=
=D1=8F =D0=BE=D0=BA=D1=80=D0=B0=D1=81=D0=BA=D0=B0 =D0=B3=D0=BB=D0=B0=D1=
=81=D0=BD=D1=8B=D1=85 =D0=B7=D0=B2=D1=83=D0=BA=D0=BE=D0=B2 =D1=80=D1=83=
=D1=81=D1=81=D0=BA=D0=BE=D0=B3=D0=BE =D1=8F=D0=B7=D1=8B=D0=BA=D0=B0 (https:=
//forms.gle/uoANC1vFjKERNazd6)
=E2=80=93 =D0=A4=D0=B5=D0=BD=D0=BE=D0=BC=D0=B5=D0=BD =D1=81=D0=BE=D0=B2=
=D1=80=D0=B5=D0=BC=D0=B5=D0=BD=D0=BD=D0=BE=D0=B9 =D1=80=D0=B5=D0=BA=D0=
=BB=D0=B0=D0=BC=D1=8B (https://www.1ka.si/a/276635)

=D0=9F=D1=80=D0=B8=D1=81=D1=8B=D0=BB=D0=B0=D0=B9=D1=82=D0=B5 =D1=81=D1=
=81=D1=8B=D0=BB=D0=BA=D1=83 =D0=BD=D0=B0 =D1=81=D0=B2=D0=BE=D0=B9 =D0=BE=
=D0=BF=D1=80=D0=BE=D1=81 =D0=B8 =D0=BA=D1=80=D0=B0=D1=82=D0=BA=D0=BE=D0=
=B5 =D0=BE=D0=BF=D0=B8=D1=81=D0=B0=D0=BD=D0=B8=D0=B5 =D0=B8=D1=81=D1=81=
=D0=BB=D0=B5=D0=B4=D0=BE=D0=B2=D0=B0=D0=BD=D0=B8=D1=8F =D0=B2 =D1=84=D0=
=BE=D1=80=D0=BC=D1=83 (https://docs.google.com/forms/d/e/1FAIpQLSegxedWGoTs=
uRQ93ATpygsR538rJeqEt1NQkmKr_Em91-Y4_w/viewform), =D1=81=D0=B0=D0=BC=D1=
=8B=D0=B5 =D0=B8=D0=BD=D1=82=D0=B5=D1=80=D0=B5=D1=81=D0=BD=D1=8B=D0=B5 =
=D0=B2=D0=BA=D0=BB=D1=8E=D1=87=D0=B8=D0=BC =D0=B2 =D0=BD=D0=B0=D1=88 =D0=
=B4=D0=B0=D0=B9=D0=B4=D0=B6=D0=B5=D1=81=D1=82!

27-29 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F, 1-2 =D0=BC=D0=B0=D1=8F
HSE Cheer: =D1=82=D1=80=D0=B5=D0=BD=D0=B8=D1=80=D0=BE=D0=B2=D0=BA=D0=B8 =
=D0=BE=D1=82 =D0=BA=D0=BE=D0=BC=D0=B0=D0=BD=D0=B4=D1=8B =D0=BF=D0=BE =D1=
=87=D0=B8=D1=80=D0=BB=D0=B8=D0=B4=D0=B8=D0=BD=D0=B3=D1=83
=D0=9A=D0=B0=D0=B6=D0=B4=D1=8B=D0=B9 =D0=B4=D0=B5=D0=BD=D1=8C =D0=B2 18:00 =
=D0=BA=D1=80=D0=BE=D0=BC=D0=B5 =D1=87=D0=B5=D1=82=D0=B2=D0=B5=D1=80=D0=
=B3=D0=B0 =D0=B8 =D0=B2=D0=BE=D1=81=D0=BA=D1=80=D0=B5=D1=81=D0=B5=D0=BD=
=D1=8C=D1=8F =D0=BA=D0=BE=D0=BC=D0=B0=D0=BD=D0=B4=D0=B0 =D0=BF=D0=BE =D1=
=87=D0=B8=D1=80=D0=BB=D0=B8=D0=B4=D0=B8=D0=BD=D0=B3=D1=83 =D0=BF=D1=80=
=D0=BE=D0=B2=D0=BE=D0=B4=D0=B8=D1=82 =D1=82=D1=80=D0=B5=D0=BD=D0=B8=D1=
=80=D0=BE=D0=B2=D0=BA=D0=B8. =D0=92 =D0=BF=D1=80=D0=BE=D0=B3=D1=80=D0=B0=
=D0=BC=D0=BC=D0=B5: =D1=80=D0=B0=D1=81=D1=82=D1=8F=D0=B6=D0=BA=D0=B0, =
=D0=B2=D1=8B=D1=81=D0=BE=D0=BA=D0=BE=D0=B8=D0=BD=D1=82=D0=B5=D0=BD=D1=81=
=D0=B8=D0=B2=D0=BD=D1=8B=D0=B5 =D1=82=D1=80=D0=B5=D0=BD=D0=B8=D1=80=D0=
=BE=D0=B2=D0=BA=D0=B8 HIIT =D0=B8 cheer =D0=BF=D0=BE=D0=B4=D0=BA=D0=B0=
=D1=87=D0=BA=D0=B0.
=D0=9F=D1=80=D0=B8=D1=81=D0=BE=D0=B5=D0=B4=D0=B8=D0=BD=D1=8F=D0=B9=D1=82=
=D0=B5=D1=81=D1=8C =D0=BA =D1=82=D1=80=D0=B5=D0=BD=D0=B8=D1=80=D0=BE=D0=
=B2=D0=BA=D0=B0=D0=BC =D0=B2 Instagram-=D0=B0=D0=BA=D0=BA=D0=B0=D1=83=D0=
=BD=D1=82=D0=B5 (https://www.instagram.com/hse.cheer/) =D0=BA=D0=BE=D0=
=BC=D0=B0=D0=BD=D0=B4=D1=8B

27 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F-1 =D0=BC=D0=B0=D1=8F
=D0=A2=D1=80=D0=B5=D0=BD=D0=B8=D1=80=D0=BE=D0=B2=D0=BA=D0=B8 =D0=BE=D1=
=82 HSE Dance
HSE Dance =D1=81 =D0=BF=D0=BE=D0=BD=D0=B5=D0=B4=D0=B5=D0=BB=D1=8C=D0=BD=
=D0=B8=D0=BA=D0=B0 =D0=BF=D0=BE =D0=BF=D1=8F=D1=82=D0=BD=D0=B8=D1=86=D1=
=83 =D0=B4=D0=B2=D0=B0 =D1=80=D0=B0=D0=B7=D0=B0 =D0=B2 =D0=B4=D0=B5=D0=
=BD=D1=8C =D0=BF=D1=80=D0=BE=D0=B2=D0=BE=D0=B4=D1=8F=D1=82 =D0=B7=D0=B0=
=D0=BD=D1=8F=D1=82=D0=B8=D1=8F =D0=BF=D0=BE =D0=BB=D0=B0=D1=82=D0=B8=D0=
=BD=D0=B5, =D1=85=D0=B8=D0=BF-=D1=85=D0=BE=D0=BF=D1=83, =D1=81=D1=82=D1=
=80=D0=B5=D1=82=D1=87=D0=B8=D0=BD=D0=B3=D1=83, =D0=B4=D0=B6=D0=B0=D0=B7-=
=D1=84=D0=B0=D0=BD=D0=BA=D1=83, =D0=B9=D0=BE=D0=B3=D0=B5 =D0=B8 =D0=BD=
=D0=B5 =D1=82=D0=BE=D0=BB=D1=8C=D0=BA=D0=BE. =D0=A1=D0=BB=D0=B5=D0=B4=D0=
=B8=D1=82=D0=B5 =D0=B7=D0=B0 =D1=80=D0=B0=D1=81=D0=BF=D0=B8=D1=81=D0=B0=
=D0=BD=D0=B8=D0=B5=D0=BC =D0=B8 =D1=82=D1=80=D0=B0=D0=BD=D1=81=D0=BB=D1=
=8F=D1=86=D0=B8=D0=B5=D0=B9 =D1=82=D1=80=D0=B5=D0=BD=D0=B8=D1=80=D0=BE=
=D0=B2=D0=BE=D0=BA =D0=B2 Instagram-=D0=B0=D0=BA=D0=BA=D0=B0=D1=83=D0=BD=
=D1=82=D0=B5 (https://www.instagram.com/hd_hse/)

28-30 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F
=D0=A2=D1=83=D1=80=D0=BD=D0=B8=D1=80 =D0=BF=D0=BE =D0=BA=D0=B8=D0=B1=D0=
=B5=D1=80=D1=81=D0=BF=D0=BE=D1=80=D1=82=D1=83
=D0=9D=D0=B0 =D1=8D=D1=82=D0=BE=D0=B9 =D0=BD=D0=B5=D0=B4=D0=B5=D0=BB=D0=
=B5 =D0=BF=D1=80=D0=BE=D0=B9=D0=B4=D1=83=D1=82 =D0=BF=D0=BB=D0=B5=D0=B9-=
=D0=BE=D1=84=D1=84=D1=8B =D0=B8 =D1=84=D0=B8=D0=BD=D0=B0=D0=BB=D1=8B =D0=
=BA=D0=B8=D0=B1=D0=B5=D1=80=D1=81=D0=BF=D0=BE=D1=80=D1=82=D0=B8=D0=B2=D0=
=BD=D0=BE=D0=B3=D0=BE =D1=82=D1=83=D1=80=D0=BD=D0=B8=D1=80=D0=B0 =D0=92=
=D1=8B=D1=88=D0=BA=D0=B8 =E2=80=94 HS, Dota 2, CS:Go, Fifa 20.
=D0=A0=D0=B0=D1=81=D0=BF=D0=B8=D1=81=D0=B0=D0=BD=D0=B8=D0=B5 (https://vk.co=
m/@cyberhse-crown-4-osnovnaya-informaciya)

28 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F-2 =D0=BC=D0=B0=D1=8F
Mental Health Spring
=D0=9D=D0=B0 =D1=8D=D1=82=D0=BE=D0=B9 =D0=BD=D0=B5=D0=B4=D0=B5=D0=BB=D0=
=B5 =D0=B2=D0=B0=D1=81 =D0=B6=D0=B4=D0=B5=D1=82 =D0=BC=D0=B5=D0=B4=D0=B8=
=D1=82=D0=B0=D1=86=D0=B8=D1=8F =D0=BE=D1=81=D0=BE=D0=B7=D0=BD=D0=B0=D0=
=BD=D0=BD=D0=BE=D1=81=D1=82=D0=B8, =D0=BF=D1=81=D0=B8=D1=85=D0=BE=D0=B9=
=D0=BE=D0=B3=D0=B0, =D1=80=D0=B0=D0=B7=D0=B3=D0=BE=D0=B2=D0=BE=D1=80 =D0=
=BF=D1=80=D0=BE =D1=8D=D0=BC=D0=BE=D1=86=D0=B8=D0=BE=D0=BD=D0=B0=D0=BB=
=D1=8C=D0=BD=D0=BE=D0=B5 =D0=B2=D1=8B=D0=B3=D0=BE=D1=80=D0=B0=D0=BD=D0=
=B8=D0=B5, =D0=B7=D0=B0=D0=BD=D1=8F=D1=82=D0=B8=D0=B5 =D0=BF=D0=BE =D1=
=80=D0=B0=D0=B7=D0=B2=D0=B8=D1=82=D0=B8=D1=8E EQ, =D0=B0 =D1=82=D0=B0=D0=
=BA=D0=B6=D0=B5 =D0=BB=D0=B5=D0=BA=D1=86=D0=B8=D0=B8 =D0=BE =D1=82=D0=BE=
=D0=BC, =D0=BA=D0=B0=D0=BA =D1=81=D0=BF=D1=80=D0=B0=D0=B2=D0=BB=D1=8F=D1=
=82=D1=8C=D1=81=D1=8F =D1=81 =D0=BE=D0=B4=D0=B8=D0=BD=D0=BE=D1=87=D0=B5=
=D1=81=D1=82=D0=B2=D0=BE=D0=BC =D0=B8 =D0=B2=D0=BB=D0=B8=D1=8F=D1=82=D1=
=8C =D0=BD=D0=B0 =D1=81=D0=B2=D0=BE=D0=B8 =D1=8D=D0=BC=D0=BE=D1=86=D0=B8=
=D0=B8 =D1=81 =D0=BF=D0=BE=D0=BC=D0=BE=D1=89=D1=8C=D1=8E =D0=BC=D1=83=D0=
=B7=D1=8B=D0=BA=D0=B8.
=D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5 (https://www.hse.ru/=
cpc/mhspring)

28 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F, 19:00
=D0=A1=D0=BF=D0=B5=D1=86=D0=B8=D1=84=D0=B8=D0=BA=D0=B0 =D1=80=D0=B0=D0=
=B1=D0=BE=D1=82=D1=8B PR =D0=B2 =D1=83=D1=81=D0=BB=D0=BE=D0=B2=D0=B8=D1=
=8F=D1=85 =D0=BA=D1=80=D0=B8=D0=B7=D0=B8=D1=81=D0=B0 =D0=B2 =D0=A1=D0=9D=
=D0=93 =D0=B8 =D0=A0=D0=BE=D1=81=D1=81=D0=B8=D0=B8
=D0=92 =D0=BF=D1=80=D1=8F=D0=BC=D0=BE=D0=BC =D1=8D=D1=84=D0=B8=D1=80=D0=
=B5 =D1=81=D0=BE=D0=BE=D0=B1=D1=89=D0=B5=D1=81=D1=82=D0=B2=D0=B0 =D0=B2=
=D1=8B=D0=BF=D1=83=D1=81=D0=BA=D0=BD=D0=B8=D0=BA=D0=BE=D0=B2 =D0=B2 Faceboo=
k =D1=80=D1=83=D0=BA=D0=BE=D0=B2=D0=BE=D0=B4=D0=B8=D1=82=D0=B5=D0=BB=D1=
=8C =D0=BD=D0=B0=D0=BF=D1=80=D0=B0=D0=B2=D0=BB=D0=B5=D0=BD=D0=B8=D1=8F infl=
uence-=D0=BC=D0=B0=D1=80=D0=BA=D0=B5=D1=82=D0=B8=D0=BD=D0=B3=D0=B0 ivi.ru =
=D0=90=D0=BD=D0=BD=D0=B0 =D0=A7=D0=B5=D1=80=D0=BA=D0=B0=D1=81=D0=BE=D0=
=B2=D0=B0 =D0=B8 =D0=B4=D0=B8=D1=80=D0=B5=D0=BA=D1=82=D0=BE=D1=80 Like PR A=
gency =D0=90=D0=BD=D0=BD=D0=B0 =D0=91=D0=BE=D1=80=D0=B8=D1=81=D0=BE=D0=
=B2=D0=B0 =D1=80=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B6=D1=83=D1=82 =D0=BE =
=D1=81=D0=BF=D0=B5=D1=86=D0=B8=D1=84=D0=B8=D0=BA=D0=B5 =D1=80=D0=B0=D0=
=B1=D0=BE=D1=82=D1=8B =D0=B2 =D1=81=D1=84=D0=B5=D1=80=D0=B5 =D1=81=D0=B2=
=D1=8F=D0=B7=D0=B5=D0=B9 =D1=81 =D0=BE=D0=B1=D1=89=D0=B5=D1=81=D1=82=D0=
=B2=D0=B5=D0=BD=D0=BD=D0=BE=D1=81=D1=82=D1=8C =D0=B2 =D1=83=D1=81=D0=BB=
=D0=BE=D0=B2=D0=B8=D1=8F=D1=85 =D0=BA=D1=80=D0=B8=D0=B7=D0=B8=D1=81=D0=
=B0. =D0=9F=D1=80=D0=B8=D1=81=D0=BE=D0=B5=D0=B4=D0=B8=D0=BD=D1=8F=D0=B9=
=D1=82=D0=B5=D1=81=D1=8C =D0=BA =D1=82=D1=80=D0=B0=D0=BD=D1=81=D0=BB=D1=
=8F=D1=86=D0=B8=D0=B8, =D1=87=D1=82=D0=BE=D0=B1=D1=8B =D1=83=D0=B7=D0=BD=
=D0=B0=D1=82=D1=8C =D0=BE=D0=B1 =D0=B0=D0=BA=D1=82=D1=83=D0=B0=D0=BB=D1=
=8C=D0=BD=D1=8B=D1=85 PR-=D1=81=D1=82=D1=80=D0=B0=D1=82=D0=B5=D0=B3=D0=
=B8=D1=8F=D1=85 =D0=BE=D1=82 =D1=8D=D0=BA=D1=81=D0=BF=D0=B5=D1=80=D1=82=
=D0=BE=D0=B2 =D0=B2 =D1=81=D0=B2=D0=BE=D0=B5=D0=B9 =D0=BE=D0=B1=D0=BB=D0=
=B0=D1=81=D1=82=D0=B8.
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F (https:/=
/alumni.hse.ru/marathon270105/#registration)

29 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F, 18:00
=D0=9F=D1=80=D0=B0=D0=BA=D1=82=D0=B8=D0=BA=D0=B0 =D0=BD=D0=B5=D0=BC=D0=
=B5=D1=86=D0=BA=D0=BE=D0=B3=D0=BE =D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD
=D0=A0=D0=B0=D0=B7=D0=B3=D0=BE=D0=B2=D0=BE=D1=80=D0=BD=D1=8B=D0=B9 =D0=
=BA=D0=BB=D1=83=D0=B1 =D0=92=D1=8B=D1=88=D0=BA=D0=B8 Poboltaem? =D0=B8 =
=D0=BA=D0=BB=D1=83=D0=B1 =D0=BB=D1=8E=D0=B1=D0=B8=D1=82=D0=B5=D0=BB=D0=
=B5=D0=B9 =D0=BD=D0=B5=D0=BC=D0=B5=D1=86=D0=BA=D0=BE=D0=B3=D0=BE gut geD-A-=
CHt! =D0=BF=D1=80=D0=B8=D0=B3=D0=BB=D0=B0=D1=88=D0=B0=D1=8E=D1=82 =D0=BE=
=D0=B1=D1=81=D1=83=D0=B4=D0=B8=D1=82=D1=8C =D1=82=D0=B5=D0=BC=D1=83 =D0=
=BC=D1=83=D0=B7=D1=8B=D0=BA=D0=B8.
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F (https:/=
/us02web.zoom.us/meeting/register/tZAvdu-prD0oGNzGcdMIoOIY8Ldksx5KnoNf)

29 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F, 18:00
=D0=9C=D0=B0=D1=81=D1=82=D0=B5=D1=80-=D0=BA=D0=BB=D0=B0=D1=81=D1=81 =C2=
=AB=D0=9F=D1=80=D0=B8=D0=BD=D0=B8=D0=BC=D0=B0=D0=B5=D0=BC =D1=80=D0=B5=
=D1=88=D0=B5=D0=BD=D0=B8=D1=8F =D0=BD=D0=B0 =D0=BE=D1=81=D0=BD=D0=BE=D0=
=B2=D0=B5 =D0=B4=D0=B0=D0=BD=D0=BD=D1=8B=D1=85=C2=BB
=D0=92=D0=B5=D0=B4=D1=83=D1=89=D0=B8=D0=B9 =D1=8D=D0=BA=D1=81=D0=BF=D0=
=B5=D1=80=D1=82 =D0=BF=D0=BE =D0=BF=D0=BE unit-=D1=8D=D0=BA=D0=BE=D0=BD=
=D0=BE=D0=BC=D0=B8=D0=BA=D0=B5 =D0=94=D0=B0=D0=BD=D0=B8=D0=B8=D0=BB =D0=
=A5=D0=B0=D0=BD=D0=B8=D0=BD =D1=80=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B6=
=D0=B5=D1=82, =D0=BA=D0=B0=D0=BA =D1=80=D0=B0=D1=81=D1=81=D1=87=D0=B8=D1=
=82=D1=8B=D0=B2=D0=B0=D1=82=D1=8C =D1=8D=D1=84=D1=84=D0=B5=D0=BA=D1=82=
=D0=B8=D0=B2=D0=BD=D0=BE=D1=81=D1=82=D1=8C =D0=B1=D0=B8=D0=B7=D0=BD=D0=
=B5=D1=81=D0=B0, =D0=B8=D1=81=D0=BF=D0=BE=D0=BB=D1=8C=D0=B7=D1=83=D1=8F =
=D1=81=D0=B8=D1=81=D1=82=D0=B5=D0=BC=D1=83 =D0=BC=D0=B5=D1=82=D1=80=D0=
=B8=D0=BA =D0=B8 =D0=BA=D0=BE=D0=B3=D0=BE=D1=80=D1=82=D0=BD=D1=8B=D0=B9 =
=D0=B0=D0=BD=D0=B0=D0=BB=D0=B8=D0=B7.
=D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5 (https://hseinc.ru/m=
kd)

30 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F, 15:00
=D0=93=D0=BB=D0=BE=D0=B1=D0=B0=D0=BB=D1=8C=D0=BD=D1=8B=D0=B9 =D1=81=D0=
=B5=D0=BC=D0=B8=D0=BD=D0=B0=D1=80 =C2=AB=D0=97=D0=B0=D1=80=D0=B0=D0=B7=
=D0=BD=D1=8B=D0=B5 =D0=B3=D0=BE=D1=80=D0=BE=D0=B4=D0=B0: =D0=9F=D0=BE=D0=
=BD=D0=B8=D0=BC=D0=B0=D0=BD=D0=B8=D0=B5 =D0=9F=D0=B0=D0=BD=D0=B4=D0=B5=
=D0=BC=D0=B8=D0=B8=C2=BB
=D0=93=D0=BE=D1=82=D0=BE=D0=B2=D1=8B =D0=BB=D0=B8 =D0=BC=D1=8B =D0=BF=D1=
=80=D0=B8=D0=BD=D1=8F=D1=82=D1=8C =D0=B3=D0=BB=D0=BE=D0=B1=D0=B0=D0=BB=
=D1=8C=D0=BD=D1=8B=D0=B9 =D0=B2=D1=8B=D0=B7=D0=BE=D0=B2 =D0=B2=D1=81=D0=
=BF=D1=8B=D1=88=D0=BA=D0=B8 =D0=BF=D0=B0=D0=BD=D0=B4=D0=B5=D0=BC=D0=B8=
=D0=B8 Covid-19? =D0=97=D0=BD=D0=B0=D0=B5=D0=BC =D0=BB=D0=B8 =D0=BC=D1=
=8B, =D0=BA=D0=B0=D0=BA =D0=BC=D0=B8=D0=BA=D1=80=D0=BE=D0=B1=D1=8B, =D0=
=BC=D0=B8=D0=B3=D1=80=D0=B0=D1=86=D0=B8=D1=8F =D0=B8 =D0=BC=D0=B5=D0=B3=
=D0=B0=D0=BF=D0=BE=D0=BB=D0=B8=D1=81=D1=8B =D1=81=D0=B2=D1=8F=D0=B7=D0=
=B0=D0=BD=D1=8B =D0=B4=D1=80=D1=83=D0=B3 =D1=81 =D0=B4=D1=80=D1=83=D0=B3=
=D0=BE=D0=BC? =D0=9C=D0=BE=D0=B6=D0=B5=D1=82 =D0=BB=D0=B8 =D0=B8=D1=81=
=D0=BA=D1=83=D1=81=D1=81=D1=82=D0=B2=D0=BE =D0=BF=D0=BE=D0=BC=D0=BE=D1=
=87=D1=8C =D0=BB=D1=83=D1=87=D1=88=D0=B5 =D0=BF=D0=BE=D0=BD=D1=8F=D1=82=
=D1=8C =D0=B3=D0=BB=D0=BE=D0=B1=D0=B0=D0=BB=D1=8C=D0=BD=D1=8B=D0=B5 =D0=
=B8=D0=BD=D1=84=D0=B5=D0=BA=D1=86=D0=B8=D0=BE=D0=BD=D0=BD=D1=8B=D0=B5 =
=D0=B7=D0=B0=D0=B1=D0=BE=D0=BB=D0=B5=D0=B2=D0=B0=D0=BD=D0=B8=D1=8F =D0=
=B8=D0=BB=D0=B8 =D0=B8=D0=B7=D1=83=D1=87=D0=B8=D1=82=D1=8C, =D0=BA=D0=B0=
=D0=BA =D0=BE=D0=BD=D0=B8 =D0=BF=D0=B5=D1=80=D0=B5=D0=BC=D0=B5=D1=89=D0=
=B0=D1=8E=D1=82=D1=81=D1=8F =D0=B2 =D0=BF=D1=80=D0=BE=D1=81=D1=82=D1=80=
=D0=B0=D0=BD=D1=81=D1=82=D0=B2=D0=B5 =D0=B8 =D0=B2=D1=80=D0=B5=D0=BC=D0=
=B5=D0=BD=D0=B8? =D0=9D=D0=B0 =D0=B2=D0=B5=D0=B1=D0=B8=D0=BD=D0=B0=D1=80=
=D0=B5 =D0=BE=D0=B1=D1=81=D1=83=D0=B4=D0=B8=D0=BC =D1=8D=D1=82=D0=B8 =D0=
=B2=D0=BE=D0=BF=D1=80=D0=BE=D1=81=D1=8B =D1=81 =D1=85=D1=83=D0=B4=D0=BE=
=D0=B6=D0=BD=D0=B8=D0=BA=D0=B0=D0=BC=D0=B8, =D0=BA=D1=83=D1=80=D0=B0=D1=
=82=D0=BE=D1=80=D0=B0=D0=BC=D0=B8, =D0=B8=D1=81=D1=81=D0=BB=D0=B5=D0=B4=
=D0=BE=D0=B2=D0=B0=D1=82=D0=B5=D0=BB=D1=8F=D0=BC=D0=B8 =D0=B8 =D0=BA=D1=
=83=D0=BB=D1=8C=D1=82=D1=83=D1=80=D0=BD=D1=8B=D0=BC=D0=B8 =D0=BF=D1=80=
=D0=BE=D0=B4=D1=8E=D1=81=D0=B5=D1=80=D0=B0=D0=BC=D0=B8 =D0=BF=D1=80=D0=
=BE=D0=B5=D0=BA=D1=82=D0=B0 =C2=AB=D0=97=D0=B0=D1=80=D0=B0=D0=B7=D0=BD=
=D1=8B=D0=B5 =D0=93=D0=BE=D1=80=D0=BE=D0=B4=D0=B0=C2=BB.
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F (https:/=
/cmd.hse.ru/announcements/359463106.html)

30 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F, 19:00
=C2=AB=D0=97=D0=B0=D1=80=D1=8F=D0=B4 =D0=B4=D0=BE=D0=B1=D1=80=D0=BE=D1=
=82=D1=8B: =D1=87=D1=82=D0=BE =D1=85=D0=BE=D1=80=D0=BE=D1=88=D0=B5=D0=B3=
=D0=BE =D0=BF=D0=BE=D1=81=D0=BC=D0=BE=D1=82=D1=80=D0=B5=D1=82=D1=8C =D0=
=BD=D0=B0 =D0=BA=D0=B0=D1=80=D0=B0=D0=BD=D1=82=D0=B8=D0=BD=D0=B5=C2=BB
=D0=95=D1=81=D0=BB=D0=B8 =D0=B2 =D1=87=D0=B5=D1=82=D1=8B=D1=80=D0=B5=D1=
=85 =D1=81=D1=82=D0=B5=D0=BD=D0=B0=D1=85 =D0=B2=D1=8B =D1=83=D0=B6=D0=B5 =
=D0=BF=D0=B5=D1=80=D0=B5=D1=81=D0=BC=D0=BE=D1=82=D1=80=D0=B5=D0=BB=D0=B8 =
=D0=B2=D1=81=D0=B5 =D1=81=D0=B5=D1=80=D0=B8=D0=B0=D0=BB=D1=8B, =D0=BE =
=D0=BA=D0=BE=D1=82=D0=BE=D1=80=D1=8B=D1=85 =D1=81=D0=BB=D1=8B=D1=88=D0=
=B0=D0=BB=D0=B8, =D1=82=D0=BE =D0=BF=D1=80=D0=B8=D1=81=D0=BE=D0=B5=D0=B4=
=D0=B8=D0=BD=D1=8F=D0=B9=D1=82=D0=B5=D1=81=D1=8C =D0=BA =D1=8D=D1=84=D0=
=B8=D1=80=D1=83 =D1=81=D0=BE=D0=BE=D0=B1=D1=89=D0=B5=D1=81=D1=82=D0=B2=
=D0=B0 =D0=B2=D1=8B=D0=BF=D1=83=D1=81=D0=BA=D0=BD=D0=B8=D0=BA=D0=BE=D0=
=B2 =D0=B2 Facebook =D0=B2 =D1=87=D0=B5=D1=82=D0=B2=D0=B5=D1=80=D0=B3. =
=D0=A1=D0=BE=D0=B7=D0=B4=D0=B0=D1=82=D0=B5=D0=BB=D0=B8 Telegram-=D0=BA=
=D0=B0=D0=BD=D0=B0=D0=BB=D0=B0 =D0=BE =D1=84=D0=B8=D0=BB=D1=8C=D0=BC=D0=
=B0=D1=85 =C2=AB=D0=94=D0=B8=D0=B2=D0=B0=D0=BD=D0=BD=D1=8B=D0=B5 =D0=BA=
=D0=B0=D1=80=D1=82=D0=BE=D1=88=D0=BA=D0=B8=C2=BB =D1=80=D0=B0=D1=81=D1=
=81=D0=BA=D0=B0=D0=B6=D1=83=D1=82, =D1=87=D1=82=D0=BE =D0=B4=D0=B5=D0=BB=
=D0=B0=D1=82=D1=8C =D0=B4=D0=B0=D0=BB=D1=8C=D1=88=D0=B5.
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F (https:/=
/alumni.hse.ru/marathon270105/#registration)

2 =D0=BC=D0=B0=D1=8F, 18:00
=D0=98=D0=BD=D1=82=D0=B5=D1=80=D0=B0=D0=BA=D1=82=D0=B8=D0=B2=D0=BD=D0=B0=
=D1=8F =D1=87=D0=B8=D1=82=D0=BA=D0=B0 =D1=80=D0=B0=D1=81=D1=81=D0=BA=D0=
=B0=D0=B7=D0=BE=D0=B2 =D0=A7=D0=B5=D1=85=D0=BE=D0=B2=D0=B0 =D1=81 =D0=B0=
=D0=BA=D1=82=D0=B5=D1=80=D0=B0=D0=BC=D0=B8 =D0=A2=D0=B5=D0=B0=D1=82=D1=
=80=D0=B0 =D0=9D=D0=98=D0=A3 =D0=92=D0=A8=D0=AD
=D0=95=D1=81=D0=BB=D0=B8 =D0=B2=D0=B0=D0=BC =D0=BA=D0=BE=D0=B3=D0=B4=D0=
=B0-=D0=BD=D0=B8=D0=B1=D1=83=D0=B4=D1=8C =D1=85=D0=BE=D1=82=D0=B5=D0=BB=
=D0=BE=D1=81=D1=8C =D0=BF=D0=BE=D1=87=D1=83=D0=B2=D1=81=D1=82=D0=B2=D0=
=BE=D0=B2=D0=B0=D1=82=D1=8C =D1=81=D0=B5=D0=B1=D1=8F =D0=BD=D0=B0=D1=81=
=D1=82=D0=BE=D1=8F=D1=89=D0=B8=D0=BC =D0=B0=D0=BA=D1=82=D0=B5=D1=80=D0=
=BE=D0=BC, =D0=A2=D0=B5=D0=B0=D1=82=D1=80 =D0=9D=D0=98=D0=A3 =D0=92=D0=
=A8=D0=AD =D0=BF=D1=80=D0=B5=D0=B4=D0=BE=D1=81=D1=82=D0=B0=D0=B2=D0=B8=
=D1=82 =D0=B2=D0=B0=D0=BC =D1=88=D0=B0=D0=BD=D1=81 =D0=BF=D0=BE=D0=B1=D1=
=8B=D0=B2=D0=B0=D1=82=D1=8C =D0=BD=D0=B0 =D0=B2=D0=B8=D1=80=D1=82=D1=83=
=D0=B0=D0=BB=D1=8C=D0=BD=D0=BE=D0=B9 =D1=81=D1=86=D0=B5=D0=BD=D0=B5 =D0=
=B8 =D0=B2=D0=BC=D0=B5=D1=81=D1=82=D0=B5 =D1=81 =D0=B0=D0=BA=D1=82=D0=B5=
=D1=80=D0=B0=D0=BC=D0=B8 =D1=82=D0=B5=D0=B0=D1=82=D1=80=D0=B0 =D0=BF=D1=
=80=D0=BE=D1=87=D0=B8=D1=82=D0=B0=D1=82=D1=8C =D0=B8 =D0=BE=D0=B1=D1=8B=
=D0=B3=D1=80=D0=B0=D1=82=D1=8C =D1=80=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=
=B7=D1=8B =D0=90.=D0=9F. =D0=A7=D0=B5=D1=85=D0=BE=D0=B2=D0=B0. =D0=A1 =
=D0=BF=D0=BE=D0=BC=D0=BE=D1=89=D1=8C=D1=8E =D0=B3=D0=B5=D0=BD=D0=B5=D1=
=80=D0=B0=D1=82=D0=BE=D1=80=D0=B0 =D1=81=D0=BB=D1=83=D1=87=D0=B0=D0=B9=
=D0=BD=D1=8B=D1=85 =D1=87=D0=B8=D1=81=D0=B5=D0=BB =D0=B2=D1=8B=D0=B1=D0=
=B5=D1=80=D1=83=D1=82 10 =D1=83=D1=87=D0=B0=D1=81=D1=82=D0=BD=D0=B8=D0=
=BA=D0=BE=D0=B2, =D0=BA=D0=BE=D1=82=D0=BE=D1=80=D1=8B=D0=B5 =D0=BF=D0=BE=
=D0=BB=D1=83=D1=87=D0=B0=D1=82 =D0=BF=D0=BE=D0=BB=D0=BD=D0=BE=D1=86=D0=
=B5=D0=BD=D0=BD=D1=8B=D0=B5 =D1=80=D0=BE=D0=BB=D0=B8, =D1=82=D0=B5=D0=BA=
=D1=81=D1=82 =D0=B8 =D1=81=D0=BC=D0=BE=D0=B3=D1=83=D1=82 =D0=BF=D1=80=D0=
=B8=D0=BD=D1=8F=D1=82=D1=8C =D1=83=D1=87=D0=B0=D1=81=D1=82=D0=B8=D0=B5 =
=D0=B2 =D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D0=B5 =C2=AB=D0=A1=D1=83=D0=
=B1=D0=B1=D0=BE=D1=82=D1=8B =D1=81 =D0=A2=D0=B5=D0=B0=D1=82=D1=80=D0=BE=
=D0=BC =D0=9D=D0=98=D0=A3 =D0=92=D0=A8=D0=AD=C2=BB.
=D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=BE=D1=81=D1=82=D0=B8 (https:/=
/vk.com/hsetheatre)

=D0=A2=D0=B0=D0=BA=D0=B6=D0=B5, =D0=B5=D1=81=D0=BB=D0=B8 =D1=83 =D0=B2=
=D0=B0=D1=81 =D0=B5=D1=81=D1=82=D1=8C =D0=BC=D0=B0=D0=BB=D0=B5=D0=BD=D1=
=8C=D0=BA=D0=B8=D0=B5 =D0=B1=D1=80=D0=B0=D1=82 =D0=B8=D0=BB=D0=B8 =D1=81=
=D0=B5=D1=81=D1=82=D1=80=D0=B0, =D0=A2=D0=B5=D0=B0=D1=82=D1=80 =D0=BF=D1=
=80=D0=B5=D0=B4=D0=BB=D0=B0=D0=B3=D0=B0=D0=B5=D1=82 =D0=B7=D0=B0=D0=BF=
=D0=B8=D1=81=D0=B0=D1=82=D1=8C =D1=81 =D0=BD=D0=B8=D0=BC=D0=B8 =D0=B2=D0=
=B8=D0=B4=D0=B5=D0=BE =D1=81=D0=BE =D1=81=D1=82=D0=B8=D1=85=D0=B0=D0=BC=
=D0=B8 =D0=B8=D0=BB=D0=B8 =D0=BF=D1=80=D0=BE=D0=B7=D0=BE=D0=B9 =D0=BE =
=D0=B2=D0=BE=D0=B9=D0=BD=D0=B5 =D0=B2 =D1=80=D0=B0=D0=BC=D0=BA=D0=B0=D1=
=85 =D0=BF=D1=80=D0=BE=D0=B5=D0=BA=D1=82=D0=B0 #=D0=B2=D1=8B=D1=88=D0=BA=
=D0=B075=D0=BB=D0=B5=D1=82=D0=BF=D0=BE=D0=B1=D0=B5=D0=B4=D1=8B. =D0=A2=
=D0=B5=D0=B0=D1=82=D1=80 =D0=9D=D0=98=D0=A3 =D0=92=D0=A8=D0=AD =D0=BE=D0=
=BF=D1=83=D0=B1=D0=BB=D0=B8=D0=BA=D1=83=D0=B5=D1=82 =D0=B8=D1=85 =D0=BD=
=D0=B0 =D1=81=D0=B2=D0=BE=D0=B5=D0=B9 =D1=81=D1=82=D1=80=D0=B0=D0=BD=D0=
=B8=D1=86=D0=B5 =D0=B2 Instagram (https://www.instagram.com/hsetheatre). =
=D0=9E=D1=82=D0=BF=D1=80=D0=B0=D0=B2=D0=BB=D1=8F=D0=B9=D1=82=D0=B5 =D0=
=B2=D0=B8=D0=B4=D0=B5=D0=BE =D0=BD=D0=B0 =D0=BF=D0=BE=D1=87=D1=82=D1=83 isi=
rotinskaya@hse.ru =D0=B4=D0=BE 5 =D0=BC=D0=B0=D1=8F.

=D0=9F=D1=80=D0=BE=D0=B5=D0=BA=D1=82 =C2=AB=D0=A6=D0=B8=D1=84=D1=80=D0=
=BE=D0=B2=D0=B0=D1=8F =D0=B6=D1=83=D1=80=D0=BD=D0=B0=D0=BB=D0=B8=D1=81=
=D1=82=D0=B8=D0=BA=D0=B0=C2=BB
=D0=92 =D1=80=D0=B0=D0=BC=D0=BA=D0=B0=D1=85 =D0=BF=D1=80=D0=BE=D0=B5=D0=
=BA=D1=82=D0=B0 (http://www.vremyan.ru/news/proekt__czifrovaya_zhurnalistik=
a_startuet_pri_podderzhke_administraczii_prezidenta_rf.html) =D0=B2=D0=
=B0=D1=81 =D0=B6=D0=B4=D1=83=D1=82 =D1=81=D0=B5=D0=BC=D0=B8=D0=BD=D0=B0=
=D1=80=D1=8B =D0=B8 =D0=B2=D1=8B=D1=81=D1=82=D1=83=D0=BF=D0=BB=D0=B5=D0=
=BD=D0=B8=D1=8F =D0=BB=D0=B8=D0=B4=D0=B5=D1=80=D0=BE=D0=B2 =D1=86=D0=B8=
=D1=84=D1=80=D0=BE=D0=B2=D0=BE=D0=B9 =D1=82=D1=80=D0=B0=D0=BD=D1=81=D1=
=84=D0=BE=D1=80=D0=BC=D0=B0=D1=86=D0=B8=D0=B8, =D1=8D=D0=BA=D1=81=D0=BF=
=D0=B5=D1=80=D1=82=D0=BE=D0=B2 =D0=B8 =D0=B6=D1=83=D1=80=D0=BD=D0=B0=D0=
=BB=D0=B8=D1=81=D1=82=D0=BE=D0=B2 =D1=82=D0=B5=D1=85=D0=BD=D0=BE=D0=BB=
=D0=BE=D0=B3=D0=B8=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D1=85 =D0=BE=D1=82=D0=
=B4=D0=B5=D0=BB=D0=BE=D0=B2 =D1=80=D0=B5=D0=B4=D0=B0=D0=BA=D1=86=D0=B8=
=D0=B9 =D0=BA=D1=80=D0=BF=D0=BD=D0=B5=D0=B9=D1=88=D0=B8=D1=85 =D0=A1=D0=
=9C=D0=98. =D0=97=D0=B0=D1=8F=D0=B2=D0=BA=D0=B8 =D0=BF=D1=80=D0=B8=D0=BD=
=D0=B8=D0=BC=D0=B0=D1=8E=D1=82=D1=81=D1=8F =D0=B4=D0=BE 12 =D0=BC=D0=B0=
=D1=8F =D0=B2=D0=BA=D0=BB=D1=8E=D1=87=D0=B8=D1=82=D0=B5=D0=BB=D1=8C=D0=
=BD=D0=BE.
=D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=B5=D0=B5 (https://xn--80aaafv=
jeashuizetqkm0b6o.xn--p1ai/)

=D0=9E=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=D0=BA=D0=B2=D0=B8=D0=B7
30 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F =D0=B2 18:00 =D0=BA=D0=BE=D0=BC=
=D0=B0=D0=BD=D0=B4=D0=B0 =D0=BC=D0=BE=D0=B1=D0=B8=D0=BB=D1=8C=D0=BD=D0=
=BE=D0=B3=D0=BE =D0=BF=D1=80=D0=B8=D0=BB=D0=BE=D0=B6=D0=B5=D0=BD=D0=B8=
=D1=8F =C2=AB=D0=97=D0=B0=D1=87=D0=B5=D1=82=D0=BD=D0=B0=D1=8F =D0=9C=D0=
=BE=D1=81=D0=BA=D0=B2=D0=B0=C2=BB =D0=BF=D1=80=D0=BE=D0=B2=D0=B5=D0=B4=
=D0=B5=D1=82 =D0=B4=D0=BB=D1=8F =D1=81=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=
=82=D0=BE=D0=B2 =D0=92=D1=8B=D1=88=D0=BA=D0=B8 =D0=BE=D0=BD=D0=BB=D0=B0=
=D0=B9=D0=BD-=D0=BA=D0=B2=D0=B8=D0=B7, =D0=BF=D0=BE=D1=81=D0=B2=D1=8F=D1=
=89=D0=B5=D0=BD=D0=BD=D1=8B=D0=B9 =D0=BE=D0=B1=D1=89=D0=B8=D0=BC =D0=B7=
=D0=BD=D0=B0=D0=BD=D0=B8=D1=8F=D0=BC =D0=B2 =D0=BE=D0=B1=D0=BB=D0=B0=D1=
=81=D1=82=D0=B8 =D0=B3=D0=B5=D0=BE=D0=B3=D1=80=D0=B0=D1=84=D0=B8=D1=8F, =
=D0=B8=D1=81=D1=82=D0=BE=D1=80=D0=B8=D0=B8, =D1=81=D0=BF=D0=BE=D1=80=D1=
=82=D0=B0 =D0=B8 =D0=BC=D1=83=D0=B7=D1=8B=D0=BA=D0=B8, =D0=BA=D0=B8=D0=
=BD=D0=BE =D0=B8 =D0=BB=D0=B8=D1=82=D0=B5=D1=80=D0=B0=D1=82=D1=83=D1=80=
=D1=8B. =D0=94=D0=BB=D1=8F =D1=83=D1=87=D0=B0=D1=81=D1=82=D0=B8=D1=8F =
=D0=BD=D1=83=D0=B6=D0=BD=D0=BE =D1=81=D0=BE=D0=B1=D1=80=D0=B0=D1=82=D1=
=8C =D0=BA=D0=BE=D0=BC=D0=B0=D0=BD=D0=B4=D1=83 =D0=B8=D0=B7 5 =D1=87=D0=
=B5=D0=BB=D0=BE=D0=B2=D0=B5=D0=BA.
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F (https:/=
/docs.google.com/forms/d/17djKzpOc2bScwenSgwxrf1veCTkNzemMr2_sQhYqLR0/viewf=
orm?edit_requested=3Dtrue%C2%A0%C2%A0)

=D0=9A=D0=BE=D0=BD=D0=BA=D1=83=D1=80=D1=81 =D0=B2 =D1=87=D0=B5=D1=81=D1=
=82=D1=8C 75-=D0=BB=D0=B5=D1=82=D0=B8=D1=8F =D0=9F=D0=BE=D0=B1=D0=B5=D0=
=B4=D1=8B
=D0=9B=D0=B8=D1=82=D0=B5=D1=80=D0=B0=D1=82=D1=83=D1=80=D0=BD=D1=8B=D0=B9 =
=D0=BF=D0=BE=D1=80=D1=82=D0=B0=D0=BB Storyteller =D0=BF=D1=80=D0=B8=D0=
=B3=D0=BB=D0=B0=D1=88=D0=B0=D0=B5=D1=82 =D0=BF=D1=80=D0=B8=D0=BD=D1=8F=
=D1=82=D1=8C =D1=83=D1=87=D0=B0=D1=81=D1=82=D0=B8=D0=B5 =D0=B2 =D0=BA=D0=
=BE=D0=BD=D0=BA=D1=83=D1=80=D1=81=D0=B5 (http://storyteller.world/konkursy/=
konkurs-v-chest-75-letija-pobedy/) =D0=BA=D0=BE=D1=80=D0=BE=D1=82=D0=BA=
=D0=B8=D1=85 =D1=80=D0=B0=D1=81=D1=81=D0=BA=D0=B0=D0=B7=D0=BE=D0=B2, =D0=
=BF=D0=BE=D0=B4=D0=B5=D0=BB=D0=B8=D1=82=D1=8C=D1=81=D1=8F =D0=B8=D1=81=
=D1=82=D0=BE=D1=80=D0=B8=D1=8F=D0=BC=D0=B8 =D0=BE =D1=81=D0=B2=D0=BE=D0=
=B8=D1=85 =D1=80=D0=BE=D0=B4=D0=BD=D1=8B=D1=85 =D0=B8 =D0=B4=D1=80=D1=83=
=D0=B3=D0=B8=D1=85, =D0=B8=D0=B7=D0=B2=D0=B5=D1=81=D1=82=D0=BD=D1=8B=D1=
=85 =D0=B8 =D0=BD=D0=B5=D0=B8=D0=B7=D0=B2=D0=B5=D1=81=D1=82=D0=BD=D1=8B=
=D1=85, =D0=B3=D0=B5=D1=80=D0=BE=D1=8F=D1=85 =D0=92=D0=B5=D0=BB=D0=B8=D0=
=BA=D0=BE=D0=B9 =D0=9E=D1=82=D0=B5=D1=87=D0=B5=D1=81=D1=82=D0=B2=D0=B5=
=D0=BD=D0=BD=D0=BE=D0=B9 =D0=B2=D0=BE=D0=B9=D0=BD=D1=8B.
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F (http://=
storyteller.world/lk/)

=D0=92=D0=B8=D1=80=D1=83=D1=81=D0=BD=D1=8B=D0=B9 =D1=85=D0=B0=D0=BA=D0=
=B0=D1=82=D0=BE=D0=BD
=D0=90=D0=B3=D0=B5=D0=BD=D1=82=D1=81=D1=82=D0=B2=D0=BE =D0=B8=D0=BD=D0=
=BD=D0=BE=D0=B2=D0=B0=D1=86=D0=B8=D0=B9 =D0=9C=D0=BE=D1=81=D0=BA=D0=B2=
=D1=8B =D0=BF=D1=80=D0=B8=D0=B3=D0=BB=D0=B0=D1=88=D0=B0=D0=B5=D1=82 =D1=
=81=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=B2 =D0=BF=D1=80=D0=B8=
=D0=BD=D1=8F=D1=82=D1=8C =D1=83=D1=87=D0=B0=D1=81=D1=82=D0=B8=D0=B5 =D0=
=B2 =D0=BE=D0=BB=D0=B0=D0=B9=D0=BD-=D1=85=D0=B0=D0=BA=D0=B0=D1=82=D0=BE=
=D0=BD=D0=B5 VirusHack =D1=81 3 =D0=BF=D0=BE 5 =D0=BC=D0=B0=D1=8F. =D0=
=94=D0=BB=D1=8F =D1=81=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=82=D0=BE=D0=B2 =
=D1=81=D1=82=D0=B0=D1=80=D1=88=D0=B8=D1=85 =D0=BA=D1=83=D1=80=D1=81=D0=
=BE=D0=B2, =D0=BC=D0=B0=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D0=BD=D1=82=
=D0=BE=D0=B2 =D0=B8 =D0=B0=D1=81=D0=BF=D0=B8=D1=80=D0=B0=D0=BD=D1=82=D0=
=BE=D0=B2 =D0=B2=D1=83=D0=B7=D0=B0 =D0=BE=D0=BD=D0=BB=D0=B0=D0=B9=D0=BD-=
=D1=85=D0=B0=D0=BA=D0=B0=D1=82=D0=BE=D0=BD VirusHack =D0=B4=D0=B0=D0=B5=
=D1=82 =D0=B2=D0=BE=D0=B7=D0=BC=D0=BE=D0=B6=D0=BD=D0=BE=D1=81=D1=82=D1=
=8C =D0=BF=D1=80=D0=B8=D0=BD=D1=8F=D1=82=D1=8C =D1=83=D1=87=D0=B0=D1=81=
=D1=82=D0=B8=D0=B5 =D0=B2 =D1=81=D0=BE=D1=80=D0=B5=D0=B2=D0=BD=D0=BE=D0=
=B2=D0=B0=D0=BD=D0=B8=D0=B8, =D0=B2=D1=8B=D0=B1=D1=80=D0=B0=D0=B2 =D0=BE=
=D0=B4=D0=BD=D1=83 =D0=B8=D0=B7 =D0=BF=D1=8F=D1=82=D0=B8 =D0=B7=D0=B0=D0=
=B4=D0=B0=D1=87 =D0=BE=D1=82 =D0=BA=D0=BE=D1=80=D0=BF=D0=BE=D1=80=D0=B0=
=D1=82=D0=B8=D0=B2=D0=BD=D1=8B=D1=85 =D0=BF=D0=B0=D1=80=D1=82=D0=BD=D0=
=B5=D1=80=D0=BE=D0=B2, =D0=B0 =D1=82=D0=B0=D0=BA=D0=B6=D0=B5 =D0=BF=D1=
=80=D0=B5=D1=82=D0=B5=D0=BD=D0=B4=D0=BE=D0=B2=D0=B0=D1=82=D1=8C =D0=BD=
=D0=B0 =D0=B2=D1=8B=D0=B8=D0=B3=D1=80=D1=8B=D1=88 =D0=BF=D0=BE=D0=B4=D0=
=B0=D1=80=D0=BA=D0=BE=D0=B2 =D0=BE=D1=82 =D0=BF=D0=B0=D1=80=D1=82=D0=BD=
=D0=B5=D1=80=D0=BE=D0=B2. =D0=9F=D0=BE=D0=B4=D0=B0=D1=87=D0=B0 =D0=B7=D0=
=B0=D1=8F=D0=B2=D0=BE=D0=BA =D0=BE=D1=82=D0=BA=D1=80=D1=8B=D1=82=D0=B0 =
=D0=B4=D0=BE 30 =D0=B0=D0=BF=D1=80=D0=B5=D0=BB=D1=8F.
=D0=A0=D0=B5=D0=B3=D0=B8=D1=81=D1=82=D1=80=D0=B0=D1=86=D0=B8=D1=8F (http://=
online.innoagency.ru/virushack)

=D0=A2=D0=B0=D0=BB=D0=B8=D1=81=D0=BC=D0=B0=D0=BD =D0=92=D1=81=D0=B5=D0=
=BC=D0=B8=D1=80=D0=BD=D1=8B=D1=85 =D0=9B=D0=B5=D1=82=D0=BD=D0=B8=D1=85 =
=D0=A1=D1=82=D1=83=D0=B4=D0=B5=D0=BD=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D1=85 =
=D0=98=D0=B3=D1=80 =D0=B2 =D0=95=D0=BA=D0=B0=D1=82=D0=B5=D1=80=D0=B8=D0=
=BD=D0=B1=D1=83=D1=80=D0=B3=D0=B5
=D0=A1=D1=82=D0=B0=D1=80=D1=82=D0=BE=D0=B2=D0=B0=D0=BB =D0=BA=D0=BE=D0=
=BD=D0=BA=D1=83=D1=80=D1=81 =D0=BF=D0=BE =D1=80=D0=B0=D0=B7=D1=80=D0=B0=
=D0=B1=D0=BE=D1=82=D0=BA=D0=B5 =D1=82=D0=B0=D0=BB=D0=B8=D1=81=D0=BC=D0=
=B0=D0=BD=D0=B0 =D0=92=D1=81=D0=B5=D0=BC=D0=B8=D1=80=D0=BD=D1=8B=D1=85 =
=D0=9B=D0=B5=D1=82=D0=BD=D0=B8=D1=85 =D0=A1=D1=82=D1=83=D0=B4=D0=B5=D0=
=BD=D1=87=D0=B5=D1=81=D0=BA=D0=B8=D1=85 =D0=98=D0=B3=D1=80 =D0=B2 2023 =
=D0=B3=D0=BE=D0=B4=D1=83 =D0=B2 =D0=95=D0=BA=D0=B0=D1=82=D0=B5=D1=80=D0=
=B8=D0=BD=D0=B1=D1=83=D1=80=D0=B3=D0=B5. =D0=A1=D0=BE=D0=B7=D0=B4=D0=B0=
=D1=82=D1=8C =D1=82=D0=B0=D0=BB=D0=B8=D1=81=D0=BC=D0=B0=D0=BD =D0=BC=D0=
=BE=D0=B6=D0=B5=D1=82 =D0=BB=D1=8E=D0=B1=D0=BE=D0=B9 =D1=81=D1=82=D1=83=
=D0=B4=D0=B5=D0=BD=D1=82. =D0=9F=D1=80=D0=B8=D0=B5=D0=BC =D1=80=D0=B0=D0=
=B1=D0=BE=D1=82 =D0=BF=D1=80=D0=BE=D0=B4=D0=BB=D0=B8=D1=82=D1=81=D1=8F =
=D0=B4=D0=BE 18 =D0=BC=D0=B0=D1=8F, =D0=BF=D0=BE=D1=81=D0=BB=D0=B5 =D1=
=87=D0=B5=D0=B3=D0=BE =D0=B6=D1=8E=D1=80=D0=B8 =D0=B2=D1=8B=D0=B1=D0=B5=
=D1=80=D0=B5=D1=82 =D1=82=D1=80=D0=B8 =D0=BB=D1=83=D1=87=D1=88=D0=B8=D1=
=85 =D0=B2=D0=B0=D1=80=D0=B8=D0=B0=D0=BD=D1=82=D0=B0.
=D0=9F=D0=BE=D0=B4=D1=80=D0=BE=D0=B1=D0=BD=D0=BE=D1=81=D1=82=D0=B8

=D0=A1=D0=BB=D0=B5=D0=B4=D0=B8=D1=82=D0=B5 =D0=B7=D0=B0 =D0=B2=D1=81=D0=
=B5=D0=BC=D0=B8 =D0=B8=D0=B7=D0=BC=D0=B5=D0=BD=D0=B5=D0=BD=D0=B8=D1=8F=
=D0=BC=D0=B8 =D0=BD=D0=B0 =C2=AB=D0=92=D1=8B=D1=88=D0=BA=D0=B5 =D0=B4=D0=
=BB=D1=8F =D1=81=D0=B2=D0=BE=D0=B8=D1=85=C2=BB (https://www.hse.ru/our/) =
=D0=B8 =D0=B2 =D0=BD=D0=B0=D1=88=D0=B5=D0=BC Telegram-=D0=BA=D0=B0=D0=BD=
=D0=B0=D0=BB=D0=B5 (https://t.me/hse_live)!

=D0=9F=D0=BE=D1=8F=D0=B2=D0=B8=D0=BB=D0=B8=D1=81=D1=8C =D0=B2=D0=BE=D0=
=BF=D1=80=D0=BE=D1=81=D1=8B =D0=B8=D0=BB=D0=B8 =D0=BF=D1=80=D0=B5=D0=B4=
=D0=BB=D0=BE=D0=B6=D0=B5=D0=BD=D0=B8=D1=8F? =D0=9F=D0=B8=D1=88=D0=B8=D1=
=82=D0=B5: community@hse.ru.

=D0=A7=D1=82=D0=BE=D0=B1=D1=8B =D0=BE=D1=82=D0=BF=D0=B8=D1=81=D0=B0=D1=
=82=D1=8C=D1=81=D1=8F =D0=BE=D1=82 =D1=8D=D1=82=D0=BE=D0=B9 =D1=80=D0=B0=
=D1=81=D1=81=D1=8B=D0=BB=D0=BA=D0=B8, =D0=BF=D0=B5=D1=80=D0=B5=D0=B9=D0=
=B4=D0=B8=D1=82=D0=B5 =D0=BF=D0=BE =D1=81=D1=81=D1=8B=D0=BB=D0=BA=D0=B5 (ht=
tps://unimail.hse.ru/ru/unsubscribe?hash=3D67y96rzmwxukgz157fxz57m156w17rz1=
ua7fpnpffq7amyrfuozst63dj3mn5fgiksjozspcmjaf1b85ox1u8n1qzyr#no_tracking)
";
    }
}
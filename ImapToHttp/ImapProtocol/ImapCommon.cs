using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ImapProtocol.Contracts;

namespace ImapProtocol
{
    public class ImapCommon
    {
        public enum MessageSequenceType
        {
            None = 0,
            Exact = 1,
            Range = 2
        }
        
        public readonly static (MessageSequenceType, int[]) BadSequence = (MessageSequenceType.None, new int[0]);
        public static (MessageSequenceType, int[]) GetMessageSequenceSet(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                return BadSequence;
            }

            if (arg.Contains(':'))
            {
                if (arg.Contains('*'))
                {
                    var match = Regex.Match(arg, @"(\d+):\*");
                    int from;
                    if (!match.Success || !int.TryParse(match.Groups[1].Value, out from))
                    {
                        return BadSequence;
                    }

                    return (MessageSequenceType.Range, new[] {from, int.MaxValue});
                }
                else
                {
                    var match = Regex.Match(arg, @"(\d+):(\d+)");
                    int from, to;
                    if (!match.Success || 
                        !int.TryParse(match.Groups[1].Value, out from) ||
                        !int.TryParse(match.Groups[2].Value, out to))
                    {
                        return BadSequence;
                    }
                    return (MessageSequenceType.Range, new[] {from, to});
                }
            }

            var values = arg.Split(',').Select(s => s.Trim());
            var parsedValues = values
                .Select(v => (int.TryParse(v, out var result), result))
                .ToList();
            if (parsedValues.Any(p => !p.Item1))
            {
                return BadSequence;
            }

            return (MessageSequenceType.Exact, parsedValues.Select(p => p.result).ToArray());
        }
        
        
        public static IEnumerable<int> ExtractRealMessageIds(IImapContext ctx, (ImapCommon.MessageSequenceType, int[]) range, MessageIdType idType)
        {
            switch (range.Item1)
            {
                case ImapCommon.MessageSequenceType.None:
                    return new int[0];
                case ImapCommon.MessageSequenceType.Exact:
                    if (idType == MessageIdType.Uid)
                    {
                        return range.Item2
                            .Where(i => ctx.EntityProvider.DirectoryProvider.HasMessage(i));
                    }
                    return ctx.EntityProvider.DirectoryProvider.GetUids(range.Item2);
                case ImapCommon.MessageSequenceType.Range:
                    int min = range.Item2[0];
                    int max = range.Item2[1];
                    var idsRange = Enumerable.Range(range.Item2[0], max - min + 1).ToArray();
                    
                    if (idType == MessageIdType.Uid)
                    {
                        return idsRange
                            .Where(i => ctx.EntityProvider.DirectoryProvider.HasMessage(i));
                    }
                    return ctx.EntityProvider.DirectoryProvider.GetUids(idsRange);
                default:
                    throw new ArgumentException(nameof(range));
            }
        }
        
        
        public static Dictionary<int, string> Messagxes = new Dictionary<int, string>() { {29,
            @"Received: from mxfront3q.mail.yandex.net (localhost [127.0.0.1])
	by mxfront3q.mail.yandex.net with LMTP id XHgJiklRSE-oiZJ5R89
	for <nektar97@yandex.ru>; Tue, 12 May 2020 00:51:02 +0300
Received: from mail-ua1-x94a.google.com (mail-ua1-x94a.google.com [2607:f8b0:4864:20::94a])
	by mxfront3q.mail.yandex.net (mxfront/Yandex) with ESMTPS id EGNfVZSP6P-p1uimQ6O;
	Tue, 12 May 2020 00:51:01 +0300
	(using TLSv1.3 with cipher TLS_AES_256_GCM_SHA384 (256/256 bits))
	(Client certificate not present)
Return-Path: 3xMi5XggTCDkij-mZkgtVXXjpion.bjjbgZ.XjhiZfoVm42tViYZs.mp@gaia.bounces.google.com
X-Yandex-Front: mxfront3q.mail.yandex.net
X-Yandex-TimeMark: 1589233861.788
Authentication-Results: mxfront3q.mail.yandex.net; spf=pass (mxfront3q.mail.yandex.net: domain of gaia.bounces.google.com designates 2607:f8b0:4864:20::94a as permitted sender, rule=[ip6:2607:f8b0:4000::/36]) smtp.mail=3xMi5XggTCDkij-mZkgtVXXjpion.bjjbgZ.XjhiZfoVm42tViYZs.mp@gaia.bounces.google.com; dkim=pass header.i=@accounts.google.com
X-Yandex-Suid-Status: 1 193281561
X-Yandex-Spam: 1
X-Yandex-Fwd: MTA1ODE4NTY2MzI0MjY4MjQ2NiwxMDIxOTA4NzA1NjUwNTA1ODc4NQ==
Received: by mail-ua1-x94a.google.com with SMTP id j2so5019010uak.3
        for <nektar97@yandex.ru>; Mon, 11 May 2020 14:51:01 -0700 (PDT)
DKIM-Signature: v=1; a=rsa-sha256; c=relaxed/relaxed;
        d=accounts.google.com; s=20161025;
        h=mime-version:date:feedback-id:message-id:subject:from:to;
        bh=LAPfTwj/m6QnMFxK+ShetUMWB5u5JMXABnGtBBn11ko=;
        b=LS3cPTc79Gu8oGmeCpVTL58g+aH6fC52lMjhhbkAZZb6JZZ0GK49ef8sD3lHHP9l3c
         vB57DunF5XC5uWpA5rnxq0uc8rtf9z68I9bgYsOPPAOyhqCA1vz4oJDZiBF2Wmq9pOgT
         wzqkGJtQvmjQBhfXGv0FzH+TKNWdPvc+J+1MGfZIHeU+1oXMnifAC3ux7HC9yjxrs5Si
         2h7uL8AZsbP0tlfbnIe9pulKbz9gE2KtZKaONuZbsep8z/n+Zijo7+JZ4+BIk5hxEwa3
         fXpVKYZOqi1e0LN9B6+kM/PxDIHf8j++oSiwUZOtwSp521vDUx/uBoYMe2kRyBMYSeTl
         GT9A==
X-Google-DKIM-Signature: v=1; a=rsa-sha256; c=relaxed/relaxed;
        d=1e100.net; s=20161025;
        h=x-gm-message-state:mime-version:date:feedback-id:message-id:subject
         :from:to;
        bh=LAPfTwj/m6QnMFxK+ShetUMWB5u5JMXABnGtBBn11ko=;
        b=LQCg89HJx16pPNrYLVOCFGk57GdKkJpqB9/JWwR4wmdUKru0x3LaZKbleqTyAVnwQL
         xpOkgoB+h4ig/mow479IEEhC8WTV2ov77tmJDHKZP1E+R/KDPeeNel+50sYujAeK9T2H
         e2nfNvq8sSQgoP5i9TY2mcwiRGkfrAF3SsURgbA32HRAvr4KnSK203a5iCh15RvsX+93
         v5JBu7JlHOB9aS1J6icI+FKYVz7u9Efyll58HCkfb4ZZjJsG/ZaSp9WjkmFKYFJ8gD0z
         aN2HqpZc0p+u6iTYMi0kzHReCSplXYr74WJyXzUhcTBn7G+GiG8S8UFqwkHSsOkaoquW
         ckqw==
X-Gm-Message-State: AOAM533grkdreBbsy8h5eNQdaWZq6iGtUomX30BG9C+UrYX13ZSAK9wg
	x0bAqVA/L56P4gtLC7avlCHgmbhmsXcr
X-Google-Smtp-Source: ABdhPJwMpLAO/+gk1aRsYoMeSiaSsq6TdWJBJHZRI/nNa/Xu1tvhl7WL5i8diXRx3O4ZKvW1hF63P16bj2Loq2YT0sRRug==
MIME-Version: 1.0
X-Received: by 2002:a1f:4d03:: with SMTP id a3mr4214163vkb.51.1589233860046;
 Mon, 11 May 2020 14:51:00 -0700 (PDT)
Date: Mon, 11 May 2020 21:50:59 +0000 (GMT)
X-Account-Notification-Type: 31-RECOVERY-anexp#hsc-control_b
Feedback-ID: 31-RECOVERY-anexp#hsc-control_b:account-notifier
X-Notifications: 945326ed94400000
Message-ID: <-qPLx0x0xxxjdxxssdslrISrf5xxvx4g.0@nxot1ifications.google.com>
Subject: =?UTF-8?B?0J7Qv9C+0LLQtdGJ0LXQvdC40LUg0L4g0LHQtdC30L7Qv9Cw0YHQvdC+0YHRgtC4INGB?=
	=?UTF-8?B?0LLRj9C30LDQvdC90L7Qs9C+INCw0LrQutCw0YPQvdGC0LAgR29vZ2xl?=
From: Google <no-reply@accounts.google.com>
To: nektar97@yandex.ru
Content-Transfer-Encoding: 8bit
Content-Type: text/html; charset=utf-8

<!DOCTYPE html><html lang=""en""><head><meta name=""format-detection"" content=""email=no""/><meta name=""format-detection"" content=""date=no""/><style nonce=""XUU54E2GbusQNZpRig13tA"">.awl a {color: #FFFFFF; text-decoration: none;} .abml a {color: #000000; font-family: Roboto-Medium,Helvetica,Arial,sans-serif; font-weight: bold; text-decoration: none;} .adgl a {color: rgba(0, 0, 0, 0.87); text-decoration: none;} .afal a {color: #b0b0b0; text-decoration: none;} @media screen and (min-width: 600px) {.v2sp {padding: 6px 30px 0px;} .v2rsp {padding: 0px 10px;}} @media screen and (min-width: 600px) {.mdv2rw {padding: 40px 40px;}} </style><link href=""//fonts.googleapis.com/css?family=Google+Sans"" rel=""stylesheet"" type=""text/css""/></head><body style=""margin: 0; padding: 0;"" bgcolor=""#FFFFFF""><table width=""100%"" height=""100%"" style=""min-width: 348px;"" border=""0"" cellspacing=""0"" cellpadding=""0"" lang=""en""><tr height=""32"" style=""height: 32px;""><td></td></tr><tr align=""center""><td><div itemscope itemtype=""//schema.org/EmailMessage""><div itemprop=""action"" itemscope itemtype=""//schema.org/ViewAction""><link itemprop=""url"" href=""https://accounts.google.com/AccountChooser?Email=nikita.tarasov1997@gmail.com&amp;continue=https://myaccount.google.com/alert/nt/1589233859000?rfn%3D31%26rfnc%3D1%26eid%3D-1949072752098066202%26et%3D1%26anexp%3Dhsc-control_b""/><meta itemprop=""name"" content=""Просмотреть действия""/></div></div><table border=""0"" cellspacing=""0"" cellpadding=""0"" style=""padding-bottom: 20px; max-width: 516px; min-width: 220px;""><tr><td width=""8"" style=""width: 8px;""></td><td><div style=""background-color: #F5F5F5; direction: ltr; padding: 16px;margin-bottom: 6px;""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""><tbody><tr><td style=""vertical-align: top;""><img height=""20"" src=""https://www.gstatic.com/accountalerts/email/Icon_recovery_x2_20_20.png""></td><td width=""13"" style=""width: 13px;""></td><td style=""direction: ltr;""><span style=""font-family: Roboto-Regular,Helvetica,Arial,sans-serif; font-size: 13px; color: rgba(0,0,0,0.87); line-height: 1.6; color: rgba(0,0,0,0.54);"">Адрес <a style=""text-decoration: none; color: rgba(0,0,0,0.87);"">nektar97@yandex.ru</a> &#x443;&#x43A;&#x430;&#x437;&#x430;&#x43D; &#x432; &#x43A;&#x430;&#x447;&#x435;&#x441;&#x442;&#x432;&#x435; &#x440;&#x435;&#x437;&#x435;&#x440;&#x432;&#x43D;&#x43E;&#x433;&#x43E; &#x434;&#x43B;&#x44F; &#x432;&#x43E;&#x441;&#x441;&#x442;&#x430;&#x43D;&#x43E;&#x432;&#x43B;&#x435;&#x43D;&#x438;&#x44F; &#x434;&#x43E;&#x441;&#x442;&#x443;&#x43F;&#x430; &#x43A; &#x430;&#x43A;&#x43A;&#x430;&#x443;&#x43D;&#x442;&#x443; <a style=""text-decoration: none; color: rgba(0,0,0,0.87);"">nikita.tarasov1997@gmail.com</a>.</span> <span><span style=""font-family: Roboto-Regular,Helvetica,Arial,sans-serif; font-size: 13px; color: rgba(0,0,0,0.87); line-height: 1.6; color: rgba(0,0,0,0.54);"">Это не Ваш аккаунт? Нажмите <a href=""https://accounts.google.com/AccountDisavow?adt=AOX8kip424YnwreejXc5XLfo4IyprZe0sqGhcXmwR2FBU21o5K_B8oPJdpAbJLc&amp;rfn=31&amp;anexp=hsc-control_b"" data-meta-key=""disavow"" style=""text-decoration: none; color: #4285F4;"" target=""_blank"">здесь</a>.</span></span></td></tr></tbody></table></div><div style=""border-style: solid; border-width: thin; border-color:#dadce0; border-radius: 8px; padding: 40px 20px;"" align=""center"" class=""mdv2rw""><img src=""https://www.gstatic.com/images/branding/googlelogo/2x/googlelogo_color_74x24dp.png"" width=""74"" height=""24"" aria-hidden=""true"" style=""margin-bottom: 16px;"" alt=""Google""><div style=""font-family: &#39;Google Sans&#39;,Roboto,RobotoDraft,Helvetica,Arial,sans-serif;border-bottom: thin solid #dadce0; color: rgba(0,0,0,0.87); line-height: 32px; padding-bottom: 24px;text-align: center; word-break: break-word;""><div style=""font-size: 24px;"">&#x412;&#x44B;&#x43F;&#x43E;&#x43B;&#x43D;&#x435;&#x43D; &#x432;&#x445;&#x43E;&#x434; &#x432; &#x412;&#x430;&#x448; &#x441;&#x432;&#x44F;&#x437;&#x430;&#x43D;&#x43D;&#x44B;&#x439; &#x430;&#x43A;&#x43A;&#x430;&#x443;&#x43D;&#x442;</div><table align=""center"" style=""margin-top:8px;""><tr style=""line-height: normal;""><td align=""right"" style=""padding-right:8px;""><img width=""20"" height=""20"" style=""width: 20px; height: 20px; vertical-align: sub; border-radius: 50%;;"" src=""https://lh3.googleusercontent.com/a-/AOh14GjCucR-rYFWBkt5LpWP2nLC-9CrrQ4WXB3iqGtP7w=s96"" alt=""""></td><td><a style=""font-family: &#39;Google Sans&#39;,Roboto,RobotoDraft,Helvetica,Arial,sans-serif;color: rgba(0,0,0,0.87); font-size: 14px; line-height: 20px;"">nikita.tarasov1997@gmail.com</a></td></tr></table></div><div style=""font-family: Roboto-Regular,Helvetica,Arial,sans-serif; font-size: 14px; color: rgba(0,0,0,0.87); line-height: 20px;padding-top: 20px; text-align: center;"">В Ваш аккаунт Google только что выполнен вход на новом устройстве Apple iPhone. Мы хотим убедиться, что это были Вы.<div style=""padding-top: 32px; text-align: center;""><a href=""https://accounts.google.com/AccountChooser?Email=nikita.tarasov1997@gmail.com&amp;continue=https://myaccount.google.com/alert/nt/1589233859000?rfn%3D31%26rfnc%3D1%26eid%3D-1949072752098066202%26et%3D1%26anexp%3Dhsc-control_b"" target=""_blank"" link-id=""main-button-link"" style=""font-family: &#39;Google Sans&#39;,Roboto,RobotoDraft,Helvetica,Arial,sans-serif; line-height: 16px; color: #ffffff; font-weight: 400; text-decoration: none;font-size: 14px;display:inline-block;padding: 10px 24px;background-color: #4184F3; border-radius: 5px; min-width: 90px;"">Посмотреть действия</a></div></div></div><div style=""text-align: left;""><div style=""font-family: Roboto-Regular,Helvetica,Arial,sans-serif;color: rgba(0,0,0,0.54); font-size: 11px; line-height: 18px; padding-top: 12px; text-align: center;""><div>Это сообщение о важных изменениях в Вашем аккаунте и сервисах Google.</div><div style=""direction: ltr;"">&copy; 2020 Google LLC, <a class=""afal"" style=""font-family: Roboto-Regular,Helvetica,Arial,sans-serif;color: rgba(0,0,0,0.54); font-size: 11px; line-height: 18px; padding-top: 12px; text-align: center;"">1600 Amphitheatre Parkway, Mountain View, CA 94043, USA</a></div></div></div></td><td width=""8"" style=""width: 8px;""></td></tr></table></td></tr><tr height=""32"" style=""height: 32px;""><td></td></tr></table></body></html>" },{16, 
            @"Received: from mxback23g.mail.yandex.net (localhost [127.0.0.1])
	by mxback23g.mail.yandex.net with LMTP id Flrtgaf4AP-i3WimzCS
	for <nektar97+nt@yandex.ru>; Sun, 26 Apr 2020 20:07:37 +0300
Received: from forward105o.mail.yandex.net (forward105o.mail.yandex.net [2a02:6b8:0:1a2d::608])
	by mxback23g.mail.yandex.net (mxback/Yandex) with ESMTP id l41jEovxrP-7aCCUAa5;
	Sun, 26 Apr 2020 20:07:36 +0300
X-Yandex-Front: mxback23g.mail.yandex.net
X-Yandex-TimeMark: 1587920856.654
X-Yandex-Suid-Status: 1 193281561
X-Yandex-Spam: 1
Received: from mxback24o.mail.yandex.net (mxback24o.mail.yandex.net [IPv6:2a02:6b8:0:1a2d::75])
	by forward105o.mail.yandex.net (Yandex) with ESMTP id 96EB642007F5
	for <nektar97+nt@yandex.ru>; Sun, 26 Apr 2020 20:07:36 +0300 (MSK)
Received: from mxback24o.mail.yandex.net (localhost [127.0.0.1])
	by mxback24o.mail.yandex.net with LMTP id yNdu4YYrip-lEUovJQb;
	Sun, 26 Apr 2020 20:07:36 +0300
Received: from mxback24o.mail.yandex.net (localhost [127.0.0.1])
	by mxback24o.mail.yandex.net (Yandex) with ESMTP id 63FB45421A29;
	Sun, 26 Apr 2020 20:07:36 +0300 (MSK)
Received: from localhost (localhost [::1])
	by mxback24o.mail.yandex.net (mxback/Yandex) with ESMTP id CJIF4Wo7r0-7aPixIx9;
	Sun, 26 Apr 2020 20:07:36 +0300
DKIM-Signature: v=1; a=rsa-sha256; c=relaxed/relaxed; d=yandex.ru; s=mail; t=1587920856;
	bh=0ssghA7nBK+MzvCyBqnnyxm6w7KXH0nd85KoqvbrUOI=;
	h=Message-Id:Date:Subject:To:From;
	b=TvmDnglhbHdGGjxQmqHgWs5NOUUYXjdVmN4IzHdBubQoWStf2OXdpq6yM0P2gGdkV
	 ZPjrhaCfoIzxC/XLrAVDKbKur5JGTnbScDNgXYT88E8ReuqOSBCm5oJwhfJ+wYdn0W
	 a29K8BWfqeYwWBPPi/c/4RjVwZRXPDYn+3iNFLRs=
Authentication-Results: mxback24o.mail.yandex.net; dkim=pass header.i=@yandex.ru
X-Yandex-Suid-Status: 1 1130000035882273,1 0
X-Yandex-Sender-Uid: 70876967
Received: by sas1-55829ddbd171.qloud-c.yandex.net with HTTP;
	Sun, 26 Apr 2020 20:07:35 +0300
From: Nikita Tarasov <mail@ntarasov.ru>
Envelope-From: nektar97@yandex.ru
To: mail <mail@ntarasov.ru>
Subject: The test letter!
MIME-Version: 1.0
X-Mailer: Yamail [ http://yandex.ru ] 5.0
Date: Sun, 26 Apr 2020 20:07:35 +0300
Message-Id: <1255x51xxxx587920843@mail.yandex.ru>
Content-Transfer-Encoding: 8bit
Content-Type: text/html; charset=utf-8
X-Yandex-Forward: 9d668b532af1baa9ecc9f739379b8bef
Return-Path: mail@ntarasov.ru
X-Yandex-Forward: 85d02b20992161de89aefe959a6f9d4d

<div> </div><div> </div><div>-- </div><div>1<b>2</b>4456<img src=""https://sun9-40.userapi.com/c857636/v857636350/1f2d7c/QtotoIZnME8.jpg""/></div><div> </div>" }};

    }
}
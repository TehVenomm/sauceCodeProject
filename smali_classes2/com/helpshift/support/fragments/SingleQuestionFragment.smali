.class public Lcom/helpshift/support/fragments/SingleQuestionFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "SingleQuestionFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;
.implements Lcom/helpshift/support/webkit/CustomWebViewClient$CustomWebViewClientListeners;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/fragments/SingleQuestionFragment$Failure;,
        Lcom/helpshift/support/fragments/SingleQuestionFragment$Success;,
        Lcom/helpshift/support/fragments/SingleQuestionFragment$SingleQuestionModes;,
        Lcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;
    }
.end annotation


# static fields
.field public static final BUNDLE_ARG_QUESTION_LANGUAGE:Ljava/lang/String; = "questionLanguage"

.field public static final BUNDLE_ARG_QUESTION_PUBLISH_ID:Ljava/lang/String; = "questionPublishId"

.field private static final TAG:Ljava/lang/String; = "Helpshift_SingleQstn"


# instance fields
.field private contactUsButton:Landroid/widget/Button;

.field private data:Lcom/helpshift/support/HSApiData;

.field private decomp:Z

.field eventSent:Z

.field private highlightedQuestion:Lcom/helpshift/support/Faq;

.field private isHelpful:I

.field private isHighlighted:Z

.field private noButton:Landroid/widget/Button;

.field private progressBar:Landroid/view/View;

.field private question:Lcom/helpshift/support/Faq;

.field private questionFooter:Landroid/view/View;

.field private questionFooterMessage:Landroid/widget/TextView;

.field private questionPublishId:Ljava/lang/String;

.field private questionReadListener:Lcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;

.field private showRootLayoutInsideCardView:Z

.field private singleQuestionMode:I

.field private supportController:Lcom/helpshift/support/controllers/SupportController;

.field private textColor:Ljava/lang/String;

.field private textColorLink:Ljava/lang/String;

.field private webView:Lcom/helpshift/support/webkit/CustomWebView;

.field private yesButton:Landroid/widget/Button;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 47
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    const/4 v0, 0x1

    .line 55
    iput v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->singleQuestionMode:I

    const/4 v0, 0x0

    .line 70
    iput v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isHelpful:I

    .line 71
    iput-boolean v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->showRootLayoutInsideCardView:Z

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/support/fragments/SingleQuestionFragment;)Lcom/helpshift/support/Faq;
    .locals 0

    .line 47
    iget-object p0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->highlightedQuestion:Lcom/helpshift/support/Faq;

    return-object p0
.end method

.method static synthetic access$002(Lcom/helpshift/support/fragments/SingleQuestionFragment;Lcom/helpshift/support/Faq;)Lcom/helpshift/support/Faq;
    .locals 0

    .line 47
    iput-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->highlightedQuestion:Lcom/helpshift/support/Faq;

    return-object p1
.end method

.method static synthetic access$100(Lcom/helpshift/support/fragments/SingleQuestionFragment;)Lcom/helpshift/support/Faq;
    .locals 0

    .line 47
    iget-object p0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    return-object p0
.end method

.method private getColorsFromTheme(Landroid/content/Context;)V
    .locals 2

    .line 129
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_0

    const v0, 0x1010435

    goto :goto_0

    :cond_0
    const v0, 0x101009b

    :goto_0
    const v1, 0x1010036

    .line 132
    invoke-static {p1, v1}, Lcom/helpshift/util/Styles;->getHexColor(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object v1

    iput-object v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->textColor:Ljava/lang/String;

    .line 133
    invoke-static {p1, v0}, Lcom/helpshift/util/Styles;->getHexColor(Landroid/content/Context;I)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->textColorLink:Ljava/lang/String;

    return-void
.end method

.method private getStyledBody(Lcom/helpshift/support/Faq;)Ljava/lang/String;
    .locals 11

    const-string v0, "24px"

    const-string v1, "32px"

    const-string v2, "16px"

    const-string v3, "1.5"

    .line 264
    invoke-static {}, Lcom/helpshift/views/FontApplier;->getFontPath()Ljava/lang/String;

    move-result-object v4

    const-string v5, ""

    const-string v6, ""

    .line 267
    invoke-static {v4}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v7

    if-nez v7, :cond_0

    .line 268
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "file:///android_asset/"

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    .line 269
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "@font-face {    font-family: custom;    src: url(\'"

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v4, "\');}"

    invoke-virtual {v5, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    const-string v6, "font-family: custom, sans-serif;"

    .line 277
    :cond_0
    iget-object v4, p1, Lcom/helpshift/support/Faq;->body:Ljava/lang/String;

    .line 278
    iget-object v7, p1, Lcom/helpshift/support/Faq;->title:Ljava/lang/String;

    .line 280
    iget-object p1, p1, Lcom/helpshift/support/Faq;->is_rtl:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    if-eqz p1, :cond_1

    .line 281
    new-instance p1, Ljava/lang/StringBuilder;

    const-string v8, "<html dir=\"rtl\">"

    invoke-direct {p1, v8}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    goto :goto_0

    .line 284
    :cond_1
    new-instance p1, Ljava/lang/StringBuilder;

    const-string v8, "<html>"

    invoke-direct {p1, v8}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    .line 286
    :goto_0
    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    const/16 v9, 0x10

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v10, "px "

    invoke-virtual {v8, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v10, "px "

    invoke-virtual {v8, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v10, 0x60

    invoke-virtual {v8, v10}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v10, "px "

    invoke-virtual {v8, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v10, "px;"

    invoke-virtual {v8, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v8

    const-string v10, "<head>"

    .line 287
    invoke-virtual {p1, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v10, "    <style type=\'text/css\'>"

    .line 288
    invoke-virtual {p1, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 289
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "        img,"

    .line 290
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "        object,"

    .line 291
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "        embed {"

    .line 292
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "            max-width: 100%;"

    .line 293
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "        }"

    .line 294
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "        a,"

    .line 295
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "        a:visited,"

    .line 296
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "        a:active,"

    .line 297
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "        a:hover {"

    .line 298
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "            color: "

    .line 299
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v5, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->textColorLink:Ljava/lang/String;

    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ";"

    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "        }"

    .line 300
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "        body {"

    .line 301
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "            background-color: transparent;"

    .line 302
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "            margin: 0;"

    .line 303
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "            padding: "

    .line 304
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, "            font-size: "

    .line 305
    invoke-virtual {p1, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ";"

    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 306
    invoke-virtual {p1, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "            line-height: "

    .line 307
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ";"

    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "            white-space: normal;"

    .line 308
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "            word-wrap: break-word;"

    .line 309
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "            color: "

    .line 310
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->textColor:Ljava/lang/String;

    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ";"

    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "        }"

    .line 311
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "        .title {"

    .line 312
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "            display: block;"

    .line 313
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "            margin: 0;"

    .line 314
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "            padding: 0 0 "

    .line 315
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, " 0;"

    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "            font-size: "

    .line 316
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, ";"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 317
    invoke-virtual {p1, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            line-height: "

    .line 318
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, ";"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        }"

    .line 319
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        h1, h2, h3 { "

    .line 320
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            line-height: 1.4; "

    .line 321
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        }"

    .line 322
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "    </style>"

    .line 323
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "    <script language=\'javascript\'>"

    .line 324
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "     window.onload = function () {"

    .line 325
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        var w = window,"

    .line 326
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            d = document,"

    .line 327
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            e = d.documentElement,"

    .line 328
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            g = d.getElementsByTagName(\'body\')[0],"

    .line 329
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            sWidth = Math.min (w.innerWidth || Infinity, e.clientWidth || Infinity, g.clientWidth || Infinity),"

    .line 332
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            sHeight = Math.min (w.innerHeight || Infinity, e.clientHeight || Infinity, g.clientHeight || Infinity);"

    .line 334
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        var frame, fw, fh;"

    .line 336
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        var iframes = document.getElementsByTagName(\'iframe\');"

    .line 337
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        var padding = "

    .line 338
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v0, 0x20

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v0, ";"

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        for (var i=0; i < iframes.length; i++) {"

    .line 339
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            frame = iframes[i];"

    .line 340
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            fw = frame.offsetWidth;"

    .line 341
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            fh = frame.offsetHeight;"

    .line 342
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            if (fw >= fh && fw > (sWidth - padding)) {"

    .line 343
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "                frame.style.width = sWidth - padding;"

    .line 344
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "                frame.style.height = ((sWidth - padding) * fh/fw).toString();"

    .line 345
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            }"

    .line 346
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        }"

    .line 347
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        document.addEventListener(\'click\', function (event) {"

    .line 348
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            if (event.target instanceof HTMLImageElement) {"

    .line 349
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "                event.preventDefault();"

    .line 350
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "                event.stopPropagation();"

    .line 351
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "            }"

    .line 352
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "        }, false);"

    .line 353
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "    };"

    .line 354
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "    </script>"

    .line 355
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "</head>"

    .line 356
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "<body>"

    .line 357
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "    <strong class=\'title\'> "

    .line 358
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, " </strong> "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "</body>"

    .line 359
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v0, "</html>"

    .line 360
    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 361
    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private hideQuestionFooter()V
    .locals 2

    .line 499
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooter:Landroid/view/View;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    return-void
.end method

.method private highlightAndReloadQuestion()V
    .locals 3

    const/4 v0, 0x1

    .line 449
    iput-boolean v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isHighlighted:Z

    .line 450
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "searchTerms"

    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getStringArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object v0

    .line 451
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->getDomain()Lcom/helpshift/common/domain/Domain;

    move-result-object v1

    new-instance v2, Lcom/helpshift/support/fragments/SingleQuestionFragment$1;

    invoke-direct {v2, p0, v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment$1;-><init>(Lcom/helpshift/support/fragments/SingleQuestionFragment;Ljava/util/ArrayList;)V

    invoke-virtual {v1, v2}, Lcom/helpshift/common/domain/Domain;->runParallel(Lcom/helpshift/common/domain/F;)V

    return-void
.end method

.method private markQuestion(Z)V
    .locals 2

    .line 365
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    if-nez v0, :cond_0

    return-void

    .line 368
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    invoke-virtual {v0}, Lcom/helpshift/support/Faq;->getId()Ljava/lang/String;

    move-result-object v0

    .line 370
    iget-object v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->data:Lcom/helpshift/support/HSApiData;

    invoke-virtual {v1, v0, p1}, Lcom/helpshift/support/HSApiData;->markFaqInDB(Ljava/lang/String;Z)V

    .line 373
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->getFaqDM()Lcom/helpshift/faq/FaqsDM;

    move-result-object v1

    invoke-virtual {v1, v0, p1}, Lcom/helpshift/faq/FaqsDM;->markHelpful(Ljava/lang/String;Z)V

    return-void
.end method

.method public static newInstance(Landroid/os/Bundle;IZLcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;)Lcom/helpshift/support/fragments/SingleQuestionFragment;
    .locals 1

    .line 80
    new-instance v0, Lcom/helpshift/support/fragments/SingleQuestionFragment;

    invoke-direct {v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;-><init>()V

    .line 81
    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->setArguments(Landroid/os/Bundle;)V

    .line 82
    iput p1, v0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->singleQuestionMode:I

    .line 83
    iput-boolean p2, v0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->showRootLayoutInsideCardView:Z

    .line 84
    iput-object p3, v0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionReadListener:Lcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;

    return-object v0
.end method

.method private setIsHelpful(I)V
    .locals 0

    if-eqz p1, :cond_0

    .line 422
    iput p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isHelpful:I

    .line 424
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->updateFooter()V

    return-void
.end method

.method private showHelpfulFooter()V
    .locals 3

    .line 511
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooter:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 512
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooterMessage:Landroid/widget/TextView;

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    sget v2, Lcom/helpshift/R$string;->hs__question_helpful_message:I

    invoke-virtual {v1, v2}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 513
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooterMessage:Landroid/widget/TextView;

    const/16 v1, 0x11

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setGravity(I)V

    .line 514
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->contactUsButton:Landroid/widget/Button;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    .line 515
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->yesButton:Landroid/widget/Button;

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    .line 516
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->noButton:Landroid/widget/Button;

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    return-void
.end method

.method private showProgress(Z)V
    .locals 1

    .line 469
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->progressBar:Landroid/view/View;

    if-eqz v0, :cond_1

    if-eqz p1, :cond_0

    .line 471
    iget-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->progressBar:Landroid/view/View;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    .line 474
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->progressBar:Landroid/view/View;

    const/16 v0, 0x8

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    :cond_1
    :goto_0
    return-void
.end method

.method private showQuestionFooter()V
    .locals 4

    .line 503
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooter:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 504
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooterMessage:Landroid/widget/TextView;

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/helpshift/R$string;->hs__mark_yes_no_question:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 505
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->contactUsButton:Landroid/widget/Button;

    const/16 v2, 0x8

    invoke-virtual {v0, v2}, Landroid/widget/Button;->setVisibility(I)V

    .line 506
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->yesButton:Landroid/widget/Button;

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    .line 507
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->noButton:Landroid/widget/Button;

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    return-void
.end method

.method private showQuestionFooterContactUs()V
    .locals 2

    .line 528
    sget-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->QUESTION_FOOTER:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    invoke-static {v0}, Lcom/helpshift/support/ContactUsFilter;->showContactUs(Lcom/helpshift/support/ContactUsFilter$LOCATION;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 529
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->contactUsButton:Landroid/widget/Button;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    goto :goto_0

    .line 532
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->contactUsButton:Landroid/widget/Button;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    :goto_0
    return-void
.end method

.method private showUnhelpfulFooter()V
    .locals 3

    .line 520
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooter:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 521
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooterMessage:Landroid/widget/TextView;

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    sget v2, Lcom/helpshift/R$string;->hs__question_unhelpful_message:I

    invoke-virtual {v1, v2}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 522
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->showQuestionFooterContactUs()V

    .line 523
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->yesButton:Landroid/widget/Button;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    .line 524
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->noButton:Landroid/widget/Button;

    invoke-virtual {v0, v1}, Landroid/widget/Button;->setVisibility(I)V

    return-void
.end method

.method private updateFooter()V
    .locals 2

    .line 480
    iget v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->singleQuestionMode:I

    const/4 v1, 0x3

    if-ne v0, v1, :cond_0

    .line 481
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->hideQuestionFooter()V

    return-void

    .line 485
    :cond_0
    iget v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isHelpful:I

    packed-switch v0, :pswitch_data_0

    goto :goto_0

    .line 490
    :pswitch_0
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->showHelpfulFooter()V

    goto :goto_0

    .line 487
    :pswitch_1
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->showQuestionFooter()V

    goto :goto_0

    .line 493
    :pswitch_2
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->showUnhelpfulFooter()V

    :goto_0
    return-void

    nop

    :pswitch_data_0
    .packed-switch -0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;
    .locals 1

    .line 413
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/contracts/FaqFlowViewParent;

    if-eqz v0, :cond_0

    .line 415
    invoke-interface {v0}, Lcom/helpshift/support/contracts/FaqFlowViewParent;->getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;

    move-result-object v0

    return-object v0

    :cond_0
    const/4 v0, 0x0

    return-object v0
.end method

.method public getQuestionId()Ljava/lang/String;
    .locals 2

    const-string v0, ""

    .line 538
    iget-object v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    if-eqz v1, :cond_0

    .line 539
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    invoke-virtual {v0}, Lcom/helpshift/support/Faq;->getId()Ljava/lang/String;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method public getQuestionPublishId()Ljava/lang/String;
    .locals 1

    .line 545
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionPublishId:Ljava/lang/String;

    return-object v0
.end method

.method public onAttach(Landroid/content/Context;)V
    .locals 1

    .line 90
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onAttach(Landroid/content/Context;)V

    .line 91
    new-instance v0, Lcom/helpshift/support/HSApiData;

    invoke-direct {v0, p1}, Lcom/helpshift/support/HSApiData;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->data:Lcom/helpshift/support/HSApiData;

    .line 92
    invoke-static {p0}, Lcom/helpshift/support/util/FragmentUtil;->getSupportFragment(Landroidx/fragment/app/Fragment;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 94
    invoke-virtual {p1}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    .line 96
    :cond_0
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->singleQuestionMode:I

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->fragmentName:Ljava/lang/String;

    return-void
.end method

.method public onClick(Landroid/view/View;)V
    .locals 3

    .line 378
    invoke-virtual {p1}, Landroid/view/View;->getId()I

    move-result v0

    sget v1, Lcom/helpshift/R$id;->helpful_button:I

    const/4 v2, 0x1

    if-ne v0, v1, :cond_0

    .line 379
    invoke-direct {p0, v2}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->markQuestion(Z)V

    .line 380
    invoke-direct {p0, v2}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->setIsHelpful(I)V

    .line 381
    iget p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->singleQuestionMode:I

    const/4 v0, 0x2

    if-ne p1, v0, :cond_3

    .line 382
    invoke-static {p0}, Lcom/helpshift/support/util/FragmentUtil;->getSupportFragment(Landroidx/fragment/app/Fragment;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p1

    if-eqz p1, :cond_3

    .line 384
    invoke-virtual {p1}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object p1

    .line 385
    invoke-virtual {p1}, Lcom/helpshift/support/controllers/SupportController;->actionDone()V

    goto :goto_0

    .line 389
    :cond_0
    invoke-virtual {p1}, Landroid/view/View;->getId()I

    move-result v0

    sget v1, Lcom/helpshift/R$id;->unhelpful_button:I

    if-ne v0, v1, :cond_1

    const/4 p1, 0x0

    .line 390
    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->markQuestion(Z)V

    const/4 p1, -0x1

    .line 391
    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->setIsHelpful(I)V

    goto :goto_0

    .line 393
    :cond_1
    invoke-virtual {p1}, Landroid/view/View;->getId()I

    move-result p1

    sget v0, Lcom/helpshift/R$id;->contact_us_button:I

    if-ne p1, v0, :cond_3

    .line 394
    iget-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    if-eqz p1, :cond_3

    .line 395
    iget p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->singleQuestionMode:I

    if-ne p1, v2, :cond_2

    .line 396
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;

    move-result-object p1

    if-eqz p1, :cond_3

    const/4 v0, 0x0

    .line 398
    invoke-interface {p1, v0}, Lcom/helpshift/support/contracts/FaqFragmentListener;->onContactUsClicked(Ljava/lang/String;)V

    goto :goto_0

    .line 402
    :cond_2
    invoke-static {p0}, Lcom/helpshift/support/util/FragmentUtil;->getSupportFragment(Landroidx/fragment/app/Fragment;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object p1

    if-eqz p1, :cond_3

    .line 404
    invoke-virtual {p1}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object p1

    .line 405
    invoke-virtual {p1}, Lcom/helpshift/support/controllers/SupportController;->sendAnyway()V

    :cond_3
    :goto_0
    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 2

    .line 138
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onCreate(Landroid/os/Bundle;)V

    .line 139
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    if-eqz p1, :cond_0

    const-string v0, "decomp"

    const/4 v1, 0x0

    .line 141
    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result p1

    iput-boolean p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->decomp:Z

    :cond_0
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 148
    sget p3, Lcom/helpshift/R$layout;->hs__single_question_fragment:I

    .line 149
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->showRootLayoutInsideCardView:Z

    if-eqz v0, :cond_0

    .line 150
    sget p3, Lcom/helpshift/R$layout;->hs__single_question_layout_with_cardview:I

    :cond_0
    const/4 v0, 0x0

    .line 152
    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroyView()V
    .locals 2

    .line 237
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/SnackbarUtil;->hideSnackbar(Landroid/view/View;)V

    const/4 v0, 0x0

    .line 240
    iput-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooter:Landroid/view/View;

    .line 241
    iget-object v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    invoke-virtual {v1, v0}, Lcom/helpshift/support/webkit/CustomWebView;->setWebViewClient(Landroid/webkit/WebViewClient;)V

    .line 242
    iput-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    .line 243
    iput-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->noButton:Landroid/widget/Button;

    .line 244
    iput-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->yesButton:Landroid/widget/Button;

    .line 245
    iput-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->contactUsButton:Landroid/widget/Button;

    .line 246
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroyView()V

    return-void
.end method

.method public onPageFinished()V
    .locals 2

    .line 435
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isVisible()Z

    move-result v0

    if-eqz v0, :cond_1

    const/4 v0, 0x0

    .line 436
    invoke-direct {p0, v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->showProgress(Z)V

    .line 437
    iget-object v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    iget v1, v1, Lcom/helpshift/support/Faq;->is_helpful:I

    invoke-direct {p0, v1}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->setIsHelpful(I)V

    .line 438
    iget-boolean v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isHighlighted:Z

    if-eqz v1, :cond_0

    .line 439
    iput-boolean v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isHighlighted:Z

    goto :goto_0

    .line 442
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->highlightAndReloadQuestion()V

    .line 444
    :goto_0
    iget-object v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    invoke-virtual {v1, v0}, Lcom/helpshift/support/webkit/CustomWebView;->setBackgroundColor(I)V

    :cond_1
    return-void
.end method

.method public onPageStarted()V
    .locals 2

    const/4 v0, 0x1

    .line 429
    invoke-direct {p0, v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->showProgress(Z)V

    .line 430
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/support/webkit/CustomWebView;->setBackgroundColor(I)V

    return-void
.end method

.method public onPause()V
    .locals 1

    .line 109
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onPause()V

    .line 110
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    invoke-virtual {v0}, Lcom/helpshift/support/webkit/CustomWebView;->onPause()V

    return-void
.end method

.method public onResume()V
    .locals 2

    .line 212
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    .line 214
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isScreenLarge()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 216
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    .line 217
    instance-of v1, v0, Lcom/helpshift/support/fragments/FaqFlowFragment;

    if-eqz v1, :cond_0

    .line 218
    check-cast v0, Lcom/helpshift/support/fragments/FaqFlowFragment;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/support/fragments/FaqFlowFragment;->updateSelectQuestionUI(Z)V

    .line 222
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    invoke-virtual {v0}, Lcom/helpshift/support/webkit/CustomWebView;->onResume()V

    .line 223
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->decomp:Z

    if-nez v0, :cond_1

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isScreenLarge()Z

    move-result v0

    if-nez v0, :cond_2

    .line 224
    :cond_1
    sget v0, Lcom/helpshift/R$string;->hs__question_header:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->setToolbarTitle(Ljava/lang/String;)V

    .line 227
    :cond_2
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    if-eqz v0, :cond_3

    .line 228
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    invoke-virtual {v0}, Lcom/helpshift/support/Faq;->getId()Ljava/lang/String;

    move-result-object v0

    .line 229
    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_3

    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->eventSent:Z

    if-nez v0, :cond_3

    .line 230
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->reportReadFaqEvent()V

    :cond_3
    return-void
.end method

.method public onStart()V
    .locals 1

    .line 101
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStart()V

    .line 102
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    .line 103
    iput-boolean v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->eventSent:Z

    :cond_0
    return-void
.end method

.method public onStop()V
    .locals 1

    .line 115
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStop()V

    .line 116
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->decomp:Z

    if-nez v0, :cond_0

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->isScreenLarge()Z

    move-result v0

    if-nez v0, :cond_1

    .line 117
    :cond_0
    sget v0, Lcom/helpshift/R$string;->hs__help_header:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->setToolbarTitle(Ljava/lang/String;)V

    :cond_1
    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 10

    .line 159
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 161
    sget p2, Lcom/helpshift/R$id;->web_view:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Lcom/helpshift/support/webkit/CustomWebView;

    iput-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    .line 162
    iget-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    new-instance v0, Lcom/helpshift/support/webkit/CustomWebViewClient;

    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-direct {v0, v1, p0}, Lcom/helpshift/support/webkit/CustomWebViewClient;-><init>(Landroid/content/Context;Lcom/helpshift/support/webkit/CustomWebViewClient$CustomWebViewClientListeners;)V

    invoke-virtual {p2, v0}, Lcom/helpshift/support/webkit/CustomWebView;->setWebViewClient(Landroid/webkit/WebViewClient;)V

    .line 164
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p2

    invoke-virtual {p2}, Landroidx/fragment/app/FragmentActivity;->getWindow()Landroid/view/Window;

    move-result-object p2

    invoke-virtual {p2}, Landroid/view/Window;->getDecorView()Landroid/view/View;

    move-result-object p2

    .line 165
    sget v0, Lcom/helpshift/R$id;->faq_content_view:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    .line 166
    iget-object v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    new-instance v2, Lcom/helpshift/support/webkit/CustomWebChromeClient;

    invoke-direct {v2, p2, v0}, Lcom/helpshift/support/webkit/CustomWebChromeClient;-><init>(Landroid/view/View;Landroid/view/View;)V

    invoke-virtual {v1, v2}, Lcom/helpshift/support/webkit/CustomWebView;->setWebChromeClient(Landroid/webkit/WebChromeClient;)V

    .line 168
    sget p2, Lcom/helpshift/R$id;->helpful_button:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/Button;

    iput-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->yesButton:Landroid/widget/Button;

    .line 169
    iget-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->yesButton:Landroid/widget/Button;

    invoke-virtual {p2, p0}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 171
    sget p2, Lcom/helpshift/R$id;->unhelpful_button:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/Button;

    iput-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->noButton:Landroid/widget/Button;

    .line 172
    iget-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->noButton:Landroid/widget/Button;

    invoke-virtual {p2, p0}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 174
    sget p2, Lcom/helpshift/R$id;->question_footer:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooter:Landroid/view/View;

    .line 175
    sget p2, Lcom/helpshift/R$id;->question_footer_message:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/TextView;

    iput-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionFooterMessage:Landroid/widget/TextView;

    .line 177
    sget p2, Lcom/helpshift/R$id;->contact_us_button:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/Button;

    iput-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->contactUsButton:Landroid/widget/Button;

    .line 178
    iget-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->contactUsButton:Landroid/widget/Button;

    invoke-virtual {p2, p0}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 182
    sget p2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v0, 0x18

    if-lt p2, v0, :cond_0

    .line 183
    iget-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->yesButton:Landroid/widget/Button;

    sget v0, Lcom/helpshift/R$string;->hs__mark_yes:I

    invoke-virtual {p2, v0}, Landroid/widget/Button;->setText(I)V

    .line 184
    iget-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->noButton:Landroid/widget/Button;

    sget v0, Lcom/helpshift/R$string;->hs__mark_no:I

    invoke-virtual {p2, v0}, Landroid/widget/Button;->setText(I)V

    .line 185
    iget-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->contactUsButton:Landroid/widget/Button;

    sget v0, Lcom/helpshift/R$string;->hs__contact_us_btn:I

    invoke-virtual {p2, v0}, Landroid/widget/Button;->setText(I)V

    .line 188
    :cond_0
    iget p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->singleQuestionMode:I

    const/4 v0, 0x2

    if-ne p2, v0, :cond_1

    .line 189
    iget-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->contactUsButton:Landroid/widget/Button;

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Lcom/helpshift/R$string;->hs__send_anyway:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p2, v0}, Landroid/widget/Button;->setText(Ljava/lang/CharSequence;)V

    .line 192
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p2

    const-string v0, "questionPublishId"

    invoke-virtual {p2, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionPublishId:Ljava/lang/String;

    .line 193
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p2

    const-string v0, "support_mode"

    invoke-virtual {p2, v0}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result p2

    .line 197
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "questionLanguage"

    const-string v2, ""

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    .line 199
    iget v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->singleQuestionMode:I

    const/4 v1, 0x0

    const/4 v2, 0x1

    const/4 v3, 0x3

    if-ne v0, v3, :cond_2

    const/4 v7, 0x1

    goto :goto_0

    :cond_2
    const/4 v7, 0x0

    :goto_0
    if-nez v7, :cond_4

    if-ne p2, v3, :cond_3

    goto :goto_1

    :cond_3
    const/4 v6, 0x0

    goto :goto_2

    :cond_4
    :goto_1
    const/4 v6, 0x1

    .line 203
    :goto_2
    iget-object v3, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->data:Lcom/helpshift/support/HSApiData;

    new-instance v4, Lcom/helpshift/support/fragments/SingleQuestionFragment$Success;

    invoke-direct {v4, p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment$Success;-><init>(Lcom/helpshift/support/fragments/SingleQuestionFragment;)V

    new-instance v5, Lcom/helpshift/support/fragments/SingleQuestionFragment$Failure;

    invoke-direct {v5, p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment$Failure;-><init>(Lcom/helpshift/support/fragments/SingleQuestionFragment;)V

    iget-object v8, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionPublishId:Ljava/lang/String;

    invoke-virtual/range {v3 .. v9}, Lcom/helpshift/support/HSApiData;->getQuestion(Landroid/os/Handler;Landroid/os/Handler;ZZLjava/lang/String;Ljava/lang/String;)V

    .line 207
    sget p2, Lcom/helpshift/R$id;->progress_bar:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->progressBar:Landroid/view/View;

    return-void
.end method

.method reportReadFaqEvent()V
    .locals 3

    .line 549
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "id"

    .line 550
    iget-object v2, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    invoke-virtual {v2}, Lcom/helpshift/support/Faq;->getId()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "nt"

    .line 551
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getContext()Landroid/content/Context;

    move-result-object v2

    invoke-static {v2}, Lcom/helpshift/util/HelpshiftConnectionUtil;->isOnline(Landroid/content/Context;)Z

    move-result v2

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 552
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v1

    sget-object v2, Lcom/helpshift/analytics/AnalyticsEventType;->READ_FAQ:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v1, v2, v0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;Ljava/util/Map;)V

    .line 553
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionReadListener:Lcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;

    if-eqz v0, :cond_0

    .line 554
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->questionReadListener:Lcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;

    iget-object v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    invoke-virtual {v1}, Lcom/helpshift/support/Faq;->getId()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Lcom/helpshift/support/fragments/SingleQuestionFragment$QuestionReadListener;->onQuestionRead(Ljava/lang/String;)V

    :cond_0
    const/4 v0, 0x1

    .line 556
    iput-boolean v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->eventSent:Z

    return-void
.end method

.method setQuestion(Lcom/helpshift/support/Faq;)V
    .locals 7

    .line 250
    iput-object p1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->question:Lcom/helpshift/support/Faq;

    .line 251
    iget-object v0, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    if-eqz v0, :cond_0

    .line 252
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getColorsFromTheme(Landroid/content/Context;)V

    .line 253
    iget-object v1, p0, Lcom/helpshift/support/fragments/SingleQuestionFragment;->webView:Lcom/helpshift/support/webkit/CustomWebView;

    const/4 v2, 0x0

    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SingleQuestionFragment;->getStyledBody(Lcom/helpshift/support/Faq;)Ljava/lang/String;

    move-result-object v3

    const-string v4, "text/html"

    const-string v5, "utf-8"

    const/4 v6, 0x0

    invoke-virtual/range {v1 .. v6}, Lcom/helpshift/support/webkit/CustomWebView;->loadDataWithBaseURL(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.class public Lcom/helpshift/support/webkit/CustomWebView;
.super Landroid/webkit/WebView;
.source "CustomWebView.java"


# direct methods
.method public constructor <init>(Landroid/content/Context;Landroid/util/AttributeSet;)V
    .locals 0

    .line 10
    invoke-direct {p0, p1, p2}, Landroid/webkit/WebView;-><init>(Landroid/content/Context;Landroid/util/AttributeSet;)V

    .line 11
    invoke-virtual {p0}, Lcom/helpshift/support/webkit/CustomWebView;->isInEditMode()Z

    move-result p1

    if-nez p1, :cond_0

    .line 12
    invoke-direct {p0}, Lcom/helpshift/support/webkit/CustomWebView;->configureWebView()V

    :cond_0
    return-void
.end method

.method private configureWebView()V
    .locals 2

    .line 17
    invoke-virtual {p0}, Lcom/helpshift/support/webkit/CustomWebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v0

    const/4 v1, 0x1

    .line 18
    invoke-virtual {v0, v1}, Landroid/webkit/WebSettings;->setJavaScriptEnabled(Z)V

    return-void
.end method

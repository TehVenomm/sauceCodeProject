.class Lnet/gree/unitywebview/WebViewPlugin$1$1;
.super Landroid/webkit/WebViewClient;
.source "WebViewPlugin.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gree/unitywebview/WebViewPlugin$1;->run()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gree/unitywebview/WebViewPlugin$1;


# direct methods
.method constructor <init>(Lnet/gree/unitywebview/WebViewPlugin$1;)V
    .locals 0

    .line 62
    iput-object p1, p0, Lnet/gree/unitywebview/WebViewPlugin$1$1;->this$1:Lnet/gree/unitywebview/WebViewPlugin$1;

    invoke-direct {p0}, Landroid/webkit/WebViewClient;-><init>()V

    return-void
.end method


# virtual methods
.method ChangeActivity(Ljava/lang/String;)V
    .locals 7
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "WrongConstant"
        }
    .end annotation

    .line 102
    iget-object v0, p0, Lnet/gree/unitywebview/WebViewPlugin$1$1;->this$1:Lnet/gree/unitywebview/WebViewPlugin$1;

    iget-object v0, v0, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lnet/gree/unitywebview/WebViewPlugin;->access$200(Lnet/gree/unitywebview/WebViewPlugin;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 105
    :cond_0
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 106
    new-instance v1, Landroid/content/Intent;

    invoke-direct {v1}, Landroid/content/Intent;-><init>()V

    const-string v2, "url"

    .line 107
    invoke-virtual {v1, v2, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string v2, "gameobject"

    .line 108
    iget-object v3, p0, Lnet/gree/unitywebview/WebViewPlugin$1$1;->this$1:Lnet/gree/unitywebview/WebViewPlugin$1;

    iget-object v3, v3, Lnet/gree/unitywebview/WebViewPlugin$1;->val$gameObject:Ljava/lang/String;

    invoke-virtual {v1, v2, v3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string v2, "margin_left"

    .line 109
    iget-object v3, p0, Lnet/gree/unitywebview/WebViewPlugin$1$1;->this$1:Lnet/gree/unitywebview/WebViewPlugin$1;

    iget-object v3, v3, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v3}, Lnet/gree/unitywebview/WebViewPlugin;->access$300(Lnet/gree/unitywebview/WebViewPlugin;)I

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    const-string v2, "margin_right"

    .line 110
    iget-object v3, p0, Lnet/gree/unitywebview/WebViewPlugin$1$1;->this$1:Lnet/gree/unitywebview/WebViewPlugin$1;

    iget-object v3, v3, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v3}, Lnet/gree/unitywebview/WebViewPlugin;->access$400(Lnet/gree/unitywebview/WebViewPlugin;)I

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    const-string v2, "margin_top"

    .line 111
    iget-object v3, p0, Lnet/gree/unitywebview/WebViewPlugin$1$1;->this$1:Lnet/gree/unitywebview/WebViewPlugin$1;

    iget-object v3, v3, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v3}, Lnet/gree/unitywebview/WebViewPlugin;->access$500(Lnet/gree/unitywebview/WebViewPlugin;)I

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    const-string v2, "margin_bottom"

    .line 112
    iget-object v3, p0, Lnet/gree/unitywebview/WebViewPlugin$1$1;->this$1:Lnet/gree/unitywebview/WebViewPlugin$1;

    iget-object v3, v3, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v3}, Lnet/gree/unitywebview/WebViewPlugin;->access$600(Lnet/gree/unitywebview/WebViewPlugin;)I

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    const/4 v2, 0x0

    const-string v3, "/"

    const/16 v4, 0xa

    .line 113
    invoke-virtual {p1, v3, v4}, Ljava/lang/String;->indexOf(Ljava/lang/String;I)I

    move-result v3

    invoke-virtual {p1, v2, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object p1

    const-string v2, "http://"

    const-string v3, ""

    invoke-virtual {p1, v2, v3}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p1

    const-string v2, "https://"

    const-string v3, ""

    invoke-virtual {p1, v2, v3}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p1

    const-string v2, ""

    .line 115
    iget-object v3, p0, Lnet/gree/unitywebview/WebViewPlugin$1$1;->this$1:Lnet/gree/unitywebview/WebViewPlugin$1;

    iget-object v3, v3, Lnet/gree/unitywebview/WebViewPlugin$1;->this$0:Lnet/gree/unitywebview/WebViewPlugin;

    invoke-static {v3}, Lnet/gree/unitywebview/WebViewPlugin;->access$700(Lnet/gree/unitywebview/WebViewPlugin;)Ljava/util/HashMap;

    move-result-object v3

    invoke-virtual {v3}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_1
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_2

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/Map$Entry;

    .line 116
    invoke-interface {v4}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    invoke-virtual {v5, p1}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v5

    if-ltz v5, :cond_1

    .line 117
    invoke-interface {v4}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/ArrayList;

    invoke-virtual {v4}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_0
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_1

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    .line 118
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v2, ";"

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    goto :goto_0

    :cond_2
    const-string p1, "cookie"

    .line 122
    invoke-virtual {v1, p1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const/high16 p1, 0x10000

    .line 123
    invoke-virtual {v1, p1}, Landroid/content/Intent;->setFlags(I)Landroid/content/Intent;

    const-string p1, "jp.colopl.wcat"

    const-string v2, "jp.colopl.wcat.WebViewActivity"

    .line 124
    invoke-virtual {v1, p1, v2}, Landroid/content/Intent;->setClassName(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 125
    invoke-virtual {v0, v1}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method isWebViewActivityUrl(Ljava/lang/String;)Z
    .locals 1

    const-string v0, "/opinion"

    .line 65
    invoke-virtual {p1, v0}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result p1

    if-ltz p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public onLoadResource(Landroid/webkit/WebView;Ljava/lang/String;)V
    .locals 1

    .line 80
    invoke-virtual {p0, p2}, Lnet/gree/unitywebview/WebViewPlugin$1$1;->isWebViewActivityUrl(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 82
    invoke-virtual {p0, p2}, Lnet/gree/unitywebview/WebViewPlugin$1$1;->ChangeActivity(Ljava/lang/String;)V

    return-void

    .line 85
    :cond_0
    invoke-super {p0, p1, p2}, Landroid/webkit/WebViewClient;->onLoadResource(Landroid/webkit/WebView;Ljava/lang/String;)V

    return-void
.end method

.method public onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V
    .locals 1

    .line 90
    invoke-virtual {p0, p2}, Lnet/gree/unitywebview/WebViewPlugin$1$1;->isWebViewActivityUrl(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 92
    invoke-virtual {p1}, Landroid/webkit/WebView;->stopLoading()V

    .line 93
    invoke-virtual {p0, p2}, Lnet/gree/unitywebview/WebViewPlugin$1$1;->ChangeActivity(Ljava/lang/String;)V

    return-void

    .line 96
    :cond_0
    invoke-super {p0, p1, p2, p3}, Landroid/webkit/WebViewClient;->onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V

    return-void
.end method

.method public shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z
    .locals 0

    .line 70
    invoke-virtual {p0, p2}, Lnet/gree/unitywebview/WebViewPlugin$1$1;->isWebViewActivityUrl(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 72
    invoke-virtual {p0, p2}, Lnet/gree/unitywebview/WebViewPlugin$1$1;->ChangeActivity(Ljava/lang/String;)V

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

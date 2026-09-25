.class Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;
.super Landroid/webkit/WebViewClient;
.source "AbstractWebViewFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private currentUrl:Ljava/lang/String;

.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)V
    .locals 0

    .line 195
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-direct {p0}, Landroid/webkit/WebViewClient;-><init>()V

    const/4 p1, 0x0

    .line 197
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->currentUrl:Ljava/lang/String;

    return-void
.end method

.method private handleError(Landroid/webkit/WebView;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 291
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->currentUrl:Ljava/lang/String;

    invoke-static {p2, v0}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 294
    :cond_0
    invoke-virtual {p1}, Landroid/webkit/WebView;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/support/NetworkUtils;->isNetworkAvailable(Landroid/content/Context;)Z

    move-result v0

    if-nez v0, :cond_1

    return-void

    .line 298
    :cond_1
    :try_start_0
    invoke-virtual {p1}, Landroid/webkit/WebView;->stopLoading()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 302
    :catch_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 303
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object v1

    invoke-static {v0, v1, p3, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p3

    invoke-static {p1, p3, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$400(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;Ljava/lang/String;Ljava/lang/String;)V

    :cond_2
    return-void
.end method

.method private handleUri(Landroid/net/Uri;)Z
    .locals 4

    .line 200
    invoke-virtual {p1}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v0

    const-string v1, "market"

    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x1

    if-nez v0, :cond_10

    .line 201
    invoke-virtual {p1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v0

    const-string v2, "https://play.google.com/"

    invoke-virtual {v0, v2}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto/16 :goto_3

    .line 207
    :cond_0
    invoke-virtual {p1}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v0

    const-string v2, "share-"

    invoke-static {v0, v2}, Lnet/gogame/gowrap/support/StringUtils;->startsWith(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 208
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object v0

    if-eqz v0, :cond_11

    .line 209
    invoke-virtual {p1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object p1

    const-string v0, "share-"

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    invoke-virtual {p1, v0}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object p1

    .line 210
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    const-string v2, "android.intent.action.SEND"

    .line 211
    invoke-virtual {v0, v2}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    const-string v2, "android.intent.extra.TEXT"

    .line 212
    invoke-virtual {v0, v2, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "text/plain"

    .line 213
    invoke-virtual {v0, p1}, Landroid/content/Intent;->setType(Ljava/lang/String;)Landroid/content/Intent;

    .line 214
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object p1

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    sget v3, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_share_prompt:I

    .line 215
    invoke-virtual {v2, v3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->getString(I)Ljava/lang/String;

    move-result-object v2

    .line 214
    invoke-static {v0, v2}, Landroid/content/Intent;->createChooser(Landroid/content/Intent;Ljava/lang/CharSequence;)Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    return v1

    .line 218
    :cond_1
    invoke-virtual {p1}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v0

    const-string v2, "community"

    invoke-static {v0, v2}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_a

    .line 219
    invoke-virtual {p1}, Landroid/net/Uri;->getSchemeSpecificPart()Ljava/lang/String;

    move-result-object p1

    .line 220
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    .line 221
    invoke-static {v2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object v2

    invoke-virtual {v0, v2}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getLocaleConfiguration(Landroid/content/Context;)Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

    move-result-object v0

    if-eqz v0, :cond_9

    const/4 v2, 0x0

    const-string v3, "home"

    .line 224
    invoke-static {p1, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_2

    .line 226
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getWhatsNewUrl()Ljava/lang/String;

    move-result-object v2

    goto :goto_0

    :cond_2
    const-string v3, "facebook"

    .line 227
    invoke-static {p1, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_3

    .line 229
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getFacebookUrl()Ljava/lang/String;

    move-result-object v2

    goto :goto_0

    :cond_3
    const-string v3, "twitter"

    .line 230
    invoke-static {p1, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_4

    .line 232
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getTwitterUrl()Ljava/lang/String;

    move-result-object v2

    goto :goto_0

    :cond_4
    const-string v3, "instagram"

    .line 233
    invoke-static {p1, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_5

    .line 235
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getInstagramUrl()Ljava/lang/String;

    move-result-object v2

    goto :goto_0

    :cond_5
    const-string v3, "youtube"

    .line 236
    invoke-static {p1, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_6

    .line 238
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getYoutubeUrl()Ljava/lang/String;

    move-result-object v2

    goto :goto_0

    :cond_6
    const-string v3, "forum"

    .line 239
    invoke-static {p1, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_7

    .line 241
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getForumUrl()Ljava/lang/String;

    move-result-object v2

    goto :goto_0

    :cond_7
    const-string v3, "wiki"

    .line 242
    invoke-static {p1, v3}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_8

    .line 244
    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;->getWikiUrl()Ljava/lang/String;

    move-result-object v2

    :cond_8
    :goto_0
    if-eqz v2, :cond_9

    .line 247
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$200(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;

    move-result-object p1

    invoke-virtual {p1, v2}, Lnet/gogame/gowrap/ui/cpr/VideoEnabledWebView;->loadUrl(Ljava/lang/String;)V

    :cond_9
    return v1

    .line 251
    :cond_a
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->handleCustomUri(Landroid/net/Uri;)Z

    move-result v0

    if-eqz v0, :cond_b

    return v1

    .line 254
    :cond_b
    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v0

    const-string v2, "www.youtube.com"

    invoke-static {v0, v2}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_f

    .line 255
    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v0

    const-string v2, "youtu.be"

    invoke-static {v0, v2}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_f

    .line 256
    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v0

    const-string v2, ".youtube.com"

    invoke-static {v0, v2}, Lnet/gogame/gowrap/support/StringUtils;->endsWith(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_f

    .line 257
    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v0

    const-string v2, ".youtu.be"

    invoke-static {v0, v2}, Lnet/gogame/gowrap/support/StringUtils;->endsWith(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_c

    goto :goto_2

    .line 261
    :cond_c
    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v0

    const-string v2, "vimeo.com"

    invoke-static {v0, v2}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_e

    .line 262
    invoke-virtual {p1}, Landroid/net/Uri;->getHost()Ljava/lang/String;

    move-result-object v0

    const-string v2, ".vimeo.com"

    invoke-static {v0, v2}, Lnet/gogame/gowrap/support/StringUtils;->endsWith(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_d

    goto :goto_1

    .line 265
    :cond_d
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->doHandleUri(Landroid/net/Uri;)Z

    move-result p1

    if-eqz p1, :cond_11

    return v1

    .line 263
    :cond_e
    :goto_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    new-instance v2, Landroid/content/Intent;

    const-string v3, "android.intent.action.VIEW"

    invoke-direct {v2, v3, p1}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    invoke-virtual {v0, v2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->startActivity(Landroid/content/Intent;)V

    return v1

    .line 258
    :cond_f
    :goto_2
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    new-instance v2, Landroid/content/Intent;

    const-string v3, "android.intent.action.VIEW"

    invoke-direct {v2, v3, p1}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    invoke-virtual {v0, v2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->startActivity(Landroid/content/Intent;)V

    return v1

    .line 202
    :cond_10
    :goto_3
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object v0

    if-eqz v0, :cond_11

    .line 203
    new-instance v0, Landroid/content/Intent;

    const-string v2, "android.intent.action.VIEW"

    invoke-direct {v0, v2, p1}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    .line 204
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object p1

    invoke-virtual {p1, v0}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    return v1

    :cond_11
    const/4 p1, 0x0

    return p1
.end method


# virtual methods
.method public onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V
    .locals 4

    .line 309
    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->currentUrl:Ljava/lang/String;

    .line 310
    invoke-static {p2}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v0

    .line 311
    invoke-virtual {v0}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v1

    const-string v2, "data"

    invoke-static {v1, v2}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    const/4 v2, 0x1

    xor-int/2addr v1, v2

    if-eqz v1, :cond_1

    .line 312
    invoke-virtual {p1}, Landroid/webkit/WebView;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-static {v1}, Lnet/gogame/gowrap/support/NetworkUtils;->isNetworkAvailable(Landroid/content/Context;)Z

    move-result v1

    if-nez v1, :cond_1

    .line 313
    invoke-virtual {p1}, Landroid/webkit/WebView;->stopLoading()V

    .line 314
    iget-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {p3}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object p3

    if-eqz p3, :cond_0

    .line 315
    iget-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Landroid/content/Context;

    move-result-object v1

    invoke-virtual {p1}, Landroid/webkit/WebView;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    sget v2, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_network_no_connection_message:I

    invoke-virtual {p1, v2}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    const-string v2, ""

    invoke-static {v0, v1, p1, v2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p3, p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$400(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    return-void

    .line 321
    :cond_1
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-virtual {v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->getBackgroundMode(Landroid/net/Uri;)Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    move-result-object v0

    .line 322
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$500(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;)Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    move-result-object v1

    if-eq v1, v0, :cond_3

    .line 323
    sget-object v1, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$4;->$SwitchMap$net$gogame$gowrap$ui$v2017_1$AbstractWebViewFragment$BackgroundMode:[I

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;->ordinal()I

    move-result v3

    aget v1, v1, v3

    if-eq v1, v2, :cond_2

    const/4 v1, -0x1

    .line 329
    invoke-virtual {p1, v1}, Landroid/webkit/WebView;->setBackgroundColor(I)V

    goto :goto_0

    :cond_2
    const/4 v1, 0x0

    .line 325
    invoke-virtual {p1, v1}, Landroid/webkit/WebView;->setBackgroundColor(I)V

    .line 332
    :goto_0
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;

    invoke-static {v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;->access$502(Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment;Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;)Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$BackgroundMode;

    .line 334
    :cond_3
    invoke-super {p0, p1, p2, p3}, Landroid/webkit/WebViewClient;->onPageStarted(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V

    return-void
.end method

.method public onReceivedError(Landroid/webkit/WebView;ILjava/lang/String;Ljava/lang/String;)V
    .locals 5

    .line 341
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v0

    const-string v1, "%s (%d)"

    const/4 v2, 0x2

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    aput-object p3, v2, v3

    .line 342
    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    const/4 v4, 0x1

    aput-object v3, v2, v4

    .line 341
    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 343
    invoke-direct {p0, p1, p4, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->handleError(Landroid/webkit/WebView;Ljava/lang/String;Ljava/lang/String;)V

    .line 344
    invoke-super {p0, p1, p2, p3, p4}, Landroid/webkit/WebViewClient;->onReceivedError(Landroid/webkit/WebView;ILjava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public onReceivedError(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;Landroid/webkit/WebResourceError;)V
    .locals 5
    .annotation build Landroid/annotation/TargetApi;
        value = 0x17
    .end annotation

    .line 351
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v0

    const-string v1, "%s (%d)"

    const/4 v2, 0x2

    new-array v2, v2, [Ljava/lang/Object;

    .line 352
    invoke-virtual {p3}, Landroid/webkit/WebResourceError;->getDescription()Ljava/lang/CharSequence;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    invoke-virtual {p3}, Landroid/webkit/WebResourceError;->getErrorCode()I

    move-result v3

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    const/4 v4, 0x1

    aput-object v3, v2, v4

    .line 351
    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 353
    invoke-interface {p2}, Landroid/webkit/WebResourceRequest;->getUrl()Landroid/net/Uri;

    move-result-object v1

    invoke-virtual {v1}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, p1, v1, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->handleError(Landroid/webkit/WebView;Ljava/lang/String;Ljava/lang/String;)V

    .line 354
    invoke-super {p0, p1, p2, p3}, Landroid/webkit/WebViewClient;->onReceivedError(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;Landroid/webkit/WebResourceError;)V

    return-void
.end method

.method public onReceivedHttpError(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;Landroid/webkit/WebResourceResponse;)V
    .locals 6
    .annotation build Landroid/annotation/TargetApi;
        value = 0x17
    .end annotation

    .line 361
    invoke-interface {p2}, Landroid/webkit/WebResourceRequest;->getUrl()Landroid/net/Uri;

    move-result-object v0

    invoke-virtual {v0}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v0

    .line 362
    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v1

    const-string v2, "%d %s"

    const/4 v3, 0x2

    new-array v3, v3, [Ljava/lang/Object;

    invoke-virtual {p3}, Landroid/webkit/WebResourceResponse;->getStatusCode()I

    move-result v4

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    const/4 v5, 0x0

    aput-object v4, v3, v5

    .line 363
    invoke-virtual {p3}, Landroid/webkit/WebResourceResponse;->getReasonPhrase()Ljava/lang/String;

    move-result-object v4

    const/4 v5, 0x1

    aput-object v4, v3, v5

    .line 361
    invoke-static {v1, v2, v3}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, p1, v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->handleError(Landroid/webkit/WebView;Ljava/lang/String;Ljava/lang/String;)V

    .line 364
    invoke-super {p0, p1, p2, p3}, Landroid/webkit/WebViewClient;->onReceivedHttpError(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;Landroid/webkit/WebResourceResponse;)V

    return-void
.end method

.method public onReceivedSslError(Landroid/webkit/WebView;Landroid/webkit/SslErrorHandler;Landroid/net/http/SslError;)V
    .locals 3

    .line 369
    invoke-virtual {p3}, Landroid/net/http/SslError;->getUrl()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1}, Landroid/webkit/WebView;->getContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    sget v2, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_network_ssl_error_message:I

    invoke-virtual {v1, v2}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, p1, v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->handleError(Landroid/webkit/WebView;Ljava/lang/String;Ljava/lang/String;)V

    .line 371
    invoke-super {p0, p1, p2, p3}, Landroid/webkit/WebViewClient;->onReceivedSslError(Landroid/webkit/WebView;Landroid/webkit/SslErrorHandler;Landroid/net/http/SslError;)V

    return-void
.end method

.method public shouldOverrideUrlLoading(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z
    .locals 1
    .annotation build Landroid/annotation/TargetApi;
        value = 0x18
    .end annotation

    .line 274
    invoke-interface {p2}, Landroid/webkit/WebResourceRequest;->getUrl()Landroid/net/Uri;

    move-result-object v0

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->handleUri(Landroid/net/Uri;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p1, 0x1

    return p1

    .line 277
    :cond_0
    invoke-super {p0, p1, p2}, Landroid/webkit/WebViewClient;->shouldOverrideUrlLoading(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z

    move-result p1

    return p1
.end method

.method public shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z
    .locals 1

    .line 283
    invoke-static {p2}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v0

    .line 284
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/v2017_1/AbstractWebViewFragment$3;->handleUri(Landroid/net/Uri;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 p1, 0x1

    return p1

    .line 287
    :cond_0
    invoke-super {p0, p1, p2}, Landroid/webkit/WebViewClient;->shouldOverrideUrlLoading(Landroid/webkit/WebView;Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

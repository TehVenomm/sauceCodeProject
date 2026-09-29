.class public abstract Lnet/gogame/gowrap/ui/AbstractMainActivity;
.super Landroid/app/Activity;
.source "AbstractMainActivity.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/UIContext;


# static fields
.field private static diskLruCache:Lnet/gogame/gowrap/support/DiskLruCache;

.field private static downloadManager:Lnet/gogame/gowrap/support/DownloadManager;


# instance fields
.field private fullscreen:Z

.field private final listener:Lnet/gogame/gowrap/GoWrapImpl$Listener;

.field protected localeConfiguration:Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

.field protected localeManager:Lnet/gogame/gowrap/support/LocaleManager;

.field private preFullscreenOrientation:Ljava/lang/Integer;


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 41
    invoke-direct {p0}, Landroid/app/Activity;-><init>()V

    .line 45
    new-instance v0, Lnet/gogame/gowrap/ui/AbstractMainActivity$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity$1;-><init>(Lnet/gogame/gowrap/ui/AbstractMainActivity;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->listener:Lnet/gogame/gowrap/GoWrapImpl$Listener;

    const/4 v0, 0x0

    .line 61
    iput-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->localeManager:Lnet/gogame/gowrap/support/LocaleManager;

    const/4 v1, 0x0

    .line 63
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->fullscreen:Z

    .line 64
    iput-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->preFullscreenOrientation:Ljava/lang/Integer;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/AbstractMainActivity;Lnet/gogame/gowrap/VipStatus;)Z
    .locals 0

    .line 41
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->isVipChatEnabled(Lnet/gogame/gowrap/VipStatus;)Z

    move-result p0

    return p0
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/ui/AbstractMainActivity;)V
    .locals 0

    .line 41
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->enableVipChat()V

    return-void
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/ui/AbstractMainActivity;)V
    .locals 0

    .line 41
    invoke-direct {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->disableVipChat()V

    return-void
.end method

.method private disableVipChat()V
    .locals 2

    const-string v0, "goWrap"

    const-string v1, "VIP chat disabled"

    .line 317
    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 318
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object v0

    .line 319
    instance-of v1, v0, Lnet/gogame/gowrap/ui/VipListener;

    if-eqz v1, :cond_0

    .line 320
    check-cast v0, Lnet/gogame/gowrap/ui/VipListener;

    .line 321
    invoke-interface {v0}, Lnet/gogame/gowrap/ui/VipListener;->onDisableVipChat()V

    :cond_0
    return-void
.end method

.method private enableVipChat()V
    .locals 2

    const-string v0, "goWrap"

    const-string v1, "VIP chat enabled"

    .line 308
    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 309
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object v0

    .line 310
    instance-of v1, v0, Lnet/gogame/gowrap/ui/VipListener;

    if-eqz v1, :cond_0

    .line 311
    check-cast v0, Lnet/gogame/gowrap/ui/VipListener;

    .line 312
    invoke-interface {v0}, Lnet/gogame/gowrap/ui/VipListener;->onEnableVipChat()V

    :cond_0
    return-void
.end method

.method private isVipChatEnabled(Lnet/gogame/gowrap/VipStatus;)Z
    .locals 1

    .line 303
    sget-object v0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->isForceEnableChat()Z

    move-result v0

    if-nez v0, :cond_1

    if-eqz p1, :cond_0

    .line 304
    invoke-virtual {p1}, Lnet/gogame/gowrap/VipStatus;->isVip()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p1}, Lnet/gogame/gowrap/VipStatus;->isSuspended()Z

    move-result p1

    if-nez p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method


# virtual methods
.method protected canShowLanguageMenu()Z
    .locals 2

    .line 255
    iget-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->localeManager:Lnet/gogame/gowrap/support/LocaleManager;

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->localeManager:Lnet/gogame/gowrap/support/LocaleManager;

    invoke-virtual {v0}, Lnet/gogame/gowrap/support/LocaleManager;->getSupportedLocaleDescriptors()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->localeManager:Lnet/gogame/gowrap/support/LocaleManager;

    .line 256
    invoke-virtual {v0}, Lnet/gogame/gowrap/support/LocaleManager;->getSupportedLocaleDescriptors()Ljava/util/List;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-le v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public clearFragments()V
    .locals 3

    .line 139
    invoke-static {p0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->hideSoftKeyboard(Landroid/app/Activity;)V

    .line 140
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getFragmentManager()Landroid/app/FragmentManager;

    move-result-object v0

    const/4 v1, 0x0

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Landroid/app/FragmentManager;->popBackStack(Ljava/lang/String;I)V

    return-void
.end method

.method protected abstract enableOffers()V
.end method

.method public enterFullscreen(Ljava/lang/Integer;)V
    .locals 4

    .line 180
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->fullscreen:Z

    if-eqz v0, :cond_0

    return-void

    .line 184
    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->onEnterFullscreen()V

    .line 186
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getWindow()Landroid/view/Window;

    move-result-object v0

    .line 187
    invoke-virtual {v0}, Landroid/view/Window;->getAttributes()Landroid/view/WindowManager$LayoutParams;

    move-result-object v1

    .line 188
    iget v2, v1, Landroid/view/WindowManager$LayoutParams;->flags:I

    or-int/lit16 v2, v2, 0x400

    iput v2, v1, Landroid/view/WindowManager$LayoutParams;->flags:I

    .line 189
    iget v2, v1, Landroid/view/WindowManager$LayoutParams;->flags:I

    or-int/lit16 v2, v2, 0x80

    iput v2, v1, Landroid/view/WindowManager$LayoutParams;->flags:I

    .line 190
    invoke-virtual {v0, v1}, Landroid/view/Window;->setAttributes(Landroid/view/WindowManager$LayoutParams;)V

    .line 191
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0xe

    const/4 v3, 0x1

    if-lt v1, v2, :cond_1

    .line 192
    invoke-virtual {v0}, Landroid/view/Window;->getDecorView()Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0, v3}, Landroid/view/View;->setSystemUiVisibility(I)V

    :cond_1
    if-eqz p1, :cond_2

    .line 197
    invoke-static {p0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->getScreenOrientation(Landroid/app/Activity;)I

    move-result v0

    .line 198
    invoke-static {v0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->getBaseScreenOrientation(I)I

    move-result v1

    .line 199
    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result v2

    invoke-static {v2}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->getBaseScreenOrientation(I)I

    move-result v2

    if-eq v1, v2, :cond_2

    .line 200
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->preFullscreenOrientation:Ljava/lang/Integer;

    .line 201
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-ge v0, v1, :cond_2

    .line 202
    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->setRequestedOrientation(I)V

    .line 207
    :cond_2
    iput-boolean v3, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->fullscreen:Z

    return-void
.end method

.method public exitFullscreen()V
    .locals 4

    .line 212
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->fullscreen:Z

    if-nez v0, :cond_0

    return-void

    .line 216
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->preFullscreenOrientation:Ljava/lang/Integer;

    if-eqz v0, :cond_2

    .line 217
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-ge v0, v1, :cond_1

    .line 218
    iget-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->preFullscreenOrientation:Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->setRequestedOrientation(I)V

    :cond_1
    const/4 v0, 0x0

    .line 220
    iput-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->preFullscreenOrientation:Ljava/lang/Integer;

    .line 223
    :cond_2
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getWindow()Landroid/view/Window;

    move-result-object v0

    .line 224
    invoke-virtual {v0}, Landroid/view/Window;->getAttributes()Landroid/view/WindowManager$LayoutParams;

    move-result-object v1

    .line 225
    iget v2, v1, Landroid/view/WindowManager$LayoutParams;->flags:I

    and-int/lit16 v2, v2, -0x401

    iput v2, v1, Landroid/view/WindowManager$LayoutParams;->flags:I

    .line 226
    iget v2, v1, Landroid/view/WindowManager$LayoutParams;->flags:I

    and-int/lit16 v2, v2, -0x81

    iput v2, v1, Landroid/view/WindowManager$LayoutParams;->flags:I

    .line 227
    invoke-virtual {v0, v1}, Landroid/view/Window;->setAttributes(Landroid/view/WindowManager$LayoutParams;)V

    .line 228
    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0xe

    const/4 v3, 0x0

    if-lt v1, v2, :cond_3

    .line 229
    invoke-virtual {v0}, Landroid/view/Window;->getDecorView()Landroid/view/View;

    move-result-object v0

    invoke-virtual {v0, v3}, Landroid/view/View;->setSystemUiVisibility(I)V

    .line 232
    :cond_3
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->onExitFullscreen()V

    .line 234
    iput-boolean v3, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->fullscreen:Z

    return-void
.end method

.method protected getActiveFragment()Landroid/app/Fragment;
    .locals 2

    .line 260
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getFragmentManager()Landroid/app/FragmentManager;

    move-result-object v0

    .line 261
    invoke-virtual {v0}, Landroid/app/FragmentManager;->getBackStackEntryCount()I

    move-result v1

    if-nez v1, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 265
    :cond_0
    invoke-virtual {v0}, Landroid/app/FragmentManager;->getBackStackEntryCount()I

    move-result v1

    add-int/lit8 v1, v1, -0x1

    invoke-virtual {v0, v1}, Landroid/app/FragmentManager;->getBackStackEntryAt(I)Landroid/app/FragmentManager$BackStackEntry;

    move-result-object v1

    .line 266
    invoke-interface {v1}, Landroid/app/FragmentManager$BackStackEntry;->getName()Ljava/lang/String;

    move-result-object v1

    .line 267
    invoke-virtual {v0, v1}, Landroid/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroid/app/Fragment;

    move-result-object v0

    return-object v0
.end method

.method public getDownloadManager()Lnet/gogame/gowrap/support/DownloadManager;
    .locals 1

    .line 239
    sget-object v0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->downloadManager:Lnet/gogame/gowrap/support/DownloadManager;

    return-object v0
.end method

.method protected abstract getFragmentContainerViewId()I
.end method

.method public goBack()Z
    .locals 5

    .line 327
    invoke-static {p0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->hideSoftKeyboard(Landroid/app/Activity;)V

    .line 328
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getFragmentManager()Landroid/app/FragmentManager;

    move-result-object v0

    .line 329
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object v1

    .line 330
    instance-of v2, v1, Lnet/gogame/gowrap/ui/BackPressedListener;

    const/4 v3, 0x1

    if-eqz v2, :cond_0

    .line 331
    check-cast v1, Lnet/gogame/gowrap/ui/BackPressedListener;

    .line 332
    invoke-interface {v1}, Lnet/gogame/gowrap/ui/BackPressedListener;->onBackPressed()Z

    move-result v1

    if-eqz v1, :cond_0

    return v3

    .line 336
    :cond_0
    invoke-virtual {v0}, Landroid/app/FragmentManager;->getBackStackEntryCount()I

    move-result v1

    if-le v1, v3, :cond_1

    .line 338
    :try_start_0
    invoke-virtual {v0}, Landroid/app/FragmentManager;->popBackStack()V
    :try_end_0
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_0

    return v3

    :catch_0
    move-exception v1

    const-string v2, "goWrap"

    const-string v4, "Exception"

    .line 341
    invoke-static {v2, v4, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 344
    :cond_1
    invoke-virtual {v0}, Landroid/app/FragmentManager;->getBackStackEntryCount()I

    move-result v0

    if-ne v0, v3, :cond_2

    .line 345
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->finish()V

    return v3

    :cond_2
    const/4 v0, 0x0

    return v0
.end method

.method protected isUseNews2017_2()Z
    .locals 2

    .line 295
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getConfiguration()Lnet/gogame/gowrap/model/configuration/Configuration;

    move-result-object v0

    if-eqz v0, :cond_0

    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    .line 296
    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getConfiguration()Lnet/gogame/gowrap/model/configuration/Configuration;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getSettings()Lnet/gogame/gowrap/model/configuration/Configuration$Settings;

    move-result-object v0

    if-eqz v0, :cond_0

    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    .line 297
    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getConfiguration()Lnet/gogame/gowrap/model/configuration/Configuration;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration;->getSettings()Lnet/gogame/gowrap/model/configuration/Configuration$Settings;

    move-result-object v0

    invoke-virtual {v0}, Lnet/gogame/gowrap/model/configuration/Configuration$Settings;->getNewsWidgetVersion()Ljava/lang/String;

    move-result-object v0

    const-string v1, "2017-2"

    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isVipChatEnabled()Z
    .locals 1

    .line 124
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->getVipStatus()Lnet/gogame/gowrap/VipStatus;

    move-result-object v0

    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->isVipChatEnabled(Lnet/gogame/gowrap/VipStatus;)Z

    move-result v0

    return v0
.end method

.method public loadHtml(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 168
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object v0

    instance-of v0, v0, Lnet/gogame/gowrap/ui/InternalWebViewContext;

    if-eqz v0, :cond_0

    .line 170
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/InternalWebViewContext;

    .line 171
    invoke-interface {v0, p1, p2}, Lnet/gogame/gowrap/ui/InternalWebViewContext;->loadHtml(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 175
    :cond_0
    invoke-static {p1, p2}, Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;->newFragmentWithHtml(Ljava/lang/String;Ljava/lang/String;)Lnet/gogame/gowrap/ui/v2017_1/InternalWebViewFragment;

    move-result-object p1

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->pushFragment(Landroid/app/Fragment;)V

    return-void
.end method

.method public loadUrl(Ljava/lang/String;Z)V
    .locals 2

    if-eqz p2, :cond_1

    if-eqz p1, :cond_1

    .line 146
    sget-object p2, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {p2}, Lnet/gogame/gowrap/GoWrapImpl;->getGuid()Ljava/lang/String;

    move-result-object p2

    if-nez p2, :cond_0

    const-string p2, ""

    :cond_0
    const-string v0, "${guid}"

    .line 150
    invoke-static {v0}, Ljava/util/regex/Pattern;->quote(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    :try_start_0
    const-string v1, "UTF-8"

    .line 152
    invoke-static {p2, v1}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, v0, p2}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    move-object p1, p2

    .line 157
    :catch_0
    :cond_1
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object p2

    instance-of p2, p2, Lnet/gogame/gowrap/ui/WebViewContext;

    if-eqz p2, :cond_2

    .line 158
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object p2

    check-cast p2, Lnet/gogame/gowrap/ui/WebViewContext;

    .line 159
    invoke-interface {p2, p1}, Lnet/gogame/gowrap/ui/WebViewContext;->loadUrl(Ljava/lang/String;)Z

    move-result p2

    if-eqz p2, :cond_2

    return-void

    .line 163
    :cond_2
    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;->newFragmentWithUrl(Ljava/lang/String;)Lnet/gogame/gowrap/ui/v2017_1/WebViewFragment;

    move-result-object p1

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->pushFragment(Landroid/app/Fragment;)V

    return-void
.end method

.method public onBackPressed()V
    .locals 1

    .line 115
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->goBack()Z

    move-result v0

    if-nez v0, :cond_0

    .line 116
    invoke-super {p0}, Landroid/app/Activity;->onBackPressed()V

    :cond_0
    return-void
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 4

    .line 70
    invoke-super {p0, p1}, Landroid/app/Activity;->onCreate(Landroid/os/Bundle;)V

    .line 72
    new-instance p1, Lnet/gogame/gowrap/support/LocaleManager;

    invoke-direct {p1, p0}, Lnet/gogame/gowrap/support/LocaleManager;-><init>(Landroid/content/Context;)V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->localeManager:Lnet/gogame/gowrap/support/LocaleManager;

    .line 73
    iget-object p1, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->localeManager:Lnet/gogame/gowrap/support/LocaleManager;

    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {v0, p0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getCurrentLocale(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/support/LocaleManager;->setLocale(Ljava/lang/String;)V

    const/4 p1, 0x1

    .line 76
    :try_start_0
    new-instance v0, Ljava/io/File;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getCacheDir()Ljava/io/File;

    move-result-object v1

    const-string v2, "net/gogame/gowrap/cache"

    invoke-direct {v0, v1, v2}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    const/4 v1, 0x2

    const-wide/32 v2, 0x4000000

    invoke-static {v0, v1, p1, v2, v3}, Lnet/gogame/gowrap/support/DiskLruCache;->open(Ljava/io/File;IIJ)Lnet/gogame/gowrap/support/DiskLruCache;

    move-result-object v0

    sput-object v0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->diskLruCache:Lnet/gogame/gowrap/support/DiskLruCache;
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 80
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 82
    :goto_0
    new-instance v0, Lnet/gogame/gowrap/support/DefaultDownloadManager;

    sget-object v1, Lnet/gogame/gowrap/ui/AbstractMainActivity;->diskLruCache:Lnet/gogame/gowrap/support/DiskLruCache;

    invoke-direct {v0, p0, v1}, Lnet/gogame/gowrap/support/DefaultDownloadManager;-><init>(Landroid/content/Context;Lnet/gogame/gowrap/support/DiskLruCache;)V

    sput-object v0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->downloadManager:Lnet/gogame/gowrap/support/DownloadManager;

    .line 84
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getLocaleConfiguration(Landroid/content/Context;)Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->localeConfiguration:Lnet/gogame/gowrap/model/configuration/Configuration$Integrations$Core$LocaleConfiguration;

    .line 86
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0, p1}, Lnet/gogame/gowrap/GoWrapImpl;->checkVipStatus(Z)V

    .line 88
    sget-object p1, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {p1}, Lnet/gogame/gowrap/GoWrapImpl;->onMenuOpened()V

    return-void
.end method

.method protected onDestroy()V
    .locals 1

    .line 108
    invoke-super {p0}, Landroid/app/Activity;->onDestroy()V

    .line 110
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {v0}, Lnet/gogame/gowrap/GoWrapImpl;->onMenuClosed()V

    return-void
.end method

.method protected abstract onEnterFullscreen()V
.end method

.method protected abstract onExitFullscreen()V
.end method

.method protected onPause()V
    .locals 2

    .line 101
    invoke-super {p0}, Landroid/app/Activity;->onPause()V

    .line 103
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->listener:Lnet/gogame/gowrap/GoWrapImpl$Listener;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/GoWrapImpl;->removeListener(Lnet/gogame/gowrap/GoWrapImpl$Listener;)V

    return-void
.end method

.method protected onResume()V
    .locals 2

    .line 93
    invoke-super {p0}, Landroid/app/Activity;->onResume()V

    .line 95
    invoke-static {p0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->lockOrientation(Landroid/app/Activity;)V

    .line 96
    sget-object v0, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/AbstractMainActivity;->listener:Lnet/gogame/gowrap/GoWrapImpl$Listener;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/GoWrapImpl;->addListener(Lnet/gogame/gowrap/GoWrapImpl$Listener;)V

    return-void
.end method

.method public pushFragment(Landroid/app/Fragment;)V
    .locals 3

    .line 129
    invoke-static {p0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->hideSoftKeyboard(Landroid/app/Activity;)V

    .line 130
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v0

    .line 131
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getFragmentManager()Landroid/app/FragmentManager;

    move-result-object v1

    invoke-virtual {v1}, Landroid/app/FragmentManager;->beginTransaction()Landroid/app/FragmentTransaction;

    move-result-object v1

    .line 132
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getFragmentContainerViewId()I

    move-result v2

    invoke-virtual {v1, v2, p1, v0}, Landroid/app/FragmentTransaction;->replace(ILandroid/app/Fragment;Ljava/lang/String;)Landroid/app/FragmentTransaction;

    .line 133
    invoke-virtual {v1, v0}, Landroid/app/FragmentTransaction;->addToBackStack(Ljava/lang/String;)Landroid/app/FragmentTransaction;

    .line 134
    invoke-virtual {v1}, Landroid/app/FragmentTransaction;->commitAllowingStateLoss()I

    return-void
.end method

.method protected showInitialFragment()V
    .locals 1

    .line 271
    invoke-static {p0}, Lnet/gogame/gowrap/support/NetworkUtils;->isNetworkAvailable(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 272
    sget-object v0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 273
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->isUseNews2017_2()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 274
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object v0

    instance-of v0, v0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    if-nez v0, :cond_3

    .line 275
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_2/NewsFragment;-><init>()V

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->pushFragment(Landroid/app/Fragment;)V

    goto :goto_0

    .line 278
    :cond_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object v0

    instance-of v0, v0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    if-nez v0, :cond_3

    .line 279
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_1/NewsFeedFragment;-><init>()V

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->pushFragment(Landroid/app/Fragment;)V

    goto :goto_0

    .line 283
    :cond_1
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object v0

    instance-of v0, v0, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;

    if-nez v0, :cond_3

    .line 284
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_1/CommunityFragment;-><init>()V

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->pushFragment(Landroid/app/Fragment;)V

    goto :goto_0

    .line 288
    :cond_2
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->getActiveFragment()Landroid/app/Fragment;

    move-result-object v0

    instance-of v0, v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    if-nez v0, :cond_3

    .line 289
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;

    invoke-direct {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFragment;-><init>()V

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/ui/AbstractMainActivity;->pushFragment(Landroid/app/Fragment;)V

    :cond_3
    :goto_0
    return-void
.end method

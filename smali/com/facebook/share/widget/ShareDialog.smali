.class public final Lcom/facebook/share/widget/ShareDialog;
.super Lcom/facebook/internal/FacebookDialogBase;
.source "ShareDialog.java"

# interfaces
.implements Lcom/facebook/share/Sharer;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/facebook/share/widget/ShareDialog$ShareStoryHandler;,
        Lcom/facebook/share/widget/ShareDialog$CameraEffectHandler;,
        Lcom/facebook/share/widget/ShareDialog$FeedHandler;,
        Lcom/facebook/share/widget/ShareDialog$WebShareHandler;,
        Lcom/facebook/share/widget/ShareDialog$NativeHandler;,
        Lcom/facebook/share/widget/ShareDialog$Mode;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/facebook/internal/FacebookDialogBase<",
        "Lcom/facebook/share/model/ShareContent;",
        "Lcom/facebook/share/Sharer$Result;",
        ">;",
        "Lcom/facebook/share/Sharer;"
    }
.end annotation


# static fields
.field private static final DEFAULT_REQUEST_CODE:I

.field private static final FEED_DIALOG:Ljava/lang/String; = "feed"

.field private static final TAG:Ljava/lang/String; = "ShareDialog"

.field private static final WEB_OG_SHARE_DIALOG:Ljava/lang/String; = "share_open_graph"

.field public static final WEB_SHARE_DIALOG:Ljava/lang/String; = "share"


# instance fields
.field private isAutomaticMode:Z

.field private shouldFailOnDataError:Z


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 104
    sget-object v0, Lcom/facebook/internal/CallbackManagerImpl$RequestCodeOffset;->Share:Lcom/facebook/internal/CallbackManagerImpl$RequestCodeOffset;

    .line 105
    invoke-virtual {v0}, Lcom/facebook/internal/CallbackManagerImpl$RequestCodeOffset;->toRequestCode()I

    move-result v0

    sput v0, Lcom/facebook/share/widget/ShareDialog;->DEFAULT_REQUEST_CODE:I

    return-void
.end method

.method public constructor <init>(Landroid/app/Activity;)V
    .locals 1

    .line 215
    sget v0, Lcom/facebook/share/widget/ShareDialog;->DEFAULT_REQUEST_CODE:I

    invoke-direct {p0, p1, v0}, Lcom/facebook/internal/FacebookDialogBase;-><init>(Landroid/app/Activity;I)V

    const/4 p1, 0x0

    .line 107
    iput-boolean p1, p0, Lcom/facebook/share/widget/ShareDialog;->shouldFailOnDataError:Z

    const/4 p1, 0x1

    .line 109
    iput-boolean p1, p0, Lcom/facebook/share/widget/ShareDialog;->isAutomaticMode:Z

    .line 217
    sget p1, Lcom/facebook/share/widget/ShareDialog;->DEFAULT_REQUEST_CODE:I

    invoke-static {p1}, Lcom/facebook/share/internal/ShareInternalUtility;->registerStaticShareCallback(I)V

    return-void
.end method

.method constructor <init>(Landroid/app/Activity;I)V
    .locals 0

    .line 245
    invoke-direct {p0, p1, p2}, Lcom/facebook/internal/FacebookDialogBase;-><init>(Landroid/app/Activity;I)V

    const/4 p1, 0x0

    .line 107
    iput-boolean p1, p0, Lcom/facebook/share/widget/ShareDialog;->shouldFailOnDataError:Z

    const/4 p1, 0x1

    .line 109
    iput-boolean p1, p0, Lcom/facebook/share/widget/ShareDialog;->isAutomaticMode:Z

    .line 247
    invoke-static {p2}, Lcom/facebook/share/internal/ShareInternalUtility;->registerStaticShareCallback(I)V

    return-void
.end method

.method public constructor <init>(Landroid/app/Fragment;)V
    .locals 1

    .line 234
    new-instance v0, Lcom/facebook/internal/FragmentWrapper;

    invoke-direct {v0, p1}, Lcom/facebook/internal/FragmentWrapper;-><init>(Landroid/app/Fragment;)V

    invoke-direct {p0, v0}, Lcom/facebook/share/widget/ShareDialog;-><init>(Lcom/facebook/internal/FragmentWrapper;)V

    return-void
.end method

.method constructor <init>(Landroid/app/Fragment;I)V
    .locals 1

    .line 257
    new-instance v0, Lcom/facebook/internal/FragmentWrapper;

    invoke-direct {v0, p1}, Lcom/facebook/internal/FragmentWrapper;-><init>(Landroid/app/Fragment;)V

    invoke-direct {p0, v0, p2}, Lcom/facebook/share/widget/ShareDialog;-><init>(Lcom/facebook/internal/FragmentWrapper;I)V

    return-void
.end method

.method public constructor <init>(Landroidx/fragment/app/Fragment;)V
    .locals 1

    .line 225
    new-instance v0, Lcom/facebook/internal/FragmentWrapper;

    invoke-direct {v0, p1}, Lcom/facebook/internal/FragmentWrapper;-><init>(Landroidx/fragment/app/Fragment;)V

    invoke-direct {p0, v0}, Lcom/facebook/share/widget/ShareDialog;-><init>(Lcom/facebook/internal/FragmentWrapper;)V

    return-void
.end method

.method constructor <init>(Landroidx/fragment/app/Fragment;I)V
    .locals 1

    .line 252
    new-instance v0, Lcom/facebook/internal/FragmentWrapper;

    invoke-direct {v0, p1}, Lcom/facebook/internal/FragmentWrapper;-><init>(Landroidx/fragment/app/Fragment;)V

    invoke-direct {p0, v0, p2}, Lcom/facebook/share/widget/ShareDialog;-><init>(Lcom/facebook/internal/FragmentWrapper;I)V

    return-void
.end method

.method private constructor <init>(Lcom/facebook/internal/FragmentWrapper;)V
    .locals 1

    .line 238
    sget v0, Lcom/facebook/share/widget/ShareDialog;->DEFAULT_REQUEST_CODE:I

    invoke-direct {p0, p1, v0}, Lcom/facebook/internal/FacebookDialogBase;-><init>(Lcom/facebook/internal/FragmentWrapper;I)V

    const/4 p1, 0x0

    .line 107
    iput-boolean p1, p0, Lcom/facebook/share/widget/ShareDialog;->shouldFailOnDataError:Z

    const/4 p1, 0x1

    .line 109
    iput-boolean p1, p0, Lcom/facebook/share/widget/ShareDialog;->isAutomaticMode:Z

    .line 240
    sget p1, Lcom/facebook/share/widget/ShareDialog;->DEFAULT_REQUEST_CODE:I

    invoke-static {p1}, Lcom/facebook/share/internal/ShareInternalUtility;->registerStaticShareCallback(I)V

    return-void
.end method

.method private constructor <init>(Lcom/facebook/internal/FragmentWrapper;I)V
    .locals 0

    .line 261
    invoke-direct {p0, p1, p2}, Lcom/facebook/internal/FacebookDialogBase;-><init>(Lcom/facebook/internal/FragmentWrapper;I)V

    const/4 p1, 0x0

    .line 107
    iput-boolean p1, p0, Lcom/facebook/share/widget/ShareDialog;->shouldFailOnDataError:Z

    const/4 p1, 0x1

    .line 109
    iput-boolean p1, p0, Lcom/facebook/share/widget/ShareDialog;->isAutomaticMode:Z

    .line 263
    invoke-static {p2}, Lcom/facebook/share/internal/ShareInternalUtility;->registerStaticShareCallback(I)V

    return-void
.end method

.method static synthetic access$1000(Lcom/facebook/share/widget/ShareDialog;)Landroid/app/Activity;
    .locals 0

    .line 72
    invoke-virtual {p0}, Lcom/facebook/share/widget/ShareDialog;->getActivityContext()Landroid/app/Activity;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$1100(Lcom/facebook/share/widget/ShareDialog;)Landroid/app/Activity;
    .locals 0

    .line 72
    invoke-virtual {p0}, Lcom/facebook/share/widget/ShareDialog;->getActivityContext()Landroid/app/Activity;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$500(Ljava/lang/Class;)Z
    .locals 0

    .line 72
    invoke-static {p0}, Lcom/facebook/share/widget/ShareDialog;->canShowNative(Ljava/lang/Class;)Z

    move-result p0

    return p0
.end method

.method static synthetic access$600(Lcom/facebook/share/widget/ShareDialog;)Landroid/app/Activity;
    .locals 0

    .line 72
    invoke-virtual {p0}, Lcom/facebook/share/widget/ShareDialog;->getActivityContext()Landroid/app/Activity;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$700(Lcom/facebook/share/widget/ShareDialog;Landroid/content/Context;Lcom/facebook/share/model/ShareContent;Lcom/facebook/share/widget/ShareDialog$Mode;)V
    .locals 0

    .line 72
    invoke-direct {p0, p1, p2, p3}, Lcom/facebook/share/widget/ShareDialog;->logDialogShare(Landroid/content/Context;Lcom/facebook/share/model/ShareContent;Lcom/facebook/share/widget/ShareDialog$Mode;)V

    return-void
.end method

.method static synthetic access$800(Ljava/lang/Class;)Lcom/facebook/internal/DialogFeature;
    .locals 0

    .line 72
    invoke-static {p0}, Lcom/facebook/share/widget/ShareDialog;->getFeature(Ljava/lang/Class;)Lcom/facebook/internal/DialogFeature;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$900(Lcom/facebook/share/model/ShareContent;)Z
    .locals 0

    .line 72
    invoke-static {p0}, Lcom/facebook/share/widget/ShareDialog;->canShowWebCheck(Lcom/facebook/share/model/ShareContent;)Z

    move-result p0

    return p0
.end method

.method public static canShow(Ljava/lang/Class;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "+",
            "Lcom/facebook/share/model/ShareContent;",
            ">;)Z"
        }
    .end annotation

    .line 165
    invoke-static {p0}, Lcom/facebook/share/widget/ShareDialog;->canShowWebTypeCheck(Ljava/lang/Class;)Z

    move-result v0

    if-nez v0, :cond_1

    invoke-static {p0}, Lcom/facebook/share/widget/ShareDialog;->canShowNative(Ljava/lang/Class;)Z

    move-result p0

    if-eqz p0, :cond_0

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p0, 0x1

    :goto_1
    return p0
.end method

.method private static canShowNative(Ljava/lang/Class;)Z
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "+",
            "Lcom/facebook/share/model/ShareContent;",
            ">;)Z"
        }
    .end annotation

    .line 169
    invoke-static {p0}, Lcom/facebook/share/widget/ShareDialog;->getFeature(Ljava/lang/Class;)Lcom/facebook/internal/DialogFeature;

    move-result-object p0

    if-eqz p0, :cond_0

    .line 171
    invoke-static {p0}, Lcom/facebook/internal/DialogPresenter;->canPresentNativeDialogWithFeature(Lcom/facebook/internal/DialogFeature;)Z

    move-result p0

    if-eqz p0, :cond_0

    const/4 p0, 0x1

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    return p0
.end method

.method private static canShowWebCheck(Lcom/facebook/share/model/ShareContent;)Z
    .locals 3

    .line 190
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-static {v0}, Lcom/facebook/share/widget/ShareDialog;->canShowWebTypeCheck(Ljava/lang/Class;)Z

    move-result v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 194
    :cond_0
    instance-of v0, p0, Lcom/facebook/share/model/ShareOpenGraphContent;

    if-eqz v0, :cond_1

    .line 195
    check-cast p0, Lcom/facebook/share/model/ShareOpenGraphContent;

    .line 197
    :try_start_0
    invoke-static {p0}, Lcom/facebook/share/internal/ShareInternalUtility;->toJSONObjectForWeb(Lcom/facebook/share/model/ShareOpenGraphContent;)Lorg/json/JSONObject;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 199
    sget-object v0, Lcom/facebook/share/widget/ShareDialog;->TAG:Ljava/lang/String;

    const-string v2, "canShow returned false because the content of the Opem Graph object can\'t be shared via the web dialog"

    invoke-static {v0, v2, p0}, Lcom/facebook/internal/Utility;->logd(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    return v1

    :cond_1
    :goto_0
    const/4 p0, 0x1

    return p0
.end method

.method private static canShowWebTypeCheck(Ljava/lang/Class;)Z
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "+",
            "Lcom/facebook/share/model/ShareContent;",
            ">;)Z"
        }
    .end annotation

    .line 183
    const-class v0, Lcom/facebook/share/model/ShareLinkContent;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-nez v0, :cond_1

    const-class v0, Lcom/facebook/share/model/ShareOpenGraphContent;

    .line 184
    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-nez v0, :cond_1

    const-class v0, Lcom/facebook/share/model/SharePhotoContent;

    .line 185
    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result p0

    if-eqz p0, :cond_0

    .line 186
    invoke-static {}, Lcom/facebook/AccessToken;->isCurrentAccessTokenActive()Z

    move-result p0

    if-eqz p0, :cond_0

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p0, 0x1

    :goto_1
    return p0
.end method

.method private static getFeature(Ljava/lang/Class;)Lcom/facebook/internal/DialogFeature;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "+",
            "Lcom/facebook/share/model/ShareContent;",
            ">;)",
            "Lcom/facebook/internal/DialogFeature;"
        }
    .end annotation

    .line 594
    const-class v0, Lcom/facebook/share/model/ShareLinkContent;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 595
    sget-object p0, Lcom/facebook/share/internal/ShareDialogFeature;->SHARE_DIALOG:Lcom/facebook/share/internal/ShareDialogFeature;

    return-object p0

    .line 596
    :cond_0
    const-class v0, Lcom/facebook/share/model/SharePhotoContent;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 597
    sget-object p0, Lcom/facebook/share/internal/ShareDialogFeature;->PHOTOS:Lcom/facebook/share/internal/ShareDialogFeature;

    return-object p0

    .line 598
    :cond_1
    const-class v0, Lcom/facebook/share/model/ShareVideoContent;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 599
    sget-object p0, Lcom/facebook/share/internal/ShareDialogFeature;->VIDEO:Lcom/facebook/share/internal/ShareDialogFeature;

    return-object p0

    .line 600
    :cond_2
    const-class v0, Lcom/facebook/share/model/ShareOpenGraphContent;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 601
    sget-object p0, Lcom/facebook/share/internal/OpenGraphActionDialogFeature;->OG_ACTION_DIALOG:Lcom/facebook/share/internal/OpenGraphActionDialogFeature;

    return-object p0

    .line 602
    :cond_3
    const-class v0, Lcom/facebook/share/model/ShareMediaContent;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-eqz v0, :cond_4

    .line 603
    sget-object p0, Lcom/facebook/share/internal/ShareDialogFeature;->MULTIMEDIA:Lcom/facebook/share/internal/ShareDialogFeature;

    return-object p0

    .line 604
    :cond_4
    const-class v0, Lcom/facebook/share/model/ShareCameraEffectContent;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result v0

    if-eqz v0, :cond_5

    .line 605
    sget-object p0, Lcom/facebook/share/internal/CameraEffectFeature;->SHARE_CAMERA_EFFECT:Lcom/facebook/share/internal/CameraEffectFeature;

    return-object p0

    .line 606
    :cond_5
    const-class v0, Lcom/facebook/share/model/ShareStoryContent;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->isAssignableFrom(Ljava/lang/Class;)Z

    move-result p0

    if-eqz p0, :cond_6

    .line 607
    sget-object p0, Lcom/facebook/share/internal/ShareStoryFeature;->SHARE_STORY_ASSET:Lcom/facebook/share/internal/ShareStoryFeature;

    return-object p0

    :cond_6
    const/4 p0, 0x0

    return-object p0
.end method

.method private logDialogShare(Landroid/content/Context;Lcom/facebook/share/model/ShareContent;Lcom/facebook/share/widget/ShareDialog$Mode;)V
    .locals 2

    .line 614
    iget-boolean v0, p0, Lcom/facebook/share/widget/ShareDialog;->isAutomaticMode:Z

    if-eqz v0, :cond_0

    .line 615
    sget-object p3, Lcom/facebook/share/widget/ShareDialog$Mode;->AUTOMATIC:Lcom/facebook/share/widget/ShareDialog$Mode;

    .line 618
    :cond_0
    sget-object v0, Lcom/facebook/share/widget/ShareDialog$1;->$SwitchMap$com$facebook$share$widget$ShareDialog$Mode:[I

    invoke-virtual {p3}, Lcom/facebook/share/widget/ShareDialog$Mode;->ordinal()I

    move-result p3

    aget p3, v0, p3

    packed-switch p3, :pswitch_data_0

    const-string p3, "unknown"

    goto :goto_0

    :pswitch_0
    const-string p3, "native"

    goto :goto_0

    :pswitch_1
    const-string p3, "web"

    goto :goto_0

    :pswitch_2
    const-string p3, "automatic"

    .line 634
    :goto_0
    invoke-virtual {p2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p2

    invoke-static {p2}, Lcom/facebook/share/widget/ShareDialog;->getFeature(Ljava/lang/Class;)Lcom/facebook/internal/DialogFeature;

    move-result-object p2

    .line 635
    sget-object v0, Lcom/facebook/share/internal/ShareDialogFeature;->SHARE_DIALOG:Lcom/facebook/share/internal/ShareDialogFeature;

    if-ne p2, v0, :cond_1

    const-string p2, "status"

    goto :goto_1

    .line 637
    :cond_1
    sget-object v0, Lcom/facebook/share/internal/ShareDialogFeature;->PHOTOS:Lcom/facebook/share/internal/ShareDialogFeature;

    if-ne p2, v0, :cond_2

    const-string p2, "photo"

    goto :goto_1

    .line 639
    :cond_2
    sget-object v0, Lcom/facebook/share/internal/ShareDialogFeature;->VIDEO:Lcom/facebook/share/internal/ShareDialogFeature;

    if-ne p2, v0, :cond_3

    const-string p2, "video"

    goto :goto_1

    .line 641
    :cond_3
    sget-object v0, Lcom/facebook/share/internal/OpenGraphActionDialogFeature;->OG_ACTION_DIALOG:Lcom/facebook/share/internal/OpenGraphActionDialogFeature;

    if-ne p2, v0, :cond_4

    const-string p2, "open_graph"

    goto :goto_1

    :cond_4
    const-string p2, "unknown"

    .line 647
    :goto_1
    new-instance v0, Lcom/facebook/appevents/InternalAppEventsLogger;

    invoke-direct {v0, p1}, Lcom/facebook/appevents/InternalAppEventsLogger;-><init>(Landroid/content/Context;)V

    .line 648
    new-instance p1, Landroid/os/Bundle;

    invoke-direct {p1}, Landroid/os/Bundle;-><init>()V

    const-string v1, "fb_share_dialog_show"

    .line 649
    invoke-virtual {p1, v1, p3}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p3, "fb_share_dialog_content_type"

    .line 653
    invoke-virtual {p1, p3, p2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p2, "fb_share_dialog_show"

    .line 657
    invoke-virtual {v0, p2, p1}, Lcom/facebook/appevents/InternalAppEventsLogger;->logEventImplicitly(Ljava/lang/String;Landroid/os/Bundle;)V

    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public static show(Landroid/app/Activity;Lcom/facebook/share/model/ShareContent;)V
    .locals 1

    .line 121
    new-instance v0, Lcom/facebook/share/widget/ShareDialog;

    invoke-direct {v0, p0}, Lcom/facebook/share/widget/ShareDialog;-><init>(Landroid/app/Activity;)V

    invoke-virtual {v0, p1}, Lcom/facebook/share/widget/ShareDialog;->show(Ljava/lang/Object;)V

    return-void
.end method

.method public static show(Landroid/app/Fragment;Lcom/facebook/share/model/ShareContent;)V
    .locals 1

    .line 147
    new-instance v0, Lcom/facebook/internal/FragmentWrapper;

    invoke-direct {v0, p0}, Lcom/facebook/internal/FragmentWrapper;-><init>(Landroid/app/Fragment;)V

    invoke-static {v0, p1}, Lcom/facebook/share/widget/ShareDialog;->show(Lcom/facebook/internal/FragmentWrapper;Lcom/facebook/share/model/ShareContent;)V

    return-void
.end method

.method public static show(Landroidx/fragment/app/Fragment;Lcom/facebook/share/model/ShareContent;)V
    .locals 1

    .line 134
    new-instance v0, Lcom/facebook/internal/FragmentWrapper;

    invoke-direct {v0, p0}, Lcom/facebook/internal/FragmentWrapper;-><init>(Landroidx/fragment/app/Fragment;)V

    invoke-static {v0, p1}, Lcom/facebook/share/widget/ShareDialog;->show(Lcom/facebook/internal/FragmentWrapper;Lcom/facebook/share/model/ShareContent;)V

    return-void
.end method

.method private static show(Lcom/facebook/internal/FragmentWrapper;Lcom/facebook/share/model/ShareContent;)V
    .locals 1

    .line 153
    new-instance v0, Lcom/facebook/share/widget/ShareDialog;

    invoke-direct {v0, p0}, Lcom/facebook/share/widget/ShareDialog;-><init>(Lcom/facebook/internal/FragmentWrapper;)V

    invoke-virtual {v0, p1}, Lcom/facebook/share/widget/ShareDialog;->show(Ljava/lang/Object;)V

    return-void
.end method


# virtual methods
.method public canShow(Lcom/facebook/share/model/ShareContent;Lcom/facebook/share/widget/ShareDialog$Mode;)Z
    .locals 1

    .line 291
    sget-object v0, Lcom/facebook/share/widget/ShareDialog$Mode;->AUTOMATIC:Lcom/facebook/share/widget/ShareDialog$Mode;

    if-ne p2, v0, :cond_0

    sget-object p2, Lcom/facebook/share/widget/ShareDialog;->BASE_AUTOMATIC_MODE:Ljava/lang/Object;

    :cond_0
    invoke-virtual {p0, p1, p2}, Lcom/facebook/share/widget/ShareDialog;->canShowImpl(Ljava/lang/Object;Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method

.method protected createBaseAppCall()Lcom/facebook/internal/AppCall;
    .locals 2

    .line 306
    new-instance v0, Lcom/facebook/internal/AppCall;

    invoke-virtual {p0}, Lcom/facebook/share/widget/ShareDialog;->getRequestCode()I

    move-result v1

    invoke-direct {v0, v1}, Lcom/facebook/internal/AppCall;-><init>(I)V

    return-object v0
.end method

.method protected getOrderedModeHandlers()Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/facebook/internal/FacebookDialogBase<",
            "Lcom/facebook/share/model/ShareContent;",
            "Lcom/facebook/share/Sharer$Result;",
            ">.ModeHandler;>;"
        }
    .end annotation

    .line 311
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 312
    new-instance v1, Lcom/facebook/share/widget/ShareDialog$NativeHandler;

    const/4 v2, 0x0

    invoke-direct {v1, p0, v2}, Lcom/facebook/share/widget/ShareDialog$NativeHandler;-><init>(Lcom/facebook/share/widget/ShareDialog;Lcom/facebook/share/widget/ShareDialog$1;)V

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 313
    new-instance v1, Lcom/facebook/share/widget/ShareDialog$FeedHandler;

    invoke-direct {v1, p0, v2}, Lcom/facebook/share/widget/ShareDialog$FeedHandler;-><init>(Lcom/facebook/share/widget/ShareDialog;Lcom/facebook/share/widget/ShareDialog$1;)V

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 314
    new-instance v1, Lcom/facebook/share/widget/ShareDialog$WebShareHandler;

    invoke-direct {v1, p0, v2}, Lcom/facebook/share/widget/ShareDialog$WebShareHandler;-><init>(Lcom/facebook/share/widget/ShareDialog;Lcom/facebook/share/widget/ShareDialog$1;)V

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 315
    new-instance v1, Lcom/facebook/share/widget/ShareDialog$CameraEffectHandler;

    invoke-direct {v1, p0, v2}, Lcom/facebook/share/widget/ShareDialog$CameraEffectHandler;-><init>(Lcom/facebook/share/widget/ShareDialog;Lcom/facebook/share/widget/ShareDialog$1;)V

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 316
    new-instance v1, Lcom/facebook/share/widget/ShareDialog$ShareStoryHandler;

    invoke-direct {v1, p0, v2}, Lcom/facebook/share/widget/ShareDialog$ShareStoryHandler;-><init>(Lcom/facebook/share/widget/ShareDialog;Lcom/facebook/share/widget/ShareDialog$1;)V

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    return-object v0
.end method

.method public getShouldFailOnDataError()Z
    .locals 1

    .line 276
    iget-boolean v0, p0, Lcom/facebook/share/widget/ShareDialog;->shouldFailOnDataError:Z

    return v0
.end method

.method protected registerCallbackImpl(Lcom/facebook/internal/CallbackManagerImpl;Lcom/facebook/FacebookCallback;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/facebook/internal/CallbackManagerImpl;",
            "Lcom/facebook/FacebookCallback<",
            "Lcom/facebook/share/Sharer$Result;",
            ">;)V"
        }
    .end annotation

    .line 271
    invoke-virtual {p0}, Lcom/facebook/share/widget/ShareDialog;->getRequestCode()I

    move-result v0

    .line 270
    invoke-static {v0, p1, p2}, Lcom/facebook/share/internal/ShareInternalUtility;->registerSharerCallback(ILcom/facebook/CallbackManager;Lcom/facebook/FacebookCallback;)V

    return-void
.end method

.method public setShouldFailOnDataError(Z)V
    .locals 0

    .line 281
    iput-boolean p1, p0, Lcom/facebook/share/widget/ShareDialog;->shouldFailOnDataError:Z

    return-void
.end method

.method public show(Lcom/facebook/share/model/ShareContent;Lcom/facebook/share/widget/ShareDialog$Mode;)V
    .locals 1

    .line 299
    sget-object v0, Lcom/facebook/share/widget/ShareDialog$Mode;->AUTOMATIC:Lcom/facebook/share/widget/ShareDialog$Mode;

    if-ne p2, v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    iput-boolean v0, p0, Lcom/facebook/share/widget/ShareDialog;->isAutomaticMode:Z

    .line 301
    iget-boolean v0, p0, Lcom/facebook/share/widget/ShareDialog;->isAutomaticMode:Z

    if-eqz v0, :cond_1

    sget-object p2, Lcom/facebook/share/widget/ShareDialog;->BASE_AUTOMATIC_MODE:Ljava/lang/Object;

    :cond_1
    invoke-virtual {p0, p1, p2}, Lcom/facebook/share/widget/ShareDialog;->showImpl(Ljava/lang/Object;Ljava/lang/Object;)V

    return-void
.end method

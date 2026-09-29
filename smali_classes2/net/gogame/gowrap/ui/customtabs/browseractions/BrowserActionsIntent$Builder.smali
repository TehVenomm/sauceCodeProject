.class public final Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;
.super Ljava/lang/Object;
.source "BrowserActionsIntent.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private mContext:Landroid/content/Context;

.field private final mIntent:Landroid/content/Intent;

.field private mMenuItems:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Landroid/os/Bundle;",
            ">;"
        }
    .end annotation
.end field

.field private mOnItemSelectedPendingIntent:Landroid/app/PendingIntent;

.field private mType:I

.field private mUri:Landroid/net/Uri;


# direct methods
.method public constructor <init>(Landroid/content/Context;Landroid/net/Uri;)V
    .locals 2

    .line 156
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 142
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.support.customtabs.browseractions.browser_action_open"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mIntent:Landroid/content/Intent;

    const/4 v0, 0x0

    .line 147
    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mMenuItems:Ljava/util/ArrayList;

    .line 148
    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mOnItemSelectedPendingIntent:Landroid/app/PendingIntent;

    .line 157
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mContext:Landroid/content/Context;

    .line 158
    iput-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mUri:Landroid/net/Uri;

    const/4 p1, 0x0

    .line 159
    iput p1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mType:I

    .line 160
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mMenuItems:Ljava/util/ArrayList;

    return-void
.end method

.method private getBundleFromItem(Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;)Landroid/os/Bundle;
    .locals 3

    .line 210
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "android.support.customtabs.browseractions.TITLE"

    .line 211
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->getTitle()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v1, "android.support.customtabs.browseractions.ACTION"

    .line 212
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->getAction()Landroid/app/PendingIntent;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 213
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->getIcon()Landroid/graphics/Bitmap;

    move-result-object v1

    if-eqz v1, :cond_0

    const-string v1, "android.support.customtabs.browseractions.ICON"

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->getIcon()Landroid/graphics/Bitmap;

    move-result-object p1

    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    :cond_0
    return-object v0
.end method


# virtual methods
.method public build()Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;
    .locals 3

    .line 222
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mIntent:Landroid/content/Intent;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mUri:Landroid/net/Uri;

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    .line 223
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.browseractions.extra.TYPE"

    iget v2, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mType:I

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    .line 224
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.browseractions.extra.MENU_ITEMS"

    iget-object v2, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mMenuItems:Ljava/util/ArrayList;

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putParcelableArrayListExtra(Ljava/lang/String;Ljava/util/ArrayList;)Landroid/content/Intent;

    .line 225
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mContext:Landroid/content/Context;

    new-instance v1, Landroid/content/Intent;

    invoke-direct {v1}, Landroid/content/Intent;-><init>()V

    const/4 v2, 0x0

    invoke-static {v0, v2, v1, v2}, Landroid/app/PendingIntent;->getActivity(Landroid/content/Context;ILandroid/content/Intent;I)Landroid/app/PendingIntent;

    move-result-object v0

    .line 226
    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v2, "android.support.customtabs.browseractions.APP_ID"

    invoke-virtual {v1, v2, v0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    .line 227
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.browseractions.extra.SELECTED_ACTION_PENDING_INTENT"

    iget-object v2, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mOnItemSelectedPendingIntent:Landroid/app/PendingIntent;

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    .line 228
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mIntent:Landroid/content/Intent;

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;-><init>(Landroid/content/Intent;Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$1;)V

    return-object v0
.end method

.method public setCustomItems(Ljava/util/ArrayList;)Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;",
            ">;)",
            "Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;"
        }
    .end annotation

    .line 179
    invoke-virtual {p1}, Ljava/util/ArrayList;->size()I

    move-result v0

    const/4 v1, 0x5

    if-ge v0, v1, :cond_3

    const/4 v0, 0x0

    .line 183
    :goto_0
    invoke-virtual {p1}, Ljava/util/ArrayList;->size()I

    move-result v1

    if-ge v0, v1, :cond_2

    .line 184
    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;

    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->getTitle()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 186
    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;

    invoke-virtual {v1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->getAction()Landroid/app/PendingIntent;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 189
    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mMenuItems:Ljava/util/ArrayList;

    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;

    invoke-direct {p0, v2}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->getBundleFromItem(Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;)Landroid/os/Bundle;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 187
    :cond_0
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Custom item action is null"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 185
    :cond_1
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "Custom item title is null"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1

    :cond_2
    return-object p0

    .line 180
    :cond_3
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string v0, "Exceeded maximum toolbar item count of 5"

    invoke-direct {p1, v0}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public setOnItemSelectedAction(Landroid/app/PendingIntent;)Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;
    .locals 0

    .line 200
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mOnItemSelectedPendingIntent:Landroid/app/PendingIntent;

    return-object p0
.end method

.method public setUrlType(I)Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;
    .locals 0

    .line 168
    iput p1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->mType:I

    return-object p0
.end method

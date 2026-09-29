.class public Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;
.super Ljava/lang/Object;
.source "BrowserActionItem.java"


# instance fields
.field private final mAction:Landroid/app/PendingIntent;

.field private mIcon:Landroid/graphics/Bitmap;

.field private final mTitle:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Landroid/app/PendingIntent;)V
    .locals 1

    const/4 v0, 0x0

    .line 49
    invoke-direct {p0, p1, p2, v0}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;-><init>(Ljava/lang/String;Landroid/app/PendingIntent;Landroid/graphics/Bitmap;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Landroid/app/PendingIntent;Landroid/graphics/Bitmap;)V
    .locals 0

    .line 37
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 38
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->mTitle:Ljava/lang/String;

    .line 39
    iput-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->mAction:Landroid/app/PendingIntent;

    .line 40
    iput-object p3, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->mIcon:Landroid/graphics/Bitmap;

    return-void
.end method


# virtual methods
.method public getAction()Landroid/app/PendingIntent;
    .locals 1

    .line 78
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->mAction:Landroid/app/PendingIntent;

    return-object v0
.end method

.method public getIcon()Landroid/graphics/Bitmap;
    .locals 1

    .line 64
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->mIcon:Landroid/graphics/Bitmap;

    return-object v0
.end method

.method public getTitle()Ljava/lang/String;
    .locals 1

    .line 71
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->mTitle:Ljava/lang/String;

    return-object v0
.end method

.method public setIcon(Landroid/graphics/Bitmap;)V
    .locals 0

    .line 57
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->mIcon:Landroid/graphics/Bitmap;

    return-void
.end method

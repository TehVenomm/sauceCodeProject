.class public Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;
.super Ljava/lang/Object;
.source "CustomDialog.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/dialog/CustomDialog;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# instance fields
.field private canceledOnTouchOutside:Z

.field private final context:Landroid/content/Context;

.field private listener:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;

.field private message:Ljava/lang/String;

.field private title:Ljava/lang/String;

.field private type:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 191
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 193
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->context:Landroid/content/Context;

    return-void
.end method


# virtual methods
.method public build()Lnet/gogame/gowrap/ui/dialog/CustomDialog;
    .locals 2

    .line 197
    new-instance v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->context:Landroid/content/Context;

    invoke-direct {v0, v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;-><init>(Landroid/content/Context;)V

    .line 198
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->type:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->setType(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;)V

    .line 199
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->title:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->setTitle(Ljava/lang/String;)V

    .line 200
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->message:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->setMessage(Ljava/lang/String;)V

    .line 201
    iget-boolean v1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->canceledOnTouchOutside:Z

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->setCanceledOnTouchOutside(Z)V

    .line 202
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->listener:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->setListener(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;)V

    return-object v0
.end method

.method public withCanceledOnTouchOutside(Z)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;
    .locals 0

    .line 232
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->canceledOnTouchOutside:Z

    return-object p0
.end method

.method public withListener(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;
    .locals 0

    .line 237
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->listener:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;

    return-object p0
.end method

.method public withMessage(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;
    .locals 1

    .line 227
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->message:Ljava/lang/String;

    return-object p0
.end method

.method public withMessage(Ljava/lang/String;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;
    .locals 0

    .line 222
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->message:Ljava/lang/String;

    return-object p0
.end method

.method public withTitle(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;
    .locals 1

    .line 217
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->title:Ljava/lang/String;

    return-object p0
.end method

.method public withTitle(Ljava/lang/String;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;
    .locals 0

    .line 212
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->title:Ljava/lang/String;

    return-object p0
.end method

.method public withType(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;
    .locals 0

    .line 207
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->type:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    return-object p0
.end method

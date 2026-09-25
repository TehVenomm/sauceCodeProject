.class Lnet/gogame/gowrap/ui/dialog/CustomDialog$2;
.super Ljava/lang/Object;
.source "CustomDialog.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/dialog/CustomDialog;-><init>(Landroid/content/Context;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/dialog/CustomDialog;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/dialog/CustomDialog;)V
    .locals 0

    .line 43
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$2;->this$0:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 47
    iget-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$2;->this$0:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->access$000(Lnet/gogame/gowrap/ui/dialog/CustomDialog;)Z

    move-result p1

    if-eqz p1, :cond_0

    .line 48
    iget-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$2;->this$0:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->dismiss()V

    :cond_0
    return-void
.end method

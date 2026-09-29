.class Lnet/gogame/gowrap/ui/dialog/CustomDialog$1;
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

    .line 36
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$1;->this$0:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 40
    iget-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$1;->this$0:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->dismiss()V

    return-void
.end method

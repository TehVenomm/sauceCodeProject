.class Lnet/gogame/gowrap/ui/MainActivity$1;
.super Ljava/lang/Object;
.source "MainActivity.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/MainActivity;->onCreate(Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private clicks:I

.field final synthetic this$0:Lnet/gogame/gowrap/ui/MainActivity;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/MainActivity;)V
    .locals 0

    .line 41
    iput-object p1, p0, Lnet/gogame/gowrap/ui/MainActivity$1;->this$0:Lnet/gogame/gowrap/ui/MainActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 p1, 0x0

    .line 43
    iput p1, p0, Lnet/gogame/gowrap/ui/MainActivity$1;->clicks:I

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    .line 47
    iget v0, p0, Lnet/gogame/gowrap/ui/MainActivity$1;->clicks:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lnet/gogame/gowrap/ui/MainActivity$1;->clicks:I

    .line 48
    iget v0, p0, Lnet/gogame/gowrap/ui/MainActivity$1;->clicks:I

    const/16 v1, 0xa

    if-lt v0, v1, :cond_0

    .line 49
    invoke-virtual {p1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gowrap/support/BuildInfo;->showBuildInfoDialog(Landroid/content/Context;)V

    :cond_0
    return-void
.end method

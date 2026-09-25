.class final Lnet/gogame/gowrap/support/BuildInfo$1;
.super Ljava/lang/Object;
.source "BuildInfo.java"

# interfaces
.implements Landroid/view/View$OnLongClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/support/BuildInfo;->showBuildInfoDialog(Landroid/content/Context;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$buildInfo:Ljava/lang/String;

.field final synthetic val$context:Landroid/content/Context;


# direct methods
.method constructor <init>(Ljava/lang/String;Landroid/content/Context;)V
    .locals 0

    .line 87
    iput-object p1, p0, Lnet/gogame/gowrap/support/BuildInfo$1;->val$buildInfo:Ljava/lang/String;

    iput-object p2, p0, Lnet/gogame/gowrap/support/BuildInfo$1;->val$context:Landroid/content/Context;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onLongClick(Landroid/view/View;)Z
    .locals 3

    .line 91
    sget p1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/4 v0, 0x0

    const/16 v1, 0xb

    if-lt p1, v1, :cond_0

    const-string p1, "Build info"

    .line 92
    iget-object v1, p0, Lnet/gogame/gowrap/support/BuildInfo$1;->val$buildInfo:Ljava/lang/String;

    invoke-static {p1, v1}, Landroid/content/ClipData;->newPlainText(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Landroid/content/ClipData;

    move-result-object p1

    .line 93
    iget-object v1, p0, Lnet/gogame/gowrap/support/BuildInfo$1;->val$context:Landroid/content/Context;

    const-string v2, "clipboard"

    invoke-virtual {v1, v2}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/content/ClipboardManager;

    .line 95
    invoke-virtual {v1, p1}, Landroid/content/ClipboardManager;->setPrimaryClip(Landroid/content/ClipData;)V

    .line 97
    iget-object p1, p0, Lnet/gogame/gowrap/support/BuildInfo$1;->val$context:Landroid/content/Context;

    const-string v1, "Build info copied to clipboard"

    invoke-static {p1, v1, v0}, Landroid/widget/Toast;->makeText(Landroid/content/Context;Ljava/lang/CharSequence;I)Landroid/widget/Toast;

    move-result-object p1

    .line 99
    invoke-virtual {p1}, Landroid/widget/Toast;->show()V

    :cond_0
    return v0
.end method

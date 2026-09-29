.class Lnet/gogame/gowrap/support/DefaultDownloadManager$4$2;
.super Ljava/lang/Object;
.source "DefaultDownloadManager.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->onDownloadFailed()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/gowrap/support/DefaultDownloadManager$4;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/support/DefaultDownloadManager$4;)V
    .locals 0

    .line 159
    iput-object p1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4$2;->this$1:Lnet/gogame/gowrap/support/DefaultDownloadManager$4;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .line 164
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4$2;->this$1:Lnet/gogame/gowrap/support/DefaultDownloadManager$4;

    iget-object v0, v0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->val$request:Lnet/gogame/gowrap/support/DownloadManager$Request;

    invoke-virtual {v0}, Lnet/gogame/gowrap/support/DownloadManager$Request;->getErrorResourceId()Ljava/lang/Integer;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 165
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4$2;->this$1:Lnet/gogame/gowrap/support/DefaultDownloadManager$4;

    iget-object v0, v0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->val$target:Lnet/gogame/gowrap/support/DownloadManager$Target;

    iget-object v1, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4$2;->this$1:Lnet/gogame/gowrap/support/DefaultDownloadManager$4;

    iget-object v1, v1, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->this$0:Lnet/gogame/gowrap/support/DefaultDownloadManager;

    invoke-static {v1}, Lnet/gogame/gowrap/support/DefaultDownloadManager;->access$400(Lnet/gogame/gowrap/support/DefaultDownloadManager;)Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    iget-object v2, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4$2;->this$1:Lnet/gogame/gowrap/support/DefaultDownloadManager$4;

    iget-object v2, v2, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->val$request:Lnet/gogame/gowrap/support/DownloadManager$Request;

    .line 166
    invoke-virtual {v2}, Lnet/gogame/gowrap/support/DownloadManager$Request;->getErrorResourceId()Ljava/lang/Integer;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Integer;->intValue()I

    move-result v2

    invoke-virtual {v1, v2}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v1

    .line 165
    invoke-interface {v0, v1}, Lnet/gogame/gowrap/support/DownloadManager$Target;->onDownloadFailed(Landroid/graphics/drawable/Drawable;)V

    goto :goto_0

    .line 168
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4$2;->this$1:Lnet/gogame/gowrap/support/DefaultDownloadManager$4;

    iget-object v0, v0, Lnet/gogame/gowrap/support/DefaultDownloadManager$4;->val$target:Lnet/gogame/gowrap/support/DownloadManager$Target;

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Lnet/gogame/gowrap/support/DownloadManager$Target;->onDownloadFailed(Landroid/graphics/drawable/Drawable;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 171
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

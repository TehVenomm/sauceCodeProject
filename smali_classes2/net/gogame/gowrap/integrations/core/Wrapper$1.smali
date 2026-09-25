.class Lnet/gogame/gowrap/integrations/core/Wrapper$1;
.super Ljava/lang/Object;
.source "Wrapper.java"

# interfaces
.implements Lnet/gogame/gowrap/support/DownloadUtils$Callback;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/integrations/core/Wrapper;->setup(Landroid/content/Context;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/integrations/core/Wrapper;

.field final synthetic val$context:Landroid/content/Context;

.field final synthetic val$statusFile:Ljava/io/File;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/integrations/core/Wrapper;Ljava/io/File;Landroid/content/Context;)V
    .locals 0

    .line 174
    iput-object p1, p0, Lnet/gogame/gowrap/integrations/core/Wrapper$1;->this$0:Lnet/gogame/gowrap/integrations/core/Wrapper;

    iput-object p2, p0, Lnet/gogame/gowrap/integrations/core/Wrapper$1;->val$statusFile:Ljava/io/File;

    iput-object p3, p0, Lnet/gogame/gowrap/integrations/core/Wrapper$1;->val$context:Landroid/content/Context;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onDownloadFailed()V
    .locals 0

    return-void
.end method

.method public onDownloadSucceeded()V
    .locals 3

    .line 179
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper$1;->val$statusFile:Ljava/io/File;

    invoke-static {v0}, Lnet/gogame/gowrap/support/JSONUtils;->read(Ljava/io/File;)Lorg/json/JSONObject;

    move-result-object v0

    .line 180
    iget-object v1, p0, Lnet/gogame/gowrap/integrations/core/Wrapper$1;->this$0:Lnet/gogame/gowrap/integrations/core/Wrapper;

    iget-object v2, p0, Lnet/gogame/gowrap/integrations/core/Wrapper$1;->this$0:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-static {v2, v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->access$100(Lnet/gogame/gowrap/integrations/core/Wrapper;Lorg/json/JSONObject;)Lnet/gogame/gowrap/integrations/core/ServerStatus;

    move-result-object v0

    invoke-static {v1, v0}, Lnet/gogame/gowrap/integrations/core/Wrapper;->access$002(Lnet/gogame/gowrap/integrations/core/Wrapper;Lnet/gogame/gowrap/integrations/core/ServerStatus;)Lnet/gogame/gowrap/integrations/core/ServerStatus;

    .line 181
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper$1;->val$statusFile:Ljava/io/File;

    invoke-virtual {v0}, Ljava/io/File;->delete()Z

    .line 183
    iget-object v0, p0, Lnet/gogame/gowrap/integrations/core/Wrapper$1;->val$context:Landroid/content/Context;

    check-cast v0, Landroid/app/Activity;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/fab/FabManager;->update(Landroid/app/Activity;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 185
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

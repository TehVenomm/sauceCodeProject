.class final Lcom/appsflyer/f$4;
.super Ljava/lang/Object;
.source ""

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/appsflyer/f;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field private synthetic ˋ:Lcom/appsflyer/f;


# direct methods
.method constructor <init>(Lcom/appsflyer/f;)V
    .locals 0

    .line 62
    iput-object p1, p0, Lcom/appsflyer/f$4;->ˋ:Lcom/appsflyer/f;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 3

    .line 65
    iget-object v0, p0, Lcom/appsflyer/f$4;->ˋ:Lcom/appsflyer/f;

    iget-object v0, v0, Lcom/appsflyer/f;->ˎ:Ljava/lang/Object;

    monitor-enter v0

    .line 66
    :try_start_0
    iget-object v1, p0, Lcom/appsflyer/f$4;->ˋ:Lcom/appsflyer/f;

    iget-boolean v1, v1, Lcom/appsflyer/f;->ˏ:Z

    if-eqz v1, :cond_0

    .line 68
    iget-object v1, p0, Lcom/appsflyer/f$4;->ˋ:Lcom/appsflyer/f;

    iget-object v1, v1, Lcom/appsflyer/f;->ˋ:Landroid/os/Handler;

    iget-object v2, p0, Lcom/appsflyer/f$4;->ˋ:Lcom/appsflyer/f;

    iget-object v2, v2, Lcom/appsflyer/f;->ˊ:Ljava/lang/Runnable;

    invoke-virtual {v1, v2}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    .line 69
    iget-object v1, p0, Lcom/appsflyer/f$4;->ˋ:Lcom/appsflyer/f;

    iget-object v1, v1, Lcom/appsflyer/f;->ˋ:Landroid/os/Handler;

    iget-object v2, p0, Lcom/appsflyer/f$4;->ˋ:Lcom/appsflyer/f;

    iget-object v2, v2, Lcom/appsflyer/f;->ॱ:Ljava/lang/Runnable;

    invoke-virtual {v1, v2}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    .line 71
    iget-object v1, p0, Lcom/appsflyer/f$4;->ˋ:Lcom/appsflyer/f;

    invoke-virtual {v1}, Lcom/appsflyer/f;->ॱ()V

    .line 72
    iget-object v1, p0, Lcom/appsflyer/f$4;->ˋ:Lcom/appsflyer/f;

    const/4 v2, 0x0

    iput-boolean v2, v1, Lcom/appsflyer/f;->ˏ:Z

    .line 74
    :cond_0
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    return-void

    :catchall_0
    move-exception v1

    monitor-exit v0

    throw v1
.end method

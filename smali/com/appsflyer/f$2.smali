.class final Lcom/appsflyer/f$2;
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
.field private synthetic ˎ:Lcom/appsflyer/f;


# direct methods
.method constructor <init>(Lcom/appsflyer/f;)V
    .locals 0

    .line 52
    iput-object p1, p0, Lcom/appsflyer/f$2;->ˎ:Lcom/appsflyer/f;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final run()V
    .locals 5

    .line 55
    iget-object v0, p0, Lcom/appsflyer/f$2;->ˎ:Lcom/appsflyer/f;

    iget-object v0, v0, Lcom/appsflyer/f;->ˎ:Ljava/lang/Object;

    monitor-enter v0

    .line 56
    :try_start_0
    iget-object v1, p0, Lcom/appsflyer/f$2;->ˎ:Lcom/appsflyer/f;

    invoke-virtual {v1}, Lcom/appsflyer/f;->ˊ()V

    .line 57
    iget-object v1, p0, Lcom/appsflyer/f$2;->ˎ:Lcom/appsflyer/f;

    iget-object v1, v1, Lcom/appsflyer/f;->ˋ:Landroid/os/Handler;

    iget-object v2, p0, Lcom/appsflyer/f$2;->ˎ:Lcom/appsflyer/f;

    iget-object v2, v2, Lcom/appsflyer/f;->ॱ:Ljava/lang/Runnable;

    const-wide/16 v3, 0x1f4

    invoke-virtual {v1, v2, v3, v4}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    .line 58
    iget-object v1, p0, Lcom/appsflyer/f$2;->ˎ:Lcom/appsflyer/f;

    const/4 v2, 0x1

    iput-boolean v2, v1, Lcom/appsflyer/f;->ˏ:Z

    .line 59
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    return-void

    :catchall_0
    move-exception v1

    monitor-exit v0

    throw v1
.end method

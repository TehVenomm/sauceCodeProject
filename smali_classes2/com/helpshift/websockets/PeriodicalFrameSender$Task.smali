.class final Lcom/helpshift/websockets/PeriodicalFrameSender$Task;
.super Ljava/util/TimerTask;
.source "PeriodicalFrameSender.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/websockets/PeriodicalFrameSender;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x12
    name = "Task"
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/websockets/PeriodicalFrameSender;


# direct methods
.method constructor <init>(Lcom/helpshift/websockets/PeriodicalFrameSender;)V
    .locals 0

    .line 179
    iput-object p1, p0, Lcom/helpshift/websockets/PeriodicalFrameSender$Task;->this$0:Lcom/helpshift/websockets/PeriodicalFrameSender;

    invoke-direct {p0}, Ljava/util/TimerTask;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 184
    iget-object v0, p0, Lcom/helpshift/websockets/PeriodicalFrameSender$Task;->this$0:Lcom/helpshift/websockets/PeriodicalFrameSender;

    invoke-virtual {v0}, Lcom/helpshift/websockets/PeriodicalFrameSender;->doTask()V

    return-void
.end method

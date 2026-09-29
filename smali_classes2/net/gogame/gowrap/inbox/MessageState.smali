.class public Lnet/gogame/gowrap/inbox/MessageState;
.super Ljava/lang/Object;
.source "MessageState.java"

# interfaces
.implements Ljava/io/Serializable;


# instance fields
.field private id:J

.field private read:Z

.field private timestamp:J

.field private type:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 5
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getId()J
    .locals 2

    .line 21
    iget-wide v0, p0, Lnet/gogame/gowrap/inbox/MessageState;->id:J

    return-wide v0
.end method

.method public getTimestamp()J
    .locals 2

    .line 29
    iget-wide v0, p0, Lnet/gogame/gowrap/inbox/MessageState;->timestamp:J

    return-wide v0
.end method

.method public getType()Ljava/lang/String;
    .locals 1

    .line 13
    iget-object v0, p0, Lnet/gogame/gowrap/inbox/MessageState;->type:Ljava/lang/String;

    return-object v0
.end method

.method public isRead()Z
    .locals 1

    .line 37
    iget-boolean v0, p0, Lnet/gogame/gowrap/inbox/MessageState;->read:Z

    return v0
.end method

.method public setId(J)V
    .locals 0

    .line 25
    iput-wide p1, p0, Lnet/gogame/gowrap/inbox/MessageState;->id:J

    return-void
.end method

.method public setRead(Z)V
    .locals 0

    .line 41
    iput-boolean p1, p0, Lnet/gogame/gowrap/inbox/MessageState;->read:Z

    return-void
.end method

.method public setTimestamp(J)V
    .locals 0

    .line 33
    iput-wide p1, p0, Lnet/gogame/gowrap/inbox/MessageState;->timestamp:J

    return-void
.end method

.method public setType(Ljava/lang/String;)V
    .locals 0

    .line 17
    iput-object p1, p0, Lnet/gogame/gowrap/inbox/MessageState;->type:Ljava/lang/String;

    return-void
.end method

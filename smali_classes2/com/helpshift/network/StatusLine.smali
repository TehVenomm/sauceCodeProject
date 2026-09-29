.class public Lcom/helpshift/network/StatusLine;
.super Ljava/lang/Object;
.source "StatusLine.java"


# instance fields
.field private statusCode:I


# direct methods
.method public constructor <init>(ILjava/lang/String;)V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 8
    iput p1, p0, Lcom/helpshift/network/StatusLine;->statusCode:I

    return-void
.end method


# virtual methods
.method public getStatusCode()I
    .locals 1

    .line 12
    iget v0, p0, Lcom/helpshift/network/StatusLine;->statusCode:I

    return v0
.end method

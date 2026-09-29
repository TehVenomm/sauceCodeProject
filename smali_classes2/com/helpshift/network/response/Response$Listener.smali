.class public interface abstract Lcom/helpshift/network/response/Response$Listener;
.super Ljava/lang/Object;
.source "Response.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/network/response/Response;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "Listener"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;"
    }
.end annotation


# virtual methods
.method public abstract onResponse(Ljava/lang/Object;Ljava/lang/Integer;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;",
            "Ljava/lang/Integer;",
            ")V"
        }
    .end annotation
.end method

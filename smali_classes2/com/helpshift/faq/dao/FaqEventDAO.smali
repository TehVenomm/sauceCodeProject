.class public interface abstract Lcom/helpshift/faq/dao/FaqEventDAO;
.super Ljava/lang/Object;
.source "FaqEventDAO.java"


# virtual methods
.method public abstract getUnSentFaqMarkHelpfulEvents()Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Boolean;",
            ">;"
        }
    .end annotation
.end method

.method public abstract insertFaqMarkHelpfulEvent(Ljava/lang/String;Z)V
.end method

.method public abstract removeFaqMarkHelpfulEvent(Ljava/lang/String;)V
.end method

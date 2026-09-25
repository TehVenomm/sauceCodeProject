.class public interface abstract Lcom/helpshift/support/storage/FaqDAO;
.super Ljava/lang/Object;
.source "FaqDAO.java"


# virtual methods
.method public abstract addFaq(Lcom/helpshift/support/Faq;)V
.end method

.method public abstract getAllFaqPublishIds()Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getFaq(Ljava/lang/String;)Lcom/helpshift/support/Faq;
.end method

.method public abstract getFaq(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/support/Faq;
.end method

.method public abstract getFaqsDataForSection(Ljava/lang/String;)Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getFaqsForSection(Ljava/lang/String;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Lcom/helpshift/support/FaqTagFilter;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getFilteredFaqs(Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;",
            "Lcom/helpshift/support/FaqTagFilter;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Faq;",
            ">;"
        }
    .end annotation
.end method

.method public abstract removeFaq(Ljava/lang/String;)V
.end method

.method public abstract setIsHelpful(Ljava/lang/String;Ljava/lang/Boolean;)I
.end method

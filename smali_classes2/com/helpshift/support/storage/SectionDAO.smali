.class public interface abstract Lcom/helpshift/support/storage/SectionDAO;
.super Ljava/lang/Object;
.source "SectionDAO.java"


# virtual methods
.method public abstract clearSectionsData()V
.end method

.method public abstract getAllSections()Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Section;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getAllSections(Lcom/helpshift/support/FaqTagFilter;)Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/support/FaqTagFilter;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Section;",
            ">;"
        }
    .end annotation
.end method

.method public abstract getSection(Ljava/lang/String;)Lcom/helpshift/support/Section;
.end method

.method public abstract storeSections(Lorg/json/JSONArray;)V
.end method

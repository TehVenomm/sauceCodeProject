.class public final Lcom/helpshift/faq/FaqCore;
.super Ljava/lang/Object;
.source "FaqCore.java"


# instance fields
.field public final body:Ljava/lang/String;

.field public final categoryTags:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field public final is_helpful:I

.field public final is_rtl:Ljava/lang/Boolean;

.field public final language:Ljava/lang/String;

.field public final publish_id:Ljava/lang/String;

.field public final qId:Ljava/lang/String;

.field public final section_id:Ljava/lang/String;

.field public final tags:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field public final title:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILjava/lang/Boolean;Ljava/util/List;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "I",
            "Ljava/lang/Boolean;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 23
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 24
    iput-object p1, p0, Lcom/helpshift/faq/FaqCore;->qId:Ljava/lang/String;

    .line 25
    iput-object p2, p0, Lcom/helpshift/faq/FaqCore;->publish_id:Ljava/lang/String;

    .line 26
    iput-object p3, p0, Lcom/helpshift/faq/FaqCore;->language:Ljava/lang/String;

    .line 27
    iput-object p4, p0, Lcom/helpshift/faq/FaqCore;->section_id:Ljava/lang/String;

    .line 28
    iput-object p5, p0, Lcom/helpshift/faq/FaqCore;->title:Ljava/lang/String;

    .line 29
    iput-object p6, p0, Lcom/helpshift/faq/FaqCore;->body:Ljava/lang/String;

    .line 30
    iput p7, p0, Lcom/helpshift/faq/FaqCore;->is_helpful:I

    .line 31
    iput-object p8, p0, Lcom/helpshift/faq/FaqCore;->is_rtl:Ljava/lang/Boolean;

    .line 32
    iput-object p9, p0, Lcom/helpshift/faq/FaqCore;->tags:Ljava/util/List;

    .line 33
    iput-object p10, p0, Lcom/helpshift/faq/FaqCore;->categoryTags:Ljava/util/List;

    return-void
.end method

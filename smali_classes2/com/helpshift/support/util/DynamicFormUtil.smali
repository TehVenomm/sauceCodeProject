.class public Lcom/helpshift/support/util/DynamicFormUtil;
.super Ljava/lang/Object;
.source "DynamicFormUtil.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static toFlow(Landroid/content/Context;Ljava/util/HashMap;)Lcom/helpshift/support/flows/Flow;
    .locals 6

    const-string v0, "type"

    .line 28
    invoke-virtual {p1, v0}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 29
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    const-string v2, "config"

    .line 30
    invoke-virtual {p1, v2}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    const-string v1, "config"

    .line 31
    invoke-virtual {p1, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/HashMap;

    :cond_0
    const-string v2, "titleResourceName"

    .line 33
    invoke-virtual {p1, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    const/4 v3, 0x0

    if-eqz v2, :cond_1

    .line 37
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    const-string v4, "string"

    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v3, v2, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v3

    :cond_1
    const-string v2, ""

    if-nez v3, :cond_2

    const-string v2, "title"

    .line 42
    invoke-virtual {p1, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    :cond_2
    const-string v4, "faqsFlow"

    .line 45
    invoke-virtual {v0, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_4

    if-eqz v3, :cond_3

    .line 47
    new-instance p0, Lcom/helpshift/support/flows/FAQsFlow;

    invoke-direct {p0, v3, v1}, Lcom/helpshift/support/flows/FAQsFlow;-><init>(ILjava/util/Map;)V

    goto/16 :goto_1

    .line 50
    :cond_3
    new-instance p0, Lcom/helpshift/support/flows/FAQsFlow;

    invoke-direct {p0, v2, v1}, Lcom/helpshift/support/flows/FAQsFlow;-><init>(Ljava/lang/String;Ljava/util/Map;)V

    goto/16 :goto_1

    :cond_4
    const-string v4, "conversationFlow"

    .line 53
    invoke-virtual {v0, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_6

    if-eqz v3, :cond_5

    .line 55
    new-instance p0, Lcom/helpshift/support/flows/ConversationFlow;

    invoke-direct {p0, v3, v1}, Lcom/helpshift/support/flows/ConversationFlow;-><init>(ILjava/util/Map;)V

    goto :goto_1

    .line 58
    :cond_5
    new-instance p0, Lcom/helpshift/support/flows/ConversationFlow;

    invoke-direct {p0, v2, v1}, Lcom/helpshift/support/flows/ConversationFlow;-><init>(Ljava/lang/String;Ljava/util/Map;)V

    goto :goto_1

    :cond_6
    const-string v4, "faqSectionFlow"

    .line 61
    invoke-virtual {v0, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_8

    const-string p0, "data"

    .line 62
    invoke-virtual {p1, p0}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Ljava/lang/String;

    if-eqz v3, :cond_7

    .line 64
    new-instance p1, Lcom/helpshift/support/flows/FAQSectionFlow;

    invoke-direct {p1, v3, p0, v1}, Lcom/helpshift/support/flows/FAQSectionFlow;-><init>(ILjava/lang/String;Ljava/util/Map;)V

    goto :goto_0

    .line 67
    :cond_7
    new-instance p1, Lcom/helpshift/support/flows/FAQSectionFlow;

    invoke-direct {p1, v2, p0, v1}, Lcom/helpshift/support/flows/FAQSectionFlow;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    goto :goto_0

    :cond_8
    const-string v4, "singleFaqFlow"

    .line 70
    invoke-virtual {v0, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_a

    const-string p0, "data"

    .line 71
    invoke-virtual {p1, p0}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Ljava/lang/String;

    if-eqz v3, :cond_9

    .line 73
    new-instance p1, Lcom/helpshift/support/flows/SingleFAQFlow;

    invoke-direct {p1, v3, p0, v1}, Lcom/helpshift/support/flows/SingleFAQFlow;-><init>(ILjava/lang/String;Ljava/util/Map;)V

    goto :goto_0

    .line 76
    :cond_9
    new-instance p1, Lcom/helpshift/support/flows/SingleFAQFlow;

    invoke-direct {p1, v2, p0, v1}, Lcom/helpshift/support/flows/SingleFAQFlow;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V

    goto :goto_0

    :cond_a
    const-string v1, "dynamicFormFlow"

    .line 79
    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_c

    const-string v0, "data"

    .line 80
    invoke-virtual {p1, v0}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/ArrayList;

    .line 81
    invoke-static {p0, p1}, Lcom/helpshift/support/util/DynamicFormUtil;->toFlowList(Landroid/content/Context;Ljava/util/List;)Ljava/util/List;

    move-result-object p0

    if-eqz v3, :cond_b

    .line 83
    new-instance p1, Lcom/helpshift/support/flows/DynamicFormFlow;

    invoke-direct {p1, v3, p0}, Lcom/helpshift/support/flows/DynamicFormFlow;-><init>(ILjava/util/List;)V

    :goto_0
    move-object p0, p1

    goto :goto_1

    .line 86
    :cond_b
    new-instance p1, Lcom/helpshift/support/flows/DynamicFormFlow;

    invoke-direct {p1, v2, p0}, Lcom/helpshift/support/flows/DynamicFormFlow;-><init>(Ljava/lang/String;Ljava/util/List;)V

    goto :goto_0

    :cond_c
    const/4 p0, 0x0

    :goto_1
    return-object p0
.end method

.method public static toFlowList(Landroid/content/Context;Ljava/util/List;)Ljava/util/List;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;>;)",
            "Ljava/util/List<",
            "Lcom/helpshift/support/flows/Flow;",
            ">;"
        }
    .end annotation

    .line 19
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 20
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/HashMap;

    .line 21
    invoke-static {p0, v1}, Lcom/helpshift/support/util/DynamicFormUtil;->toFlow(Landroid/content/Context;Ljava/util/HashMap;)Lcom/helpshift/support/flows/Flow;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    return-object v0
.end method

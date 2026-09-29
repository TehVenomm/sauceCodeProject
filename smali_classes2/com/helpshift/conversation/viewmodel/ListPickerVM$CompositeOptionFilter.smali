.class Lcom/helpshift/conversation/viewmodel/ListPickerVM$CompositeOptionFilter;
.super Ljava/lang/Object;
.source "ListPickerVM.java"

# interfaces
.implements Lcom/helpshift/conversation/viewmodel/ListPickerVM$OptionFilter;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/viewmodel/ListPickerVM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "CompositeOptionFilter"
.end annotation


# instance fields
.field private optionFilters:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/ListPickerVM$OptionFilter;",
            ">;"
        }
    .end annotation
.end field

.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ListPickerVM;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/ListPickerVM$OptionFilter;",
            ">;)V"
        }
    .end annotation

    .line 250
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$CompositeOptionFilter;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 251
    iput-object p2, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$CompositeOptionFilter;->optionFilters:Ljava/util/List;

    return-void
.end method


# virtual methods
.method public filter(Ljava/util/List;Ljava/util/List;)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;",
            ">;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;",
            ">;"
        }
    .end annotation

    .line 256
    new-instance v0, Ljava/util/LinkedHashSet;

    invoke-direct {v0}, Ljava/util/LinkedHashSet;-><init>()V

    .line 257
    iget-object v1, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$CompositeOptionFilter;->optionFilters:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/viewmodel/ListPickerVM$OptionFilter;

    .line 258
    invoke-interface {v2, p1, p2}, Lcom/helpshift/conversation/viewmodel/ListPickerVM$OptionFilter;->filter(Ljava/util/List;Ljava/util/List;)Ljava/util/List;

    move-result-object v2

    invoke-interface {v0, v2}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    goto :goto_0

    .line 260
    :cond_0
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1, v0}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    return-object p1
.end method

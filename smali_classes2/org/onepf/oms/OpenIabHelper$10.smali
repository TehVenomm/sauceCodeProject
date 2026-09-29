.class Lorg/onepf/oms/OpenIabHelper$10;
.super Ljava/lang/Object;
.source "OpenIabHelper.java"

# interfaces
.implements Lorg/onepf/oms/OpenIabHelper$OpenStoresDiscoveredListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lorg/onepf/oms/OpenIabHelper;->setup(Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lorg/onepf/oms/OpenIabHelper;

.field final synthetic val$appstoresToCheck:Ljava/util/Set;

.field final synthetic val$availableStoreNames:Ljava/util/Set;

.field final synthetic val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;


# direct methods
.method constructor <init>(Lorg/onepf/oms/OpenIabHelper;Ljava/util/Set;Ljava/util/Set;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;)V
    .locals 0

    .line 674
    iput-object p1, p0, Lorg/onepf/oms/OpenIabHelper$10;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iput-object p2, p0, Lorg/onepf/oms/OpenIabHelper$10;->val$availableStoreNames:Ljava/util/Set;

    iput-object p3, p0, Lorg/onepf/oms/OpenIabHelper$10;->val$appstoresToCheck:Ljava/util/Set;

    iput-object p4, p0, Lorg/onepf/oms/OpenIabHelper$10;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public openStoresDiscovered(Ljava/util/List;)V
    .locals 5
    .param p1    # Ljava/util/List;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lorg/onepf/oms/Appstore;",
            ">;)V"
        }
    .end annotation

    .line 677
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0, p1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 679
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$10;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {p1}, Lorg/onepf/oms/OpenIabHelper;->access$1000(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Map;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_0
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 680
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$10;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v2}, Lorg/onepf/oms/OpenIabHelper;->access$1000(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Map;

    move-result-object v2

    invoke-interface {v2, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 681
    invoke-static {v2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v3

    if-nez v3, :cond_0

    iget-object v3, p0, Lorg/onepf/oms/OpenIabHelper$10;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v3}, Lorg/onepf/oms/OpenIabHelper;->access$1100(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Map;

    move-result-object v3

    invoke-interface {v3, v2}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    iget-object v3, p0, Lorg/onepf/oms/OpenIabHelper$10;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v3}, Lorg/onepf/oms/OpenIabHelper;->access$000(Lorg/onepf/oms/OpenIabHelper;)Landroid/content/Context;

    move-result-object v3

    invoke-static {v3, v1}, Lorg/onepf/oms/util/Utils;->packageInstalled(Landroid/content/Context;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 684
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$10;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v1}, Lorg/onepf/oms/OpenIabHelper;->access$1100(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Map;

    move-result-object v1

    invoke-interface {v1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;

    invoke-interface {v1}, Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;->get()Lorg/onepf/oms/Appstore;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 688
    :cond_1
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$10;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {p1}, Lorg/onepf/oms/OpenIabHelper;->access$1100(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Map;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_2
    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_3

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 689
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$10;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v2}, Lorg/onepf/oms/OpenIabHelper;->access$1000(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Map;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Map;->values()Ljava/util/Collection;

    move-result-object v2

    invoke-interface {v2, v1}, Ljava/util/Collection;->contains(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    .line 690
    iget-object v2, p0, Lorg/onepf/oms/OpenIabHelper$10;->this$0:Lorg/onepf/oms/OpenIabHelper;

    invoke-static {v2}, Lorg/onepf/oms/OpenIabHelper;->access$1100(Lorg/onepf/oms/OpenIabHelper;)Ljava/util/Map;

    move-result-object v2

    invoke-interface {v2, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;

    invoke-interface {v1}, Lorg/onepf/oms/OpenIabHelper$AppstoreFactory;->get()Lorg/onepf/oms/Appstore;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 694
    :cond_3
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$10;->val$availableStoreNames:Ljava/util/Set;

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_4
    :goto_2
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_6

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 695
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_5
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_4

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lorg/onepf/oms/Appstore;

    .line 696
    invoke-interface {v3}, Lorg/onepf/oms/Appstore;->getAppstoreName()Ljava/lang/String;

    move-result-object v4

    invoke-static {v4, v1}, Landroid/text/TextUtils;->equals(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Z

    move-result v4

    if-eqz v4, :cond_5

    .line 697
    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$10;->val$appstoresToCheck:Ljava/util/Set;

    invoke-interface {v1, v3}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 703
    :cond_6
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$10;->val$appstoresToCheck:Ljava/util/Set;

    invoke-interface {p1, v0}, Ljava/util/Set;->addAll(Ljava/util/Collection;)Z

    .line 704
    iget-object p1, p0, Lorg/onepf/oms/OpenIabHelper$10;->this$0:Lorg/onepf/oms/OpenIabHelper;

    iget-object v0, p0, Lorg/onepf/oms/OpenIabHelper$10;->val$listener:Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;

    iget-object v1, p0, Lorg/onepf/oms/OpenIabHelper$10;->val$appstoresToCheck:Ljava/util/Set;

    invoke-static {p1, v0, v1}, Lorg/onepf/oms/OpenIabHelper;->access$1200(Lorg/onepf/oms/OpenIabHelper;Lorg/onepf/oms/appstore/googleUtils/IabHelper$OnIabSetupFinishedListener;Ljava/util/Collection;)V

    return-void
.end method
